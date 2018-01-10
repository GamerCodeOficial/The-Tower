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
    public Text money;

    public Image face;
    public Image avatar;

 

	// Use this for initialization
	void Start () {
        cors = Resources.LoadAll<Sprite>("Hearts");
        rpg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRpg>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Life();
    }
	
	// Update is called once per frame
	void Update () {
        stats.text = inv.rpg.ListStats();
        money.text = inv.money.ToString();
        Life();
        Color c = inv.rpg.GetColor(PlayerPrefs.GetString("Cor"));
        face.color = c;
        avatar.color = c;
        
    }

    

    public void Life() {

        int hp=(int)rpg.cHp;
        if (hp <= 0) {
            vidas[0].GetComponent<Image>().sprite = cors[0];
            return; }
        else
        {
            int mHp = (int)rpg.hp;
            for (int i = 0; i < 10; i++)
            {

                vidas[i].GetComponent<Image>().sprite = cors[5];

            }

            for (int i = 0; i < (hp / 4); i++)
            {

                vidas[i].GetComponent<Image>().sprite = cors[0];

            }
            for (int i = hp / 4; i < mHp / 4; i++)
            {

                vidas[i].GetComponent<Image>().sprite = cors[4];

            }
            int p = hp % 4;

            vidas[(hp - 1) / 4].GetComponent<Image>().sprite = cors[p];

        }
    }

    public void Charc(){
        print("Charc");
        character.SetActive(!character.activeSelf);
    }

}
