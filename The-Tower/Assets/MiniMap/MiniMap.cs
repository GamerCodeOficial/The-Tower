using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {
    public DungeonMaster dun;
    public GameObject player;
    public GameObject plIcon;
    public Sprite[] img;
    public GameObject pix;

    public GameObject[,] tiles;

    public int[,] like;

    public Vector3 pois;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        img = Resources.LoadAll<Sprite>("Graphics/MiniMap/Pixels");
        dun.miniMap = this;
        print("Awake");
        

    }
    public void LoadAll(int[,] grid) {
        tiles = new GameObject[dun.size * dun.tamanho, dun.size * dun.tamanho];
        for (int x=0;x<dun.size*dun.tamanho;x++) {
            for(int y = 0; y < dun.size * dun.tamanho; y++) {
                Vector3 pos = transform.position;
                pos.x += x - (dun.size * dun.tamanho/2);
                pos.y += y - (dun.size * dun.tamanho / 2);
                pos.z += 10;
               
                if (grid[x, y] > 0&& grid[x, y] < 3)
                {
                    GameObject o = Instantiate(pix, pos, transform.rotation);
                    o.GetComponent<SpriteRenderer>().sprite = img[1];
                    o.GetComponent<SpriteRenderer>().enabled = false;
                    o.transform.parent = gameObject.transform;
                    tiles[x, y] = o;
                }
                
            }
        }
        like = grid;
    }
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position;
        pois = transform.position;
        pois.x += pos.x-75;
        pois.y += pos.y-75;
        pois.z = 0;
        plIcon.transform.position = pois;
        if (like!=null) {
            TakeShadowOff();
        }
	}
    public void TakeShadowOff()
    {
        for (int x = 0; x < dun.size * dun.tamanho; x++)
        {
            for (int y = 0; y < dun.size * dun.tamanho; y++)
            {
                if (x > player.transform.position.x - 8 && x < player.transform.position.x + 8 && y > player.transform.position.y - 8 && y < player.transform.position.y + 8 && like[x,y]>0 && like[x, y] < 3) {
                    tiles[x, y].GetComponent<SpriteRenderer>().enabled = true;
                   
                }
            }
        }
    }
}
