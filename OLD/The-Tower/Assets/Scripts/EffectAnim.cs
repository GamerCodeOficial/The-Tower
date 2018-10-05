using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnim : MonoBehaviour {
    public SpriteRenderer rend;
    private float t;
	// Use this for initialization
	void Start () {
        rend = gameObject.GetComponent<SpriteRenderer>();
        Vector3 pos=transform.position;
        pos.x += Random.Range(-0.4f,0.4f);
        pos.y += Random.Range(-0.4f, 0.4f);
        transform.position = pos;
    }
	
	// Update is called once per frame
	void Update () {
       
        transform.Translate(Vector3.up*Time.deltaTime*1.3f);
        if (t > 1) Destroy(gameObject);
        t += Time.deltaTime;
    }
}
