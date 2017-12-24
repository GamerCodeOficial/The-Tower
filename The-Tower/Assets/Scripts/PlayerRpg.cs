using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRpg : MonoBehaviour
{
    //dir 1:direita 2:baixo 3:esquerda 4:direita
    public int direction;
    public int dir;
    //right:1 down:2 left:3 up:4
   
   
    public Sprite[] atkEsp;
    public SpriteRenderer rend;
    

    public float hp;
    public float dex;
    public float str;
    public float def;

    public float ac;
    public Collider2D col;

   

    public float radiusM;
    public LayerMask m;

    public float raioN;
    public LayerMask d;
    public Collider2D dory;

    public Vector3 nPos;

    public Transform hit;
    public Transform aim;

    public float t;

    public Inventory inv;



    // Use this for initialization
    void Start()
    {
        nPos = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rX = aim.rotation.x;
        float rY = aim.rotation.y;
        //print(rX+" "+rY);
        Vector2 atkPos= transform.position;
        if (rY > 0 && rX >= -0.27 && rX <= 0.27f) { dir = 1;
            atkPos.x += radiusM ; 

        }
        if (rX < -0.27) { dir = 4;
            atkPos.y -= radiusM * 2;
        }
        if (rY < 0 && rX >= -0.27 && rX <= 0.27f) { dir = 3;
            atkPos.x -= radiusM ;
        }
        if (rX > 0.27) { dir = 2;
            atkPos.y += radiusM*2 ;
        }

        rend.sprite = atkEsp[dir];



        def = inv.iValue[inv.slot[3]];


        nPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nPos.z = -1;

        aim.LookAt(nPos);
        if (dir == 2 || dir == 4)
        {
            float p = radiusM * 2;
        }
        else {
            float p = radiusM;
        }
        

        col = Physics2D.OverlapCircle(atkPos, radiusM , m);


        if (hp <= 0) Die();


        if (col != null)
        {
            
            if (t >= inv.size[inv.slot[1]] / dex && Input.GetMouseButtonDown(0))
            {
                

                col.GetComponent<Enemy>().TakeDamage(str + inv.iValue[inv.slot[1]]);

            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            t = 0;
        }

        t += Time.deltaTime;

        dory = Physics2D.OverlapCircle(transform.position, raioN, d);
        if (dory != null && Input.GetKeyDown(KeyCode.E)) { dory.GetComponent<DoorControl>().Open(); print("ABBRE"); }
    }



    void Update()
    {


    }

    public void TakeDamage(float dam)
    {
        float a = Random.Range(0, 100);

        if (a > def)
        {
            print(a + ">" + def + " -" + dam);
            hp -= dam;
        }
        else
        {
            print(a + "<" + def + " blocked");
        }
    }
    public void Die()
    {
        print("Morreu");
        Destroy(gameObject);
    }

}
