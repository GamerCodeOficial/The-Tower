using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public RoomBase[] ids;
    public RoomBase[] rot;
    public RoomBase[] col;

    public RoomBase[] enemies;


    public GameObject componnent;


    public RoomControl rom;
    public GameObject player;
    public GameObject shadow;
    public GameObject door;
    public GameObject end;

    public GameObject[] rooms;

    public bool[] doors;
    public bool oppened;
    public int type;

    public int[] spawn;

    public Sprite[] img;
    public GameObject[] en;

    void Start()
    {

        rom = GameObject.FindGameObjectWithTag("Board").GetComponent<RoomControl>();
        spawn = new int[50];
        img = Resources.LoadAll<Sprite>("Graphics/Tiles/Room" + rom.andar);
        en = rom.monsters;
        player = GameObject.FindGameObjectWithTag("Player");
        Load();
        Modificate();
        oppened = false;
        if (type == 1) {
            // Spawn(); 
            oppened = true;
            player.transform.position = transform.position;
        }
        if (type == 2)
        {
            // End();
        }
        if (type == 3)
        {
            //Monster();
        }
        if (type == 4) {
            //Boss();
        }

    }
    void Update()
    {
        shadow.SetActive(!oppened);
    }
    public void Modificate() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject p=gameObject.transform.GetChild(i).gameObject; 
            if (p.GetComponent<RoomComponnent>()) {
                
                    p.GetComponent<SpriteRenderer>().sprite = img[p.GetComponent<RoomComponnent>().id];
              
            }
        }
    }
    public void CreateDoors()
    {
        if (doors[1]) {
            Vector2 pos = transform.position;
            pos.x += 4.5f;
            GameObject dor = Instantiate(door, pos, transform.rotation);
            dor.transform.parent = gameObject.transform;
            dor.gameObject.GetComponent<DoorControl>().rom = gameObject.GetComponent<Room>();
            dor.gameObject.GetComponent<DoorControl>().dir = 1;
        }
        if (doors[2])
        {
            Vector2 pos = transform.position;
            pos.y -= 4.5f;
            GameObject dor = Instantiate(door, pos, transform.rotation);
            dor.transform.parent = gameObject.transform;
            dor.gameObject.GetComponent<DoorControl>().rom = gameObject.GetComponent<Room>();
            dor.gameObject.GetComponent<DoorControl>().dir = 2;
        }
        if (doors[3])
        {
            Vector2 pos = transform.position;
            pos.x -= 4.5f;
            GameObject dor = Instantiate(door, pos, transform.rotation);
            dor.transform.parent = gameObject.transform;
            dor.gameObject.GetComponent<DoorControl>().rom = gameObject.GetComponent<Room>();
            dor.gameObject.GetComponent<DoorControl>().dir = 3;
        }
        if (doors[4])
        {
            Vector2 pos = transform.position;
            pos.y += 4.5f;
            GameObject dor = Instantiate(door, pos, transform.rotation);
            dor.transform.parent = gameObject.transform;
            dor.gameObject.GetComponent<DoorControl>().rom = gameObject.GetComponent<Room>();
            dor.gameObject.GetComponent<DoorControl>().dir = 4;
        }

    }
    public void Spawn() {

    }
    public void End() {
        GameObject e = Instantiate(end, transform.position, transform.rotation);

    }
    public void Monster() {
        int j = 0;

        for (int i = 0; i < rom.monSpwPct.Length; i++) {
            for (int p = 0; p < rom.monSpwPct[i]; p++) {
                spawn[j] = i;
                j++;
            }
        }
        int ammount = Random.Range(1, 6);
        for (int i = 0; i < ammount; i++) {
            int r = Random.Range(0, j);
            GameObject en = Instantiate(rom.monsters[spawn[r]], transform.position, transform.rotation);
            en.GetComponent<Enemy>().rom = this;
        }


    }
    public void Boss() {
        GameObject en = Instantiate(rom.boss, transform.position, transform.rotation);
        en.GetComponent<Boss>().en.rom = this;
    }
    public void Load() {
        for (int i = 0; i < 64; i++) {
           
                Vector3 pos = transform.position;
                pos.x += (i % 8)-3.5f;
                pos.y += (int)(i / 8) - 3.5f;
                GameObject p = Instantiate(componnent, pos, transform.rotation, transform);
                p.GetComponent<RoomComponnent>().id = ids[type].Convert()[i];
            int d = rot[type].Convert()[i];
            //1:direita 2:esquerda 3:baixo 
            if (d == 1) { Quaternion rote = Quaternion.Euler(0, 0, -90);  p.transform.rotation = rote; }
            if (d == 2) { Quaternion rote = Quaternion.Euler(0, 0, 90); p.transform.rotation = rote; }
            if (d == 3) { Quaternion rote = Quaternion.Euler(0, 0, 180); p.transform.rotation = rote; }
            if (enemies[type].Convert()[i] != 0)
            {
                GameObject o = Instantiate(en[enemies[type].Convert()[i]], p.transform.position, transform.rotation, transform);
                o.GetComponent<Enemy>().rom = this;
            }
        }

    }
    
}
