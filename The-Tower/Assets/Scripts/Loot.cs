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
        total = inv.total;
        pickable = true;
        id = Random.Range(0, total);

        while (inv.iSlot[id] == 0)
        {
            id=Random.Range(0, total);
        }
       
    }
  

}
