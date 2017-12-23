using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour {
    public GameObject[] rooms;
    public int[] rX;
    public int[] rY;
    public bool[,] rDoor;
    public int rNum;

    public int[] ammount;//0none 1:player 2:end 3:monsters 3:


    public GameObject room;

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
        int j = 0;
        for (int i = 0; i < 15; i++) {
            for (int p = 0; p<ammount[i]; p++){
                rooms[j].GetComponent<Room>().type = i;
                j++;
            }
        }
    }
    public void Doors(int index,bool[] doors) {
        print("Doors " + doors[1] + " " + doors[2] + " " + doors[3] + " " + doors[4] + "index: " + index);
        rooms[index].GetComponent<Room>().doors = doors;
        rooms[index].GetComponent<Room>().CreateDoors();
    }

}
