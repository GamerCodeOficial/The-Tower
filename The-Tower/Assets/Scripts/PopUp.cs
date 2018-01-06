using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp : MonoBehaviour {
    public string msg;
    public float duration;

    public Text message;

    private float t;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        message.text = msg;
        t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (t > duration) { Destroy(gameObject); Time.timeScale = 1; }
        t += Time.unscaledDeltaTime;
	}
}
