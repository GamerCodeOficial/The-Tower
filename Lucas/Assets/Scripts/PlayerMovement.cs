using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerRpg rpg;
    public Inventory inv;
    public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
	}
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*Time.deltaTime*rpg.dex*h);
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * Time.deltaTime * rpg.dex * v);
    }
}
