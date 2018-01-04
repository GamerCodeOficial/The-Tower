using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTest : MonoBehaviour {
    public Sprite[] test;
    public string path;
	// Use this for initialization
	void Awake () {
       
        test = Resources.LoadAll<Sprite>(path);
        print(path);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
