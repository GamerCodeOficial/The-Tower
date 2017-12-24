using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float pct;

    public Transform trans;

    public GameObject drop;
    

    public float hp;
    public float str;
    public float dex;
    public float def;
    public float alc;

    public GameObject player;
    public PlayerRpg pRpg;

    private float t;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        pRpg = GameObject.FindGameObjectWithTag("RPG").GetComponent<PlayerRpg>();
    }
    private void FixedUpdate()
    {
        trans.position = transform.position;
        trans.LookAt(player.transform);


        float d = Vector3.Distance(transform.position, player.transform.position);
        if (d < 30)
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
    }
    public void TakeDamage(float dam) {
       float a= Random.Range(0, 100);
        if (a > def)  hp -= dam; 
    }
    public void Die() {
        
        if (Random.Range(0, 100) < pct)
        {
           
            Instantiate(drop, transform.position, trans.rotation);
        }
       
        Destroy(gameObject);
    }
}
