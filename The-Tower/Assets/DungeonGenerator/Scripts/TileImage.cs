using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileImage : MonoBehaviour {
    public Sprite sprite;
    public SpriteRenderer rend;
	// Use this for initialization
	void Start () {
        rend.sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
