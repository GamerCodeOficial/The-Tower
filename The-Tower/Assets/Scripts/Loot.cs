using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public int tier;
    public GameObject sala;
    public int id;
    public float pct;
    public Inventory inv;
    public bool pickable;

    void Start()
    {
        pickable = true;
        id = Random.Range((tier - 1) * 100, tier * 100);
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if (Random.Range(0,100) > pct)
        {

            Destroy(gameObject);
        }

        while (inv.iSlot[id] == 0)
        {
            id=Random.Range((tier - 1) * 100, tier * 100);
        }
    }
  

}
