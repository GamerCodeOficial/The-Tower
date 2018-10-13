using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject player;
    public float speed;
    public int damage;
    public float range;
    public float coolDown;
    private float t;

    public GameObject slash;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 dir = player.transform.position - transform.position;
        dir /= dir.magnitude;
        transform.Translate(dir*speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) < range) {
            t -= Time.deltaTime;
            if (t <= 0){
                player.GetComponent<PlayerRPG>().TakeDamage(damage);
                Instantiate(slash, transform.position, Quaternion.identity);
                t = coolDown;
            }

        }
        else {
            t = coolDown;
        }
	}
}
