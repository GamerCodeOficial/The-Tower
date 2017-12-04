using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRpg : MonoBehaviour {
    public float hp;
    public float dex;
    public float str;
    public float def;

    public float ac;
    public Collider2D col;

    public float radiusM;
    public LayerMask m;

    public Vector3 nPos;

    public Transform hit;
    public Transform aim;

    public float t;

    public Inventory inv;

   

    // Use this for initialization
    void Start () {
        nPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        def = inv.iValue[inv.slot[3]];


        nPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nPos.z = -1;
        
        aim.LookAt(nPos);
      
        col =Physics2D.OverlapCircle(hit.position, radiusM+( inv.size[inv.slot[1]]*0.1f), m);

        if (hp <= 0) Die();
        

        if (col != null)
        {
            if (t >=  inv.size[inv.slot[1]] / dex  && Input.GetMouseButtonDown(0))
            {
                print("dam"+ (inv.iValue[inv.slot[1]]+str));

                col.GetComponent<Enemy>().TakeDamage(str+inv.iValue[inv.slot[1]]);
                
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            t = 0;
        }

        t += Time.deltaTime;
    }

    public void TakeDamage(float dam)
    {
        float a = Random.Range(0, 100);
        
        if (a > def) {
            print(a + ">" + def + " -"+dam);
            hp -= dam;
        }
        else{
            print(a + "<" + def + " blocked");
        }
    }
    public void Die() {
        print("Morreu");
        Destroy(gameObject);
    }

}
