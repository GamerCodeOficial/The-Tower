﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float disToPlayer;
    public LayerMask wall;

    public float pct;

    public Transform trans;

    public GameObject drop;

    public int[] minMax;

    public float hp;

    public float str;
    public float dex;
    public float def;

    public float cHp;

    public float rStr;
    public float rDex;
    public float rDef;

    public float alc;

    public GameObject player;
    public PlayerRpg pRpg;

    private float t;

    public bool sPlayer;


    public Room rom;

    public int dropQuality;

    public Sprite[] img;
    public GameObject efctImg;

    public SpriteRenderer rend;

    public Sprite[] enAnim;
    public string link;

    public bool awaked;
    public float aT;

    public float redT;

    // Use this for initialization
    void Start () {

        cHp = hp;
        img = Resources.LoadAll<Sprite>("Graphics/Icons/EffectIcons");
        player = GameObject.FindGameObjectWithTag("Player");
        enAnim = Resources.LoadAll<Sprite>("Graphics/Enemies/"+link);
        pRpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
        effectDb= pRpg.gameObject.GetComponent<StatsModifiers>().effectDb;
        
    }
    private void FixedUpdate()
    {
      disToPlayer= Vector3.Distance(transform.position, player.transform.position);
    

    }
    // Update is called once per frame

    void Update ( ) {
        if (rom.oppened && !awaked) {
            aT += Time.deltaTime;
           if(aT>0.4f) awaked = true;
        }
        if (cHp <= 0) Die();
        
            if (ti >= 1)
        {
            foreach (int efct in effects)
            {
                cHp -= effectDb.list[efct].damage;
                for (int i = 0; i < 4; i++)
                {
                    GameObject p = Instantiate(efctImg, transform.position, transform.rotation);
                    p.GetComponent<EffectAnim>().rend.sprite = img[efct];
                }
            }
            for (int i = 0; i < duration.Count; i++)
            {
                duration[i]-=1;
                if (duration[i] <= 0)
                {
                    effects.Remove(effects[i]);
                    duration.Remove(duration[i]);
                }

            }
            ti = 0;
        }
        if (redT > 0)
        {
            rend.color = new Color(255, 30, 30, 0.8f);
            print("Red");
            redT -= Time.deltaTime;
        }
        else {
            rend.color = new Color(255, 255, 255, 1);
        }
        ti += Time.deltaTime;
        EffectStatus();
    }
    public void TakeDamage(float dam) {
       float a= Random.Range(0, 100);
        if (a > rDef) {
            cHp -= dam;
            redT = 0.1f;
        } 
    }
    public void Die() {
        
        if (Random.Range(0, 100) < pct)
        {
            print("Chance");
            if (Random.Range(0, 100) > 20)
            {
                GameObject d = Instantiate(drop, transform.position, trans.rotation);
                d.GetComponent<Loot>().dropQuality = dropQuality;
            }
            else {
                int c= pRpg.mod.effectDb.list.Count;
                int p = Random.Range(1, c);
                pRpg.inv.AddUsable(p);
                
            }

        }
        int r = Random.Range(minMax[0],minMax[1]);
       
        player.GetComponent<Inventory>().money += r;
        
        Destroy(gameObject);
    }


    public float ti;

    public EffectDb effectDb;
    public List<int> effects = new List<int>();
    public List<int> duration = new List<int>();
    public PlayerRpg rpg;


    public void EffectStatus()
    {
        rDex = dex;
        rStr = str;
        rDef = def;
        foreach (int efct in effects)
        {
           
            rDex += effectDb.list[efct].dex;
            rStr += effectDb.list[efct].str;
            rDef += effectDb.list[efct].def;
        }
        if (rDex < 0) rDex = 0;
        if (rStr < 0) rStr = 0;
        if (rDef < 0) rDef = 0;
    }

    public void AddEffect(int ef)
    {
        print("ef: "+ef);
        print("count: "+effects.Count);
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i] == ef)
            {
                print("igual eff i:" + effects[i] + " ef: " + ef);
                print("i: "+i);
                print("coiso: " + effectDb.list[ef].duration);
                print("Dur: " + duration[i]);
                duration[i] = effectDb.list[ef].duration;
                return;
            }
        }
        effects.Add(ef);
        duration.Add(effectDb.list[ef].duration);
    }
    public void Walk(float size,float speed) {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position,size,wall);
        if(col!=null)
        foreach (Collider2D c in col) {
                Vector3 dif = c.transform.position - transform.position;
                dif.Normalize();
                transform.Translate(dif* Time.deltaTime * speed*-1);
                
            }
        Vector3 df = player.transform.position - transform.position;
        df.Normalize();
        transform.Translate(df * Time.deltaTime * speed);

    }
 
}


