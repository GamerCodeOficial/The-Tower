using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehaviour : MonoBehaviour {
    public Enemy en;
    public float atkRange;
    public float size;
    public float giveUpRange;
    public float wlkDuration;

    public float wlkTime;
    public float atkTime;

    public float damage;
    public float atkDuration;

    public float speed;

    public int dir; 


	// Use this for initialization
	void Start () {
        en = gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
        float d = en.disToPlayer;
        if (en.rom.oppened && d < giveUpRange) {

            if (d < atkRange-0.1f)
            {
               
                if (atkTime==0) {
                    atkTime = 0.01f;
                }
            }
            else if(atkTime==0){
                en.Walk(size,speed);
                if (wlkTime == 0) wlkTime = 0.01f;
            }
           
        }

        if (atkTime > 0)
        {
            Attack(atkDuration);
        }
        else if(wlkTime>0) {
            Walk(wlkDuration);
        }

        float z = PointToPlayer();
        if ((z >= 0 && z <= 45) || (z > 315 && z <= 360))
        {
            dir = 1;
           

        }
        if ((z > 45 && z <= 135))
        {
            dir = 4;
            
        }
        if ((z > 135 && z <= 225))
        {
            dir = 3;
            
        }
        if ((z > 225 && z <= 315))
        {
            dir = 2;
            
        }


    }

   
    public float PointToPlayer()
    {
        Vector3 difference = en.player.transform.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rota = Quaternion.Euler(0f, 0f, rotation_z + 0.0f);
        return rota.eulerAngles.z;
    }
    public void Walk(float maxTim)
    {
        int min = new int();
        int max = new int();
        if (dir == 1)
        {
            min = 19; max = 24;
        }
        if (dir == 2)
        {
            min = 1; max = 5;
        }
        if (dir == 3)
        {
            min = 13; max = 18;
        }
        if (dir == 4)
        {
            min = 7; max = 12;
        }
        wlkTime += Time.deltaTime;
        int p = (int)(((wlkTime / maxTim) * (max - min)) + min);
        if (p < min) p = min;
        if (p > max - 1) p = max - 1;
        ChangeSprite(p);
 
        if (wlkTime >= maxTim)
        {
                wlkTime = 0;
        }
    }

    public void Attack(float maxTim)
    {
        atkTime += Time.deltaTime;
            int min = 0;
            int max = 0;

            wlkTime = 0;


            if (dir == 1)
            {
                min = 32; max = 36;

            }
            if (dir == 2)
            {
                min = 24; max = 28;

            }
            if (dir == 3)
            {
                min = 36; max = 40;

            }
            if (dir == 4)
            {
                min = 28; max = 32;

            }


            
            int p = (int)(((atkTime / maxTim) * (max - min)) + min);

            if (p > max - 1) p = max - 1;

            ChangeSprite(p);

            if (atkTime >= maxTim)
            {
                atkTime = 0;
            if (en.disToPlayer < atkRange)
            {
                en.player.GetComponent<PlayerRpg>().TakeDamage(damage);
            }
            return;
        }

    }

    public void ChangeSprite(int index)
    {
        en.rend.sprite = en.enAnim[index];

    }

}
