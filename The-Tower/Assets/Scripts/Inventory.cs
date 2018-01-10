using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Inventory : MonoBehaviour {

    public float tim;

    public RoomControl rom;

    public SceneControl cont;

    public Collider2D col;
    public LayerMask loot;
    public LayerMask all;
    
    public Collider2D[] coly;
    

    public GameObject tkorlv;
    public Text found;
    public Text current;

    public int total;

    public int money;

    public GameObject pop;


    public Loot[] line;


    public int[] slot; //0: null 1: Weapon 2: Secondary 3: Other 4: Head 5: Armour 6: Feet

    public PlayerRpg rpg;
        

    

    // Use this for initialization
    void Start () {
      
        cont = GameObject.FindGameObjectWithTag("Control").GetComponent<SceneControl>();
        rom = GameObject.FindGameObjectWithTag("Board").GetComponent<RoomControl>();

        for (int i = 0; i < 8; i++)
        {
            slot[i] = PlayerPrefs.GetInt("Slot" + i,0);
            
        }
        money = PlayerPrefs.GetInt("Money" ,0);
      
        
       PopU("Fase"+rom.andar,4);
    }
    void Update() {
        
        if (line[0] == null) {
            tkorlv.SetActive(false);
            
        } else {
            tkorlv.SetActive(true);

            found.text ="Found: "+ListStats(found.text, line[0].id);
            
            current.text = "Current: " + ListStats(current.text, slot[itemDb.list[line[0].id].slot]);
            
        }
    }
	// Update is called once per frame
	void FixedUpdate () {

        

        if (line[0] != null)
        {
            float d = Vector3.Distance(transform.position, line[0].gameObject.transform.position);
            if (d > 1) { Trash(); }
        }
        col = Physics2D.OverlapCircle(transform.position, 0.4f, loot);

        if (col != null)
        {

            Loot g = col.gameObject.GetComponent<Loot>();
            if (g.pickable == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (line[i] == null)
                    {
                        line[i] = g;
                        print("line "+i+" id"+line[i].id);
                        g.pickable = false;
                        i = 11;
                    }
                }
            }
        }

        coly = Physics2D.OverlapCircleAll(transform.position, 0.4f, all);

        if (coly != null&&Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider2D cool in coly) { 
                if (cool.gameObject.tag == "End") {
                    End();
                        }
         }   
        }

        }
    public void Take() {
        Loot g = line[0];
        
        slot[itemDb.list[line[0].id].slot] = line[0].id;
        if (itemDb.list[line[0].id].slot == 2) rpg.bullets = 5;
        for (int i = 0; i < 9; i++)
        {
            if (line[i + 1] != null)
            {
                line[i] = line[i + 1];
            }
            else { line[i] = null; }
            
        }
        Destroy(g.gameObject);
    }
    public void Trash()
    {
        Loot g = line[0];
        for (int i = 0; i < 9; i++)
        {
            if (line[i + 1] != null)
            {
                line[i] = line[i + 1];
            }
            else { line[i] = null; }

        }
        g.pickable = true;
    }
    public string ListStats(string stat,int id) {
        stat = "\n";
       
        
        stat += itemDb.list[id].name + "\n";
        
        if (itemDb.list[id].hp != 0)stat += "hp: " + itemDb.list[id].hp + "\n";
        
        if (itemDb.list[id].dex != 0) stat += "dex: " + itemDb.list[id].dex + "\n";
        
        if (itemDb.list[id].str != 0) stat += "str: " + itemDb.list[id].str + "\n";
        
        if (itemDb.list[id].def != 0) stat += "def: " + itemDb.list[id].def + "\n";

        
        stat = stat.Replace('$', '\n');
        
        return stat;

}
    public void End()
    {
        int p = rom.andar + 1;
        PlayerPrefs.SetInt("Andar", p);
        rpg.SaveStatus();
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt("Slot" + i, slot[i]);
        }
       
        PlayerPrefs.SetInt("Money" ,money);
        cont.GoToScene("Fase" + p);
    }



///////               XML                ////////////////////////////////////////////////////////////////////
    public ItemDataBase itemDb;


    public void Awake()
    {
        Open();
        rpg = gameObject.GetComponent<PlayerRpg>();
        rpg.itemDb = itemDb;
        print("started");
    }
   




    public string GenerateText()
    {
        string t = "";
        if (itemDb.list[0] != null)
        {
            foreach (Item it in itemDb.list)
            {
                t += it.id + "- Nome: *" + it.name + "* Slot:" + it.slot + " Slot:" + it.slot + " Hp:" + it.hp + " Dex:" + it.dex + " Str:" + it.str + " Def:" + it.def + "\n";

            }

        }
        t = t.Replace('$', '\n');

        return t;
    }



    public void Open()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Xml/Items");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        Converter(xmldoc);
    }

    public void Converter(XmlDocument doc)
    {
        foreach (Item it in itemDb.list)
        {
            itemDb.list.Remove(it);
        }
        foreach (XmlNode itemd in doc.ChildNodes)
        {
            foreach (XmlNode items in itemd.ChildNodes)
            {

                foreach (XmlNode it in items.ChildNodes)
                {
                    Item item = new Item();
                    item.id = (int)float.Parse(it.ChildNodes[0].InnerText);
                    item.name = it.ChildNodes[1].InnerText;
                    item.slot = (int)float.Parse(it.ChildNodes[2].InnerText);
                    item.hp = float.Parse(it.ChildNodes[3].InnerText);
                    item.dex = float.Parse(it.ChildNodes[4].InnerText);
                    item.str = float.Parse(it.ChildNodes[5].InnerText);
                    item.def = float.Parse(it.ChildNodes[6].InnerText);
                    item.chance = (int)float.Parse(it.ChildNodes[7].InnerText);
                    itemDb.list.Add(item);
                }
                
            }
        }
    }

    
    public void PopU(string msg, float duration) {
        print("PopUp");
        pop.GetComponent<PopUp>().msg = msg;
        pop.GetComponent<PopUp>().t = duration;
        
    }

}

[System.Serializable]
public class ItemDataBase
{
    [XmlArray("Items")]
    public List<Item> list = new List<Item>();

}
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public int slot;
    public float hp;
    public float dex;
    public float str;
    public float def;
    public int chance;
}

