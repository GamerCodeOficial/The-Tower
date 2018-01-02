using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public Enemy en;
    public GameObject end;
	// Use this for initialization
	void Start () {
        end = GameObject.FindGameObjectWithTag("End");
	}
	
	// Update is called once per frame
	void Update () {
         end.GetComponent<CircleCollider2D>().enabled = (!en);
	}
}
