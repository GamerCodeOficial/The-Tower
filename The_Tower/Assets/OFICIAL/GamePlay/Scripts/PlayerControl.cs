using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    float x = 0;
    float y = 0;
    Vector2 velocity; 
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        velocity = new Vector2(x, y);
        if (velocity.magnitude != 0) velocity /= velocity.magnitude;

    }
    void FixedUpdate () {

        transform.Translate(velocity * speed * Time.deltaTime);
        
    }
}
