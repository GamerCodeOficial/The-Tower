using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public Sprite[] img;

    public RoomControl rContr;

    public int size;
    public int[,] grid;
    public int[,] real;

    public GameObject[] tiles;

    public int nCorridors;

    public int tamanho;

    public MiniMap miniMap;

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
                        if (pos[ra] == true) { nDir[ra] = true; p++;  }
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

    public void Conections()// Corridors position in grid
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)//loop through all the grid
            {


                if (grid[x, y] == 1)//if grid == corridor
                {
                    
                    
                    if (x < size - 1)// distance of at least one from 
                    {
                        if (grid[x + 1, y] == 2)// right grid == room 
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
                        if (grid[x + 1, y] != 0)// right != wall
                        {
                            for (int o = -6; o < 1; o++) {
                                for (int i = 4; i < 6; i++)
                                {
                                    real[((x + 1) * tamanho) +o, (y * tamanho) + i] = 1;
                                }
                            }
                        }
                    }
                    if (x > 1)
                    {
                        if (grid[x - 1, y] != 0)// left != wall
                        {

                            for (int o = -1; o < 6; o++)
                            {
                                for (int i = 4; i < 6; i++)
                                {
                                    real[(x * tamanho) + o, (y * tamanho) + i] = 1;
                                }
                            }

                        }
                    }
                    if (y < size - 1)
                    {
                        if (grid[x, y + 1] != 0)// top != wall
                        {

                            for (int o = -6; o < 1; o++)
                            {
                                for (int i = 4; i < 6; i++)
                                {
                                    real[(x * tamanho) + i, ((y + 1) * tamanho) +o] = 1;
                                }
                            }

                        }
                    }
                    if (y > 1)
                    {
                        if (grid[x, y - 1] != 0)// bottom != wall
                        {
                            for (int o = -1; o < 6; o++)
                            {
                                for (int i = 4; i < 6; i++)
                                {
                                    real[(x * tamanho) + i, (y * tamanho) + o] = 1;
                                }
                            }
                        }
                    }


                }
            }
        }
    }

    public void Roomificate()
    {
        

        for (int x = 1; x < size-1; x++)
        {
            for (int y = 1; y < size-1; y++)
            {
                bool[] rooms = new bool[5];
                if (grid[x+1, y] != 0)
                {
                    rooms[1] = true;
                }
                if (grid[x, y-1] != 0)
                {
                    rooms[2] = true;
                }
                if (grid[x-1, y] != 0)
                {
                    rooms[3] = true;
                }
                if (grid[x, y+1] != 0)
                {
                    rooms[4] = true;
                }

               
            }
        }
    }
    void Roomie() {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y] == 2)
                {
                    bool[] door = new bool[5];
                    if (grid[x + 1, y] != 0) door[1] = true;
                    if (grid[x , y - 1] != 0) door[2] = true;
                    if (grid[x - 1, y] != 0) door[3] = true;
                    if (grid[x , y + 1] != 0) door[4] = true;
                    
                    rContr.AddRoom(x,y,door); 
                }
            }
        }
    }
    void Doorate() {
        int p = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y] == 2)
                {
                    bool[] door = new bool[5];
                    if (grid[x + 1, y] == 1) door[1] = true;
                    if (grid[x, y - 1] == 1) door[2] = true;
                    if (grid[x - 1, y] == 1) door[3] = true;
                    if (grid[x, y + 1] == 1) door[4] = true;
                    
                    rContr.Doors(p ,door);
                    p++;
                }
            }
        }
    }
    public void WallColliders() {
        for (int x = 2; x < (size * tamanho) - 2; x++)
        {
            for (int y = 2; y < (size * tamanho)-2; y++)
            {
                if (real[x, y] > 0 && real[x, y] < 3 ) {
                    if (real[x, y + 1]==0 || real[x, y + 1] > 2) {

                        real[x, y + 1] = 3;
                    }
                    if (real[x, y - 1] == 0 || real[x, y - 1] > 2)
                    { 
                            real[x, y - 1] = 4;
                    }
                    if (real[x + 1, y] == 0)
                    {
                        real[x + 1, y] = 5;
                    }
                    if (real[x - 1, y] == 0)
                    {
                        real[x - 1, y] = 6;
                    }
                }
            }
        }
      
        for (int x = 2; x < (size * tamanho) - 2; x++)
        {
            for (int y = 2; y < (size * tamanho) - 2; y++)
            {
                if (real[x, y] == 3)
                {
                    if (real[x + 1, y ] == 0 && real[x + 2, y] == 0)
                    {
                        real[x + 1, y ] = 5;
                    }
                    if (real[x - 1, y ] == 0 && real[x - 2, y] == 0)
                    {
                        real[x - 1, y ] = 6;
                    }
                }
       
            }
        }
        
  
        for (int x = 2; x < (size * tamanho) - 2; x++)
        {
            for (int y = 2; y < (size * tamanho) - 2; y++)
            {
                if (real[x, y] == 4)
                {
                    if (real[x + 1, y ] == 0)
                    {
                        real[x + 1, y ] = 7;
                    }
                    if (real[x - 1, y ] == 0)
                    {
                        real[x - 1, y] = 8;
                    }
                }

            }
        }
        for (int x = 2; x < (size * tamanho) - 2; x++)
        {
            for (int y = 2; y < (size * tamanho) - 2; y++)
            {
                if (real[x, y] == 4)
                {
                    if (real[x + 1, y] ==1|| real[x + 1, y] == 2)
                    {
                        real[x , y] = 9;
                    }
                    if (real[x - 1, y] == 1 || real[x - 1, y] == 2)
                    {
                        real[x , y] = 10;
                    }
                }

            }
        }


    }

    // Use this for initialization
    void Start()
    {

        img = Resources.LoadAll<Sprite>("Graphics/Tiles/Fase"+rContr.andar);
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
        WallColliders();
        Roomificate();
        GenerateAll();
        Roomie();
        rContr.CreateRooms();
        rContr.Positionate();
        rContr.ChooseTypes();
        Doorate();
        
        miniMap.LoadAll(real);
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
                GameObject fl=Instantiate(tiles[real[x, y]], pos, q);
                fl.GetComponent<SpriteRenderer>().sprite = img[real[x, y]];
                fl.transform.parent = transform.parent;
            }
        }


    }
}
