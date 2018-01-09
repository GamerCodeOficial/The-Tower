using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp : MonoBehaviour {
    public string msg;
    public float duration;

    public Text message;

    public float t;

    public GameObject panel;

	// Use this for initialization
	void Start () {     
     
	}
	
	// Update is called once per frame
	void Update () {
        t -= Time.unscaledDeltaTime;
        message.text = msg;
        if (t > 0) { panel.SetActive(true); print("T>0"); Time.timeScale = 0; }
        else {
            panel.SetActive(false); Time.timeScale = 1;
        }
        
	}
}
