using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehaviour : MonoBehaviour {
    public Enemy en;
    public float atkRange;
    public float size;
    public float giveUpRange;

    public float atkTime;

    public float damage;
    public float atkDuration;

    public float speed;
	// Use this for initialization
	void Start () {
        en = gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
        float d = en.disToPlayer;
        if (en.rom.oppened && d < giveUpRange) {

            if (d < atkRange)
            {
                if (atkTime==0) {
                    atkTime = 0.01f;
                }
            }
            else if(atkTime==0){
                en.Walk(size,speed);
            }
           
        }

         if (atkTime > 0) {
                Attack();
            }
	}
    public void Attack()
    {
        if (atkTime>=atkDuration) {
            atkTime = 0;
            if (en.disToPlayer < atkRange)
            {
                en.player.GetComponent<PlayerRpg>().TakeDamage(damage);
            }
            return;
        }
        atkTime += Time.deltaTime;
    }
}
