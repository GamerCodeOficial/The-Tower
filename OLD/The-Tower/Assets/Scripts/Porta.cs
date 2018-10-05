using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour {

    public Vector2 destino;
    public Vector3 camPos;
    public GameObject cam;

    public bool counting;
    public float t;

    public GameObject player;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (counting == true) t += Time.deltaTime;

        if (t > 0.2f)
        {
            player.gameObject.transform.position = destino;
            cam.transform.position = camPos;
            t = 0;
            counting = false;
        }
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            player = col.gameObject;
            cam.transform.position = new Vector3(-1000, -1000, -10);
            counting = true;
           
        }
    } 
}
