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
        
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        
        pickable = true;
        id = Random.Range(0, inv.itemDb.list.Capacity);

        
         


    }
  

}
