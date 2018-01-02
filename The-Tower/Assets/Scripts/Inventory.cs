using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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


    public string[] iName;
    public int[] iSlot;
    public string[] iType;
    public int[] iValue;
    public int[] size;

    public float[] hp;
    public float[] dex;
    public float[] str;
    public float[] def;
    public float[] aura;
    


    public Loot[] line;


    public int[] slot; //0 null  1 Weapon  2 Secondary  3 Armor  4 Other 5 potions 6 head 7 feet

    public PlayerRpg rpg;
        

    

    // Use this for initialization
    void Start () {
        rpg = GameObject.FindGameObjectWithTag("RPG").GetComponent<PlayerRpg>();
        cont = GameObject.FindGameObjectWithTag("Control").GetComponent<SceneControl>();
        rom = GameObject.FindGameObjectWithTag("Board").GetComponent<RoomControl>();

        for (int i = 0; i < 8; i++)
        {
            slot[i] = PlayerPrefs.GetInt("Slot" + i,0);
            
        }
        money = PlayerPrefs.GetInt("Money" ,0);
    }
    void Update() {
        
        if (line[0] == null) {
            tkorlv.SetActive(false);
            
        } else {
            tkorlv.SetActive(true);

            found.text ="Found: "+ListStats(found.text, line[0].id);
            
            current.text = "Current: " + ListStats(current.text, slot[iSlot[line[0].id]]);
            
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
        slot[iSlot[line[0].id]] = line[0].id;
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
        stat += iName[id]+"\n";
        
        if(hp[id]!=0)stat += "hp: " + hp[id] + "\n";

        if (dex[id] != 0) stat += "dex: " + dex[id] + "\n";

        if (str[id] != 0) stat += "str: " + str[id] + "\n";

        if (def[id] != 0) stat += "def: " + def[id] + "\n";

        if (aura[id] != 0) stat += "aura: " + aura[id];
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
        ////
        PlayerPrefs.SetInt("Money" ,money);
        cont.GoToScene("Fase" + p);
    }

}
