using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public RoomControl rom;
    public GameObject player;
    public GameObject shadow;
    public GameObject door;
    public bool[] doors;
    public bool oppened;
    public int type;

    public int[] spawn; 

    void Start()
    {
        rom = GameObject.FindGameObjectWithTag("Board").GetComponent<RoomControl>();
        spawn = new int[50];

        player = GameObject.FindGameObjectWithTag("Player");
        oppened=false;
        if (type == 1) {
            Spawn();
            oppened = true;
            
            player.transform.position=transform.position;
        }
        if (type == 2)
        {
            End();
        }
        if (type == 3)
        {
            Monster();
        }

    }
    void Update()
    {
        shadow.SetActive(!oppened); 
    }
    public void CreateDoors()
    {
        if(doors[1]) {
            Vector2 pos = transform.position;
            pos.x += 4.5f;
            GameObject dor = Instantiate(door, pos,transform.rotation);
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

    }
    public void Monster() {
        int j = 0;
        
        for (int i = 0; i < rom.monSpwPct.Length; i++) {
            for (int p = 0; p < rom.monSpwPct[i];p++) {
                spawn[j] = i;
                    j++;
            }
        }
        int ammount = Random.Range(1, 7);
        for (int i=0;i<ammount;i++) {
            int r = Random.Range(0, j);
            Instantiate(rom.monsters[spawn[r]], transform.position, transform.rotation);
        }
        

    }
    public void Boss() {

    }
}
