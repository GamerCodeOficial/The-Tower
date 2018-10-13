using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {
    public int hp;
    public GameObject blood;
    public void TakeDamage(int damage) {
        hp -= damage;
        Instantiate(blood, transform.position, Quaternion.identity);
        if (hp <= 0) Destroy(gameObject);
    }
}
