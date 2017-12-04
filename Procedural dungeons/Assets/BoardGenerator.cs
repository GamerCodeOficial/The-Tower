using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    //1:right 2:down 3:left 4:up 

    public int numR;
    public int size;
    public int[,] board; //0 wall 1 room 2 corridor
    public GameObject[] tiles;            

    public void CreateRoom(int centx,int centy,int width,int weight,int dir) {

        bool[] corri=new bool[5];

        for (int i = 1; i < 5;i++) {
            if (Random.Range(0, 100)<50) {
                corri[i]= true;
            }
            }
        corri[dir] = false;
        
        for (int x = centx - (width / 2); x <= centx + (width / 2); x++)
        {
            for (int y = centy - (width / 2); y <= centy + (width / 2); y++)
            {
                board[x, y] = 1;
            }
        }

       
            if (corri[1] == true)
            {
            int x= centx + (width / 2)+1;
            int y= centy+Random.Range(-width / 2, width / 2);
            CreateCorridor(1,x,y);
            }
            
        if (corri[2] == true)
        {
            int y = centy - (width / 2) - 1;
            int x = centy + Random.Range(-width / 2, width / 2);
            CreateCorridor(2, x, y);
        }
        if (corri[3] == true)
        {
            int x = centx - (width / 2) - 1;
            int y = centy + Random.Range(-width / 2, width / 2);
            CreateCorridor(3, x, y);
        }
        if (corri[4] == true)
        {
            int y = centy + (width / 2) + 1;
            int x = centy + Random.Range(-width / 2, width / 2);
            CreateCorridor(4, x, y);
        }
        
  
    }

   

    public void CreateCorridor(int dir,int x,int y) {
        int size = Random.Range(3,6);

       /*
        if (dir == 1) {
            for (int i = x; i < x + size; i++) {
                board[i, y] = 2;
            }
            int widht = Random.Range(5, 8);
            int height = Random.Range(5, 8);

            CreateRoom(x + size + 1 + (widht / 2), y + Random.Range(-height / 2, height  / 2), widht ,height , 3);
        }
        else if (dir == 2)
        {
            for (int i = y; i > y - size; i--)
            {
                board[x, i] = 2;
            }

            int widht = Random.Range(5, 8);
            int height = Random.Range(5, 8);

            CreateRoom( x + Random.Range(-widht / 2, widht / 2), y - size - 1 - (widht / 2), widht, height, 4);


        }
        else if (dir == 3)
        {
            for (int i = x; i > x - size; i--)
            {
                board[i, y] = 2;
            }

            int widht = Random.Range(5, 8);
            int height = Random.Range(5, 8);

            CreateRoom(x - size - 1 - (widht / 2), y + Random.Range(-height / 2, height / 2), widht, height, 1);

        }
        else if (dir == 4)
        {
            for (int i = y; i < y + size; i++)
            {
                board[x, i] = 2;
            }

            int widht = Random.Range(5, 8);
            int height = Random.Range(5, 8);
            CreateRoom(x + Random.Range(-widht / 2, widht / 2), y + size + 1 + (widht / 2), widht, height, 2);

        }
        

        */

    
    }

    public void GenerateAll() {

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y <size; y++)
            {
                Vector3 pos;
                pos.x = x;
                pos.y = y;
                pos.z = 0;
                Quaternion q = new Quaternion();
                Instantiate(tiles[board[x,y]],pos,q);

            }
        }


    }

	void Start () {
        board = new int[size, size];
        for (int x=0;x<size;x++) {
            for (int y = 0; y < size; y++)
            {
              
                board[x,y] = 0;
            }
        }
        
        CreateRoom(size / 2, size / 2, Random.Range(5, 8), Random.Range(5, 8),0);
        GenerateAll();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
