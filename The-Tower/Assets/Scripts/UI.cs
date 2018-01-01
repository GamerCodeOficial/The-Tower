using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour {
    public PlayerRpg rpg;
    public Inventory inv;
    public GameObject[] vidas;
    public Sprite[] cors;

    public GameObject character;

    public Text stats;

    public Image face;
    public Image avatar;

	// Use this for initialization
	void Start () {
        rpg = GameObject.FindGameObjectWithTag("RPG").GetComponent<PlayerRpg>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Life();
    }
	
	// Update is called once per frame
	void Update () {
        stats.text = inv.rpg.ListStats();
        Life();
        Color c = inv.rpg.GetColor(PlayerPrefs.GetString("Cor"));
        face.color = c;
        avatar.color = c;
    }

    public void Life() {

        int hp=(int)rpg.cHp;
        int mHp = (int)rpg.hp;
        for (int i = 0; i < 10; i++)
        {

            vidas[i ].GetComponent<SpriteRenderer>().sprite = cors[5];

        }

        for (int i=0;i< (hp / 4) ; i++) {
            
            vidas[i].GetComponent<SpriteRenderer>().sprite = cors[0]; 
 
        }
        for (int i = hp / 4; i < mHp/4; i++)
        {
            
            vidas[i].GetComponent<SpriteRenderer>().sprite = cors[4];

        }
        int p = hp % 4;
        
        vidas[(hp-1) / 4].GetComponent<SpriteRenderer>().sprite = cors[p];
        

    }

    public void Charc(){
        print("Charc");
        character.SetActive(!character.activeSelf);
    }

}
