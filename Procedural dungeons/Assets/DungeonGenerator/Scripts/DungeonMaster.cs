using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public int size;
    public int[,] grid;
    public int[,] real;

    public GameObject[] tiles;

    public int nCorridors;
    

    public void Corridors(int x,int y,int nCor){


        for (int i = 0; i < nCor ;i++) {
            
            grid[x,y]=1;
            bool[] pos = new bool[5];
            bool[] nDir = new bool[5];
            int q = 0;
            if (x < size - 1)
            {
                if (grid[x + 1, y] == 0 && grid[x + 1, y-1] == 0 && grid[x + 1, y + 1] == 0) { pos[1] = true; q++; }

            }
            if(x > 1)
            {
                if (grid[x - 1, y] == 0 && grid[x - 1, y - 1] == 0 && grid[x - 1, y + 1] == 0) { pos[3] = true; q++; }
            }
            if (y > 1)
            {
                if (grid[x, y - 1] == 0 && grid[x + 1, y - 1] == 0 && grid[x - 1, y - 1] == 0) { pos[2] = true; q++; }
            }
            if ( y < size - 1)
            {
                if (grid[x, y + 1] == 0 && grid[x + 1, y + 1] == 0 && grid[x - 1, y + 1] == 0) { pos[4] = true; q++; }
               
            }
            int oi = 0;
            if (q > 0) oi = 1;
                    int p = 0;

            while (p < oi)
            {
                int ra = Random.Range(1, 4);
                if (pos[ra] == true) { nDir[ra] = true; p++; }
            }


            if (nDir[1] && x < size - 1)
            {
                x++;
            }
            if (nDir[2] && y > 1)
            {
                y--;
            }

            if (nDir[3] && x > 1)
            {
                x--;

            }
            if (nDir[4] && y < size - 1)
            {
                y++;

            }
        }



    }

    public void CreateRooms() {

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y] == 1) {

                    bool[] pos = new bool[5];
                    bool[] nDir = new bool[5];
                    int q = 0;
                   
                        if (grid[x, y + 1] == 0) { pos[1] = true; q++; }

                        if (grid[x - 1, y] == 0 ) { pos[3] = true; q++; }
                    
                        if (grid[x, y - 1] == 0) { pos[2] = true; q++; }

                        if (grid[x, y + 1] == 0) { pos[4] = true; q++; }

                    
                    int oi = 0;
                    if (q > 0) oi = 1;
                    int p = 0;

                    while (p < oi)
                    {
                        int ra = Random.Range(1, 4);
                        if (pos[ra] == true) { nDir[ra] = true; p++; print(ra); }
                    }


                    if (nDir[1] && x < size - 1)
                    {
                        grid[x + 1, y] = 2;
                    }
                    if (nDir[2] && y > 1)
                    {
                        grid[x , y - 1] = 2;
                    }

                    if (nDir[3] && x > 1)
                    {
                        grid[x - 1, y] = 2;

                    }
                    if (nDir[4] && y < size - 1)
                    {
                        grid[x , y + 1] = 2;

                    }



                }
            }
        }

    }

    public void Realize() {

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y] == 0)
                {
                    for (int xi = x * 10; xi < (x + 1) * 10; xi++)
                    {
                        for (int yi = y * 10; yi < (y + 1) * 10; yi++)
                        {
                            real[xi, yi] = 0;
                        }
                    }
                }
                else if (grid[x, y] == 1)
                {
                    for (int xi = x * 10; xi < (x + 1) * 10; xi++)
                    {
                        for (int yi = y * 10; yi < (y + 1) * 10; yi++)
                        {
                            real[xi, yi] = 1;
                        }
                    }
                }
                else if (grid[x, y] == 2)
                {
                    print ("Sala");
                    for (int xi = (x * 10) + 1; xi < ((x + 1) * 10) - 1; xi++)
                    {
                        for (int yi = (y * 10) + 1; yi < ((y + 1) * 10) - 1; yi++)
                        {
                            print("normal");
                            real[xi, yi] = 2;
                        }
                    }

                    



                }
            }
        }


    }

    public void Conections()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y] == 2)
                {

                    if (x < size - 1)
                    {
                        if (grid[x + 1, y] != 0)
                        {
                            print("direita");
                            real[((x + 1) * 10) - 1, (y * 10) + 4] = 1;
                            real[((x + 1) * 10) - 1, (y * 10) + 5] = 1;
                        }
                    }
                    if (x > 1)
                    {
                        if (grid[x - 1, y] != 0)
                        {
                            print("esquerda");
                            real[x * 10, (y * 10) + 4] = 1;
                            real[x * 10, (y * 10) + 5] = 1;
                        }
                    }
                    if (y < size - 1)
                    {
                        if (grid[x, y + 1] != 0)
                        {
                            print("cima");
                            real[(x * 10) + 4, ((y + 1) * 10)-1] = 1;
                            real[(x * 10) + 5, ((y + 1) * 10)-1] = 1;
                        }
                    }
                    if (y > 1)
                    {
                        if (grid[x, y - 1] != 0)
                        {
                            print("baixo");
                            real[(x * 10) + 4, y * 10] = 1;
                            real[(x * 10) + 5, y * 10] = 1;
                        }
                    }

                }
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        

        grid = new int[size, size];
        real = new int[size*10, size*10];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                grid[x, y] = 0;
            }
        }

        Corridors(size/2,size/2 , (nCorridors/2)+1);
        Corridors(size / 2, size / 2, nCorridors / 2);
        CreateRooms();
        Realize();
        Conections();
        GenerateAll();

    }
    



    public void GenerateAll()
    {

        for (int x = 0; x < size*10; x++)
        {
            for (int y = 0; y < size*10; y++)
            {
                Vector3 pos;
                pos.x = x;
                pos.y = y;
                pos.z = 0;
                Quaternion q = new Quaternion();
                Instantiate(tiles[real[x, y]], pos, q);

            }
        }


    }
}
