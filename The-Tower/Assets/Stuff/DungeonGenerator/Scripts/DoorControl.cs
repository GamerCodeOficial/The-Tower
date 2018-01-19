using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {
    public int dir;
    public Room rom;
    public GameObject player;
    public bool oppened;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ReSize();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.SetActive(!oppened);
	}
    public void ReSize() {
        if (dir == 1 || dir == 3) {
            Vector3 scale = transform.localScale;
            scale.y = 4;
            scale.x = 1;
            transform.localScale = scale;
        }
        if (dir == 2 || dir == 4)
        {
            Vector3 scale = transform.localScale;
            scale.y = 1;
            scale.x = 4;
            transform.localScale = scale;
        }
    }
    public void Open() {
        oppened = true;
        rom.oppened = true;
    }
}
