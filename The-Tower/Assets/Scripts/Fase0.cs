using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase0 : MonoBehaviour {
    public SceneControl cont;
	// Use this for initialization
	void Start () {
        cont = GameObject.FindGameObjectWithTag("Control").GetComponent<SceneControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame() {
        cont.GoToScene("Fase1");
    }
}
