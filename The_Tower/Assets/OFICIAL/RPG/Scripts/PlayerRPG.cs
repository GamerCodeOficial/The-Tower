using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : MonoBehaviour {
    public int damage;
    public int hp;

    public GameObject blood;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(int damage) {
        hp -= damage;
        Instantiate(blood, transform.position, Quaternion.identity);
        if (hp <= 0) {
            print("Dead");
        }
    }
}
