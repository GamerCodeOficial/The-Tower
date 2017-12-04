using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    //1:right 2:down 3:left 4:up 

    public int numR;
    int numInici;
    public int size;
    public int[,] board; //0 wall 1 room 2 corridor
    public GameObject[] tiles;            

    public void CreateRoom(int centx,int centy,int width,int weight,int dir) {
        
        bool[] corri=new bool[5];
        int newDir= 0;
        while (newDir == 0) {
            newDir= Random.Range(1, 4);
            if (newDir == dir) newDir = 0;
            }
        
            corri[newDir] = true;
            numR--;
       
        /*
            for (int i = 1; i < 5; i++)
            {
               if (Random.Range(0, 100) < 100 * numR / numInici&&i!=dir)
                {
                
                print(100*numR/numInici+" "+numR);
                numR--;
                corri[i] = true;
                }
                
            }
        
            */
            for (int x = centx - 3; x <= centx + 3; x++)
            {
                for (int y = centy - 3; y <= centy + 3; y++)
                {
                    board[x, y] = 1;
                }
            }


            if (corri[1] == true)
            {
                int x = centx + 3 + 1;
                int y = centy + Random.Range(-3, 3);
             CreateCorridor(1, x, y);
            }

            if (corri[2] == true)
            {
                int y = centy - 3 - 1;
                int x = centy + Random.Range(-3, 3);
             CreateCorridor(2, x, y);
            }
            if (corri[3] == true)
            {
                int x = centx - 3 - 1;
                int y = centy + Random.Range(-3, 3);
             CreateCorridor(3, x, y);
            }
            if (corri[4] == true)
            {
                int y = centy + 3 + 1;
                int x = centy + Random.Range(-3,3);
            CreateCorridor(4, x, y);
            }



        
    }

   

    public void CreateCorridor(int dir,int x,int y) {
        
        if (numR > 0)
        {
            int size = Random.Range(3, 6);

            if (x > 15 && x < 90 && y > 10 && y < 95)
            {

                if (dir == 1)
                {
                    for (int i = x; i < x + size; i++)
                    {
                        board[i, y] = 2;
                    }


                   if(board[x + 1 + 3, y]==0) CreateRoom(x + 1 + 3, y + Random.Range(-3, 3), 6, 6, 3);
                }

                else if (dir == 2)
                {
                    for (int i = y; i > y - size; i--)
                    {
                        board[x, i] = 2;
                    }

                    if (board[ x, y - 1 - 3] == 0) CreateRoom(x + Random.Range(-3, 3), y + 1 + 3, 6, 6, 4);


                }
                else if (dir == 3)
                {
                    for (int i = x; i > x - size; i--)
                    {
                        board[i, y] = 2;
                    }

                    if (board[x - 1 - 3, y] == 0) CreateRoom(x - 1 - 3, y + Random.Range(-3, 3), 6, 6, 1);

                }
                else if (dir == 4)
                {
                    for (int i = y; i < y + size; i++)
                    {
                        board[x, i] = 2;
                    }

                    if (board[x, y + 1 + 3] == 0) CreateRoom(x + Random.Range(-3, 3), y - 1 - 3, 6, 6, 2);

                }




            }
        }
      
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
        numInici = numR;

        board = new int[size, size];
        for (int x=0;x<size;x++) {
            for (int y = 0; y < size; y++)
            {
              
                board[x,y] = 0;
            }
        }
        
        CreateRoom(size / 2, size / 2, 6, 6,0);

        GenerateAll();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
