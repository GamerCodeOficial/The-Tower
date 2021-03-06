﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerRpg rpg;
    public Inventory inv;
    public float speed;


	// Use this for initialization
	void Start () {
        Time.timeScale = 0f;
        rpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
	}
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (rpg.atkTime==0) {
            Vector3 dir = (Vector3.right * h) + (Vector3.up * v);
            transform.Translate(dir * Time.deltaTime * rpg.rDex/1.5f );
        }
        if (v != 0) {
            if (v > 0)
            {
                rpg.direc = 4;
            }
            else {
                rpg.direc = 2;
            }
        }
        else if (h > 0)
        {
            rpg.direc = 1;
        }
        else if(h<0) {
            rpg.direc = 3;
        }
        
        if (h != 0 || v != 0&& rpg.rollTime==0&&rpg.atkTime==0)
        {
            if (rpg.wlkTime == 0) rpg.wlkTime = 0.01f;
          
        }
        else {
            
            rpg.wlkTime = 0;
        }

    }
}
