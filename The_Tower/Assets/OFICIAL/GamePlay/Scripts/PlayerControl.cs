using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    public float dashSpeed;
    public float range;
    public float atkSize;
    float x = 0;
    float y = 0;
    public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }
    void FixedUpdate () {
        rb.velocity = new Vector2(x, y);
        rb.velocity *= speed / Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }
    void Attack() {
        Vector2 v = new Vector2(x, y);
        v /= v.magnitude;
        Vector2 pos = transform.position;
        RaycastHit2D[] objs = Physics2D.CircleCastAll((pos + v) * range, atkSize, v);
        foreach (RaycastHit2D o in objs) {
            print(o.collider.gameObject.name);
        }
    }
    void Dash() {
        print("Dash");
        Vector2 v = new Vector2(x, y);
        v /= v.magnitude;
        v *= dashSpeed;
        rb.AddForce(v);
    } 
}
