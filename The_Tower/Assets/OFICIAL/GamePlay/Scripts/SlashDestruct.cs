using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDestruct : MonoBehaviour {
    public float t=0;
    public float duration;

	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t >= duration) Destroy(gameObject);
	}
}
