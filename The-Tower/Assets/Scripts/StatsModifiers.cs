using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class StatsModifiers : MonoBehaviour {
    public EffectDb effectDb;
    public EffectDb effect;
    public PlayerRpg rpg;


	// Use this for initialization
	void Start () {
        rpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Add() {
        foreach (Effect efct in effectDb.list)
        {
            rpg.rDex += efct.dex;
            rpg.rStr += efct.str;
            rpg.rDef += efct.def;
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
}
