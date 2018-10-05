using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public int total;
    public int id;
    
    public Inventory inv;
    public bool pickable;

    public int dropQuality;

    void Start()
    {  
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        
        pickable = true;
        print("Capacity: "+inv.itemDb.list.Count);

        List<int> it = new List<int>();
        foreach (Item item in inv.itemDb.list)
        {
            print("Item: " + " chance: " + item.chance+" Id: "+item.id);
            if (item.chance<=dropQuality)
            {
                for (int i = 0; i < item.chance; i++)
                {

                    it.Add(item.id);
                    print("Add: " + " Id: " + item.id);

                }
            }
        }
        print(it.Count);
        id = it[Random.Range(0, it.Count-1)];
        print("Loot Id"+id);
        
         


    }
  

}
