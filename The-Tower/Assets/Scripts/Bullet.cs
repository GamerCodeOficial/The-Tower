using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;
    public float damage;
    public float t;
    public LayerMask en;
    // Use this for initialization
    void Start () {
        speed = Random.Range(6,8);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right*speed*Time.deltaTime);

       if(damage>5) damage -= Time.deltaTime * 10;

        t += Time.deltaTime;
        if (t > 5) Destroy(gameObject);
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.15f, en);
        if (col != null) {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
   
}
