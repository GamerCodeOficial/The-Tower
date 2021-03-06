﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class PlayerRpg : MonoBehaviour
{
    public bool firstW;

    public StatsModifiers mod;

    private Quaternion rota;
    //dir 1:direita 2:baixo 3:esquerda 4:direita
    public int direction;
    public int dir;
    //right:1 down:2 left:3 up:4

    public int bullets;
   
    
    public SpriteRenderer rend;
   

    public float cHp;

    public float hp;
    public float dex;
    public float str;
    public float def;


    public float rHp;
    public float rDex;
    public float rStr;
    public float rDef;


    public float ac;
    public Collider2D[] col;

   

    public float radiusM;
    public LayerMask m;

    public float raioN;
    public LayerMask d;
    public Collider2D dory;

    public Vector3 nPos;

    public float t;

    public Inventory inv;

    public SpriteRenderer render;
    public SpriteRenderer head;
    public SpriteRenderer chest;
    public SpriteRenderer feet;
    public SpriteRenderer pri;
    public SpriteRenderer sec;

    public float wlkTime;
    public float atkTime;
    public float rollTime;
    public Sprite[] plImg;
    public Sprite[] headImg;
    public Sprite[] chestImg;
    public Sprite[] feetImg;
    public Sprite[] priImg;
    public Sprite[] secImg;

    public Sprite[] atkImg;

    public int direc;

    public float atkDuration;
    public float walkDuration;
    public float rollDuration;

    public GameObject bullet;

    float z = new float();

    public GameObject aim;


    public void GetStatus() {
        hp=PlayerPrefs.GetFloat("Hp");
        dex = PlayerPrefs.GetFloat("Dex");
        str = PlayerPrefs.GetFloat("Str");
        def = PlayerPrefs.GetFloat("Def");
}
    public void SaveStatus()
    {
        PlayerPrefs.SetFloat("Hp",hp);
        PlayerPrefs.SetFloat("Dex", dex);
        PlayerPrefs.SetFloat("Str", str);
        PlayerPrefs.SetFloat("Def", def);
        
    }

    public Color GetColor(string cor)
    {
        int[] oi = new int[3];
        char[] c = cor.ToCharArray();
        string t = "";
        for (int i = 0; i < 3; i++)
        {
            t += c[i];
        }
        int.TryParse(t, out oi[0]);
        t = "";
        for (int i = 3; i < 6; i++)
        {
            t += c[i];
        }
        int.TryParse(t, out oi[1]);
        t = "";
        for (int i = 6; i < 9; i++)
        {
            t += c[i];
        }
        int.TryParse(t, out oi[2]);

        Color co = new Color();
        float[] norm = new float[3];
        for (int i = 0; i < 3; i++)
        {
            norm[i] = oi[i];
            norm[i] /= 255;
        }
        co.r = norm[0];
        co.g = norm[1];
        co.b = norm[2];
        co.a = 1;
        return co;
    }



    // Use this for initialization
    void Start()
    {
        atkImg = Resources.LoadAll<Sprite>("Graphics/Player/Ataques");

        firstW = true;
        plImg = Resources.LoadAll<Sprite>("Graphics/Player/Aventureiro");
        priImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[1]);
        secImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[2]);
        headImg = Resources.LoadAll<Sprite>("Graphics/Player/"+inv.slot[4]);
        chestImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[5]);
        feetImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[6]);
        mod = gameObject.GetComponent<StatsModifiers>();
        GetStatus();
        CalculatStats();
        cHp = rHp;
        nPos = Vector3.zero;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Quaternion rot = new Quaternion();

        rot = Quaternion.Euler(0f, 0f, z + 0.0f);
        aim.transform.rotation = rot;

        Color c = GetColor(PlayerPrefs.GetString("Cor"));

        render.color = c;


        CalculatStats();
        Vector2 atkPos = transform.position;
        if (atkTime == 0)
        {
            z = PointToMouse(); 
        }
        
        
        
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
        
     


        if (Input.GetKeyDown(KeyCode.Space)&&rollTime==0) {
            
            rollTime = 0.01f;
        }
        


        nPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nPos.z = -1;

        
        
      

        col = Physics2D.OverlapCircleAll(atkPos, radiusM , m);


        if (cHp <= 0) Die();


        t += Time.deltaTime;

        dory = Physics2D.OverlapCircle(transform.position, raioN, d);
        if (dory != null && Input.GetKeyDown(KeyCode.E)) { dory.GetComponent<DoorControl>().Open();  }
      
        
    }


    public void StartAtack() {
        if (atkTime == 0) atkTime = 0.01f;
    }
    void Update()
    {
        priImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[1]);
        secImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[2]);
        headImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[4]);
        chestImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[5]);
        feetImg = Resources.LoadAll<Sprite>("Graphics/Player/" + inv.slot[6]);
        ChangeAtk(16);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            firstW = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            firstW = false;
        }
        if (atkTime > 0)
        {
            Ataque(atkDuration/rDex);
        }
        else if (rollTime > 0)
        {
            Roll(rollDuration);
        }
        else if (wlkTime > 0)
        {
            Walk(walkDuration/rDex);
        }
        else {
            if(dir==1)ChangeSprite(18);
            if (dir == 2) ChangeSprite(0);
            if (dir == 3) ChangeSprite(12);
            if (dir == 4) ChangeSprite(6);
            
        }

    }

    public void ChangeSprite(int index) {
        render.sprite = plImg[index];
        
        pri.sprite = priImg[index];
        sec.sprite = secImg[index];
        head.sprite = headImg[index];
        chest.sprite = chestImg[index];
        feet.sprite = feetImg[index];
    }

    public void ChangeAtk(int index) {
        rend.sprite = atkImg[index];

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
        PlayerPrefs.SetInt("Save",0);
        inv.cont.GoToScene("Menu");
    }
    public void CalculatStats() {
        print("HP inicial: "+hp);
        rHp=hp;
        rDex=dex;
        rStr=str;
        rDef=def;
        if(inv.itemDb.list!=null)
        for (int i = 0; i < 8; i++) {

            int j = inv.slot[i];
            rHp +=  itemDb.list[j].hp;
            rDex += itemDb.list[j].dex;
            rStr += itemDb.list[j].str;
            rDef += itemDb.list[j].def;
           
        }
        if(mod.effectDb.list!=null)
        mod.Add();
        if (rollTime > 0) rDef += 100;
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

    public string ListStats()
    {
        string stat = "";
        

         stat += "hp: " + rHp+ "\n";

        stat += "dex: " + rDex + "\n";

        stat += "str: " + rStr+ "\n";

        stat += "def: " + rDef + "\n";

        
        stat = stat.Replace('$', '\n');
        return stat;

    }


    ///////               XML                ////////////////////////////////////////////////////////////////////
    public ItemDataBase itemDb;


    public string GenerateText()
    {
        string t = "";
        if (itemDb.list[0] != null)
        {
            foreach (Item it in itemDb.list)
            {
                t += it.id + "- Nome: *" + it.name + "* Slot:" + it.slot + " Slot:" + it.slot + " Hp:" + it.hp + " Dex:" + it.dex + " Str:" + it.str + " Def:" + it.def + "\n";

            }

        }
        t = t.Replace('$', '\n');

        return t;
    }
    /// ///////// Animations

    public void Ataque(float maxTim)
    {
        if (firstW)
        {
            int min = 0;
            int max = 0;

            int minA=0;
            int maxA = 0;

            wlkTime = 0;

            
            if (dir == 1)
            {
                min = 32; max = 36;
                minA = 12; maxA = 16;
            }
            if (dir == 2)
            {
                min = 24; max = 28;
                minA = 0; maxA = 4;
            }
            if (dir == 3)
            {
                min = 36; max = 40;
                minA = 8; maxA = 12;
            }
            if (dir == 4)
            {
                min = 28; max = 32;
                minA = 4; maxA = 8;
            }
            

            atkTime += Time.deltaTime;
            int p = (int)(((atkTime / maxTim) * (max - min)) + min);
            int o = (int)(((atkTime / maxTim) * (maxA - minA)) + minA);
            if (p > max-1) p = max-1;
            if (o > maxA-1) o = maxA-1;
            ChangeSprite(p);
            ChangeAtk(o);

            if (col != null)
            {
                foreach (Collider2D cl in col)
                {
                    cl.GetComponent<Enemy>().TakeDamage(rStr / maxTim * Time.deltaTime);
                    print("EffId: "+ itemDb.list[inv.slot[1]].effect);
                    cl.GetComponent<Enemy>().AddEffect(itemDb.list[inv.slot[1]].effect);
                   
                }

            }

            if (atkTime > maxTim)
            {
                atkTime = 0;
            }
        }
        else if(inv.slot[2]!=0) {

            int min = 0;
            int max = 0;

            wlkTime = 0;
            if (dir == 1)
            {
                min = 19; max = 23;
            }
            if (dir == 2)
            {
                min = 1; max = 5;
            }
            if (dir == 3)
            {
                min = 13; max = 17;
            }
            if (dir == 4)
            {
                min = 7; max = 11;
            }


            atkTime += Time.deltaTime;
            int p = (int)(((atkTime / maxTim) * (max - min)) + min);
            ChangeSprite(p);
            

            if (atkTime > maxTim)
            {
                
                for (int i=0;i<5; i++) {
                    Quaternion rote = Quaternion.Euler(0, 0, PointToMouse()+Random.Range(-20,20));
                    Instantiate(bullet, transform.position, rote);
                    

                }
                bullets--;
                if (bullets <= 0) inv.slot[2] = 0;
                atkTime = 0;
               
            }


            
        }
    }
    public void Walk(float maxTim) {
        int min = new int();
        int max = new int();
        if (direc == 1) {
            min = 19;max = 24;
        }
        if (direc == 2)
        {
            min = 1; max = 5;
        }
        if (direc == 3)
        {
            min = 13; max = 18;
        }
        if (direc == 4)
        {
            min = 7; max = 12;
        }
        wlkTime += Time.deltaTime;
        int p = (int)(((wlkTime / maxTim) * (max - min)) + min);
        if (p < min) p = min;
        if (p > max-1) p = max-1;
        ChangeSprite(p);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (wlkTime > maxTim)
        {
            if (h != 0 || v != 0)
            {
                wlkTime = 0.01f;
            }
            else
            {
                wlkTime = 0;
            }
        }
    }
  
    public void Roll(float maxTim)
    {
        int min = new int();
        int max = new int();
        if (direc == 1)
        {
            min = 19; max = 23;
        }
        if (direc == 2)
        {
            min = 1; max = 5;
        }
        if (direc == 3)
        {
            min = 13; max = 17;
        }
        if (direc == 4)
        {
            min = 7; max = 11;
        }
        rollTime += Time.deltaTime;
        int p = (int)(((rollTime / maxTim) * (max - min)) + min);
        ChangeSprite(p);
        if (rollTime > maxTim)
        {
            rollTime = 0;
        }
    }
}



