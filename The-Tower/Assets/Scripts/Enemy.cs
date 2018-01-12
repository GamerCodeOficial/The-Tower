using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float pct;

    public Transform trans;

    public GameObject drop;

    public int[] minMax;

    public float hp;
    public float str;
    public float dex;
    public float def;
    public float alc;

    public GameObject player;
    public PlayerRpg pRpg;

    private float t;

    public bool sPlayer;


    public Room rom;

    public int dropQuality;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pRpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
        effectDb= pRpg.gameObject.GetComponent<StatsModifiers>().effectDb;
        
    }
    private void FixedUpdate()
    {
        trans.position = transform.position;
        trans.LookAt(player.transform);


        //float d = Vector3.Distance(transform.position, player.transform.position);
        if (rom.oppened)
        { 
            transform.Translate(trans.forward * Time.deltaTime * dex);
        }

    }
    // Update is called once per frame

    void Update ( ) {
        
        if (hp <= 0) Die();

       float d = Vector3.Distance(transform.position, player.transform.position);
        if (t >= 4 / dex && d<alc)
        {
            pRpg.TakeDamage(str);
            t = 0;
        }
        t+=Time.deltaTime;

        if (ti >= 1)
        {
            foreach (int efct in effects)
            {
                hp -= effectDb.list[efct].damage;

            }
            for (int i = 0; i < duration.Count; i++)
            {
                duration[i]--;
                if (duration[i] <= 0)
                {
                    effects.Remove(effects[i]);
                    duration.Remove(duration[i]);
                }

            }
            ti = 0;
        }
        ti += Time.deltaTime;
        EffectStatus();
    }
    public void TakeDamage(float dam) {
       float a= Random.Range(0, 100);
        if (a > def)  hp -= dam; 
    }
    public void Die() {
        
        if (Random.Range(0, 100) < pct)
        {
            print("Chance");
           
            GameObject d =Instantiate(drop, transform.position, trans.rotation);
            d.GetComponent<Loot>().dropQuality = dropQuality;
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
        foreach (int efct in effects)
        {
            dex += effectDb.list[efct].dex;
            str += effectDb.list[efct].str;
            def += effectDb.list[efct].def;
        }
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

 
}


