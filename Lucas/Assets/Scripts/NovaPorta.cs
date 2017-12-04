using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaPorta : MonoBehaviour {


    public bool openned;

    public GameObject player;

    public Color aber;
    public Color fec;

    public SpriteRenderer rend;
    public BoxCollider2D col;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        float d = Vector3.Distance(transform.position, player.transform.position);
        if (d < 2 && Input.GetKeyDown(KeyCode.E)) {
            if (openned == true) { openned = false; }
            else
            {
                openned = true;
            }
        }
        if (openned == true) {
            col.enabled = false;
            rend.color = aber;
        } else {
            col.enabled = true;
            rend.color = fec;
        }
    }
   
}
