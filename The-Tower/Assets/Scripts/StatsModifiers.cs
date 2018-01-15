using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class StatsModifiers : MonoBehaviour {
    public EffectDb effectDb;
    public List<int> effects = new List<int>();
    public List<int> duration = new List<int>();
    public PlayerRpg rpg;


    public Sprite[] img;
    public GameObject efctImg;

    public float t;

	// Use this for initialization
	void Start () {
        img = Resources.LoadAll<Sprite>("Graphics/Icons/EffectIcons");
        rpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
        Open();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (t >= 1)
        {
            foreach (int efct in effects)
            {
                rpg.cHp -= effectDb.list[efct].damage;
                for (int i = 0; i < 4; i++)
                {
                    GameObject p = Instantiate(efctImg, transform.position, transform.rotation);
                    p.GetComponent<EffectAnim>().rend.sprite = img[efct];
                }
            }
            for (int i = 0; i < duration.Count; i++)
            {
                duration[i] -= 1;
                if (duration[i] <= 0)
                {
                    effects.Remove(effects[i]);
                    duration.Remove(duration[i]);
                }

            }
            t = 0;
        }
        t += Time.deltaTime;
        Add();
    }

    public void Add() {
        foreach (int efct in effects)
        {
            rpg.rDex += effectDb.list[efct].dex;
            rpg.rStr += effectDb.list[efct].str;
            rpg.rDef += effectDb.list[efct].def;
        }
    }


    public void AddEffect(int ef) {
        for (int i = 0; i < effects.Count; i++)
            if (effects[i] == ef)
            {
                duration[i] = effectDb.list[ef].duration;
                return;
            }

        effects.Add(ef);
        duration.Add(effectDb.list[ef].duration);
    }

    public void Open()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Xml/Effects");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        Converter(xmldoc);
    }

    public void Converter(XmlDocument doc)
    {
        foreach (Effect it in effectDb.list)
        {
            effectDb.list.Remove(it);
        }
        foreach (XmlNode itemd in doc.ChildNodes)
        {
            foreach (XmlNode items in itemd.ChildNodes)
            {

                foreach (XmlNode it in items.ChildNodes)
                {
                    Effect item = new Effect();
                    item.id = (int)float.Parse(it.ChildNodes[0].InnerText);
                    item.name = it.ChildNodes[1].InnerText;
                    item.dex = float.Parse(it.ChildNodes[2].InnerText);
                    item.str = float.Parse(it.ChildNodes[3].InnerText);
                    item.def = float.Parse(it.ChildNodes[4].InnerText);
                    item.damage = float.Parse(it.ChildNodes[5].InnerText);
                    item.duration = (int)float.Parse(it.ChildNodes[6].InnerText);
                    
                    effectDb.list.Add(item);
                }

            }
        }
    }
}
[System.Serializable]
public class EffectDb {
    [XmlArray("Effects")]
    public List<Effect> list = new List<Effect>();
}
[System.Serializable]
public class Effect{
    public int id;
    public string name;
    public float dex;
    public float str;
    public float def;
    public float damage;
    public int duration;
   
}
