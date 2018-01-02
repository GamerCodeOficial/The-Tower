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

    public int tamanho;

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
                    for (int xi = x * tamanho; xi < (x + 1) * tamanho; xi++)
                    {
                        for (int yi = y * tamanho; yi < (y + 1) * tamanho; yi++)
                        {
                            real[xi, yi] = 0;
                        }
                    }
                }
                else if (grid[x, y] == 1)
                {
                    for (int xi = (x * tamanho) + 2; xi < ((x + 1) * tamanho) - 2; xi++)
                    {
                        for (int yi = (y * tamanho) + 2; yi < ((y + 1) * tamanho) - 2; yi++)
                        {
                            
                            real[xi, yi] = 1;
                        }
                    }
                }
                else if (grid[x, y] == 2)
                {
                    
                    for (int xi = (x * tamanho) + 1; xi < ((x + 1) * tamanho) - 1; xi++)
                    {
                        for (int yi = (y * tamanho) + 1; yi < ((y + 1) * tamanho) - 1; yi++)
                        {
                           
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
                if (grid[x, y] == 1)
                {

                    if (x < size - 1)
                    {
                        if (grid[x + 1, y] == 2)
                        {
                         
                            real[((x + 1) * tamanho) - 1, (y * tamanho) + 4] = 1;
                            real[((x + 1) * tamanho) - 1, (y * tamanho) + 5] = 1;
                        }
                    }
                    if (x > 1)
                    {
                        if (grid[x - 1, y] == 2)
                        {
                            
                            real[x * tamanho, (y * tamanho) + 4] = 1;
                            real[x * tamanho, (y * tamanho) + 5] = 1;
                        }
                    }
                    if (y < size - 1)
                    {
                        if (grid[x, y + 1] == 2)
                        {
                            
                            real[(x * tamanho) + 4, ((y + 1) * tamanho) -1] = 1;
                            real[(x * tamanho) + 5, ((y + 1) * tamanho) -1] = 1;
                        }
                    }
                    if (y > 1)
                    {
                        if (grid[x, y - 1] == 2)
                        {
                            
                            real[(x * tamanho) + 4, y * tamanho] = 1;
                            real[(x * tamanho) + 5, y * tamanho] = 1;
                        }
                    }


                    if (x < size - 1)
                    {
                        if (grid[x + 1, y] != 0)
                        {
                          
                            for (int i = 3; i < 7; i++) {
                                real[((x + 1) * tamanho) - 1, (y * tamanho) + i] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[((x + 1) * tamanho) - 2, (y * tamanho) + i] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[((x + 1) * tamanho) , (y * tamanho) + i] = 1;

                            }

                        }
                    }
                    if (x > 1)
                    {
                        if (grid[x - 1, y] != 0)
                        {
                          
                            for (int i = 3; i < 7; i++)
                            {
                                real[x * tamanho, (y * tamanho) + i] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) +1, (y * tamanho) + i] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) -1, (y * tamanho) + i] = 1;

                            }

                        }
                    }
                    if (y < size - 1)
                    {
                        if (grid[x, y + 1] != 0)
                        {
                          
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, ((y + 1) * tamanho) - 1] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, ((y + 1) * tamanho) - 2] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, ((y + 1) * tamanho) ] = 1;

                            }

                        }
                    }
                    if (y > 1)
                    {
                        if (grid[x, y - 1] != 0)
                        {
                          
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, y * tamanho] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, (y * tamanho) +1] = 1;

                            }
                            for (int i = 3; i < 7; i++)
                            {
                                real[(x * tamanho) + i, (y * tamanho) -1] = 1;

                            }

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

        for (int x = 0; x < size* tamanho; x++)
        {
            for (int y = 0; y < size* tamanho; y++)
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
