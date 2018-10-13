using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public PlayerRPG rpg;
    
    public float speed;
    public float dashSpeed;
    public float range;
    public float atkSize;
    public Vector2 direc;
    float x = 0;
    float y = 0;

    public Rigidbody2D rb;

    public GameObject slash;

	// Use this for initialization
	void Start () {
        rpg = gameObject.GetComponent<PlayerRPG>();
	}

    // Update is called once per frame
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        FindDirection();

        if (Input.GetKeyDown(KeyCode.Space)) Dash();
        if (Input.GetMouseButtonDown(0)) Attack();
    }
    void FixedUpdate () {
        rb.velocity = new Vector2(x, y);
        rb.velocity *= speed / Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }
    void FindDirection() {
        float h = 0;
        float v = 0;
        if (x > 0) h = 1;
        if (x < 0) h = -1;
        if (y > 0) v = 1;
        if (y < 0) v = -1;
        Vector2 nD = direc;

        if (h != 0) {
            nD.x = h;
            if (v == 0) nD.y = 0;
        }
        if (v != 0){
            nD.y = v;
            if (h == 0) nD.x = 0;
        }
        direc = nD;
    }
    void Attack() {
        print("Attack");
        Instantiate(slash, transform.position, Quaternion.identity);
        Vector2 pos = transform.position;
        RaycastHit2D[] objs = Physics2D.CircleCastAll(pos, atkSize, direc, range);
        foreach (RaycastHit2D o in objs) {
            
            if (o.collider.gameObject.GetComponent<Destructable>()) {
                
                o.collider.gameObject.GetComponent<Destructable>().TakeDamage(rpg.damage);
            }
        }
    }
    void Dash() {
        print("Dash");
        Vector2 v = new Vector2(x, y);
        if (v.magnitude != 0) { 
        v /= v.magnitude;
        v *= dashSpeed;
        rb.AddForce(v);
        }
    } 
}
