using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public int total;
    public int id;
    
    public Inventory inv;
    public bool pickable;

    void Start()
    {
        print("Eu sou foda");
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        total = inv.total;
        pickable = true;
        id = Random.Range(0, total);

        while (PlayerPrefs.GetInt("iSlot"+id) == 0)
        {
            id=Random.Range(0, total);
        }
       
        
    }
  

}
