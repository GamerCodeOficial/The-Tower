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
        PlayerPrefs.SetInt("Hp", 20);
        PlayerPrefs.SetInt("Dex", 5);
        PlayerPrefs.SetInt("Str", 5);
        PlayerPrefs.SetInt("Def", 0);
        PlayerPrefs.SetInt("Aura", 0);
        PlayerPrefs.SetInt("Andar", 1);

        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt("Slot" + i,0);
        }

        cont.GoToScene("Fase1");
    }
}
