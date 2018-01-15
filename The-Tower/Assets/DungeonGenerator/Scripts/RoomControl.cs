using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour {


    public int andar;
    public GameObject[] rooms;
    public int[] rX;
    public int[] rY;
    public bool[,] rDoor;
    public int rNum;

    public int[] ammount;//0none 1:player 2:end 3:monsters 4:Boss


    public GameObject room;

    public GameObject boss;
    public GameObject[] monsters;
    public float[] monSpwPct;
    

    // Use this for initialization
    void Start() {
        rDoor = new bool[30, 5];
        for (int i = 0; i < 30; i++) {
            for (int d = 0; d < 5; d++)
            {
                rDoor[i, d] = false;
            }
        }
    }

    // Update is called once per frame


    public void AddRoom(int x, int y, bool[] door) {
        rX[rNum] = x;
        rY[rNum] = y;
        //print(door[1]);
        //print(rDoor[1,1]);
        /*
        for (int i=0;i<5;i++) {
            rDoor[rNum,i] = door[i];
        }
        */

        rNum++;

    }

    public void CreateRooms()
    {
        for (int i = 0; i < rNum; i++) {
            Instantiate(room, transform.position, transform.rotation);
        }
        rooms = GameObject.FindGameObjectsWithTag("Room");
    }
    public void Positionate() {
        int p = 0;
        foreach (GameObject rom in rooms)
        {
            Vector2 pos = new Vector2();
            pos.x = (rX[p] * 10) + 4.5f;
            pos.y = (rY[p] * 10) + 4.5f;
            rom.transform.position = pos;
            p++;
        }
    } 
    public void ChooseTypes() {
       
        for (int i = 0; i < 15; i++) {
            for (int p = 0; p<ammount[i]; p++){
                int j = Random.Range(0,rooms.Length);
                while (rooms[j].GetComponent<Room>().type!=0) {
                    j = Random.Range(0, rooms.Length);
                }
                rooms[j].GetComponent<Room>().type = i;
                
            }
        }
    }
    public void Doors(int index,bool[] doors) {
        
        rooms[index].GetComponent<Room>().doors = doors;
        rooms[index].GetComponent<Room>().CreateDoors();
    }

}
