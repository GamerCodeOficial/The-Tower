using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRpg : MonoBehaviour
{
    private Quaternion rota;
    //dir 1:direita 2:baixo 3:esquerda 4:direita
    public int direction;
    public int dir;
    //right:1 down:2 left:3 up:4
   
   
    public Sprite[] atkEsp;
    public SpriteRenderer rend;


    public float cHp;

    public float hp;
    public float dex;
    public float str;
    public float def;
    public float aura;

    public float rHp;
    public float rDex;
    public float rStr;
    public float rDef;
    public float rAura;

    public float ac;
    public Collider2D[] col;

   

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

    public void GetStatus() {
        hp=PlayerPrefs.GetFloat("Hp");
        dex = PlayerPrefs.GetFloat("Dex");
        str = PlayerPrefs.GetFloat("Str");
        def = PlayerPrefs.GetFloat("Def");
        aura = PlayerPrefs.GetFloat("Aura");

}
    public void SaveStatus()
    {
        PlayerPrefs.SetFloat("Hp",hp);
        PlayerPrefs.SetFloat("Dex", dex);
        PlayerPrefs.SetFloat("Str", str);
        PlayerPrefs.SetFloat("Def", def);
        PlayerPrefs.SetFloat("Aura", aura);
    }

    // Use this for initialization
    void Start()
    {

        GetStatus();
        cHp = hp;
        nPos = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculatStats();

        float z = PointToMouse();
        
        Vector2 atkPos = transform.position;

        
        
        
        if ((z >= 0 && z <= 45)|| (z > 315 && z <= 360)) { dir = 1;
            atkPos.x += radiusM ; 

        }
        if ((z > 45 && z <= 135)) { dir = 4;
            atkPos.y += radiusM;
        }
        if ((z > 135 && z <= 225)) { dir = 3;
            atkPos.x -= radiusM ;
        }
        if ((z > 225 && z <= 315)) { dir = 2;
            atkPos.y -= radiusM ;
        }
        
        rend.sprite = atkEsp[dir];



        


        nPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nPos.z = -1;

        aim.LookAt(nPos);
        
      

        col = Physics2D.OverlapCircleAll(atkPos, radiusM , m);


        if (cHp <= 0) Die();


        if (col != null)
        {
            
            if (t >= inv.size[inv.slot[1]] / rDex && Input.GetMouseButtonDown(0))
            {

                foreach(Collider2D cl in col){
                    cl.GetComponent<Enemy>().TakeDamage(rStr);
                    print("dam: " + rStr);
                }

            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            t = 0;
        }

        t += Time.deltaTime;

        dory = Physics2D.OverlapCircle(transform.position, raioN, d);
        if (dory != null && Input.GetKeyDown(KeyCode.E)) { dory.GetComponent<DoorControl>().Open();  }
    }



    void Update()
    {


    }

    public void TakeDamage(float dam)
    {
        float a = Random.Range(0, 100);

        if (a > rDef)
        {
            print(a + ">" + rDef + " -" + dam);
            cHp -= dam;
        }
        else
        {
            print(a + "<" + rDef + " blocked");
        }
    }
    public void Die()
    {
        print("Morreu");
        Destroy(gameObject);
    }
    public void CalculatStats() {
        rHp=hp;
        rDex=dex;
        rStr=str;
        rDef=def;
        rAura=aura;
        for (int i = 0; i < 8; i++) {

            int j = inv.slot[i];
            rHp += inv.hp[j];
            rDex += inv.dex[j];
            rStr += inv.str[j];
            rDef += inv.def[j];
            rAura += inv.aura[j];

        }
        
    }

    //Public Vars
    public Camera cam;

    //Private Vars
    private Vector3 mousePosition;

    private Vector2 mousePos;
    private Vector3 screenPos;

    public float PointToMouse()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rota = Quaternion.Euler(0f, 0f, rotation_z + 0.0f);
        return rota.eulerAngles.z;

    }


}
