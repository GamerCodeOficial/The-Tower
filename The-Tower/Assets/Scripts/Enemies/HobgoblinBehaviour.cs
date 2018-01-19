using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobgoblinBehaviour : MonoBehaviour
{
    public Enemy en;
    public float atkRange;
    public float size;
    public float giveUpRange;
    public float wlkDuration;
    public float spDuration;

    public float wlkTime;
    public float atkTime;
    public float spTime;

    public float damage;
    public float spDamage;
    public float atkDuration;

    public float speed;

    public int dir;

    public Vector3 target;

    public float coolDown;

    // Use this for initialization
    void Start()
    {
        coolDown = 5;
        en = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        float d = en.disToPlayer;
        if (en.rom.oppened && d < giveUpRange)
        {
            if (coolDown <= 0)
            {
                coolDown = 12;
                target = en.player.transform.position - transform.position;
                spTime = 0.01f;
            }
            if (d < atkRange - 0.2f)
            {
                print("Menor");
                if (atkTime == 0)
                {
                    atkTime = 0.01f;
                }
            }
            else if (atkTime == 0)
            {
                en.Walk(size, speed);
                if (wlkTime == 0) wlkTime = 0.01f;
            }



            if (spTime > 0)
            {
                print("Duration: " + spDuration);
                Special(spDuration);
            }
            else if (atkTime > 0)
            {
                Attack(atkDuration);
            }
            else if (wlkTime > 0)
            {
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
            coolDown -= Time.deltaTime;
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


        atkTime += Time.deltaTime;
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

    public void Special(float maxTim) {
        atkTime = 0;
        wlkTime = 0;

        if (spTime > 1){
            target.Normalize();
            transform.Translate(target * Time.deltaTime * speed * 0.8f);
        }

        if (en.disToPlayer < atkRange - 0.2f) {
            en.pRpg.TakeDamage(spDamage);
    
            spTime = 0;
            return;
        }

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

        int p = (int)(((spTime / maxTim) * (max - min)) + min);
        if (p > max - 1) p = max - 1;

        ChangeSprite(p);

        if (spTime >= maxTim)
        {
            spTime = 0; 
            return;
        }

        spTime += Time.deltaTime;

    }
    public void ChangeSprite(int index)
    {
        if(index>=0&&index<en.enAnim.Length)
        en.rend.sprite = en.enAnim[index];

    }

}
