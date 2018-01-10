using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public Enemy en;
    public GameObject end;
    public bool cut;
    public string bName;
	// Use this for initialization
	void Start () {
        cut = false;
        end = GameObject.FindGameObjectWithTag("End");
	}
	
	// Update is called once per frame
	void Update () {
        if (en.rom.oppened&&!cut) {
            en.pRpg.inv.PopU(bName,3);
            cut = true;
        }
         end.GetComponent<CircleCollider2D>().enabled = (!en);
	}
}
