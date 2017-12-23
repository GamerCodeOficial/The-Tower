using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public GameObject player;
    public GameObject shadow;
    public GameObject door;
    public bool[] doors;
    public bool oppened;
    public int type;
    void Start()
    {
        oppened=false;
        if (type == 1) {
            oppened = true;
            Instantiate(player,transform.position,transform.rotation);
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
}
