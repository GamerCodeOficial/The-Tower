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
        print("Capacity: "+inv.itemDb.list.Count);
        id = Random.Range(1, inv.itemDb.list.Count);
        print("Loot Id"+id);
        
         


    }
  

}
