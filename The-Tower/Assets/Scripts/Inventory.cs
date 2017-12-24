using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

    public Collider2D col;
    public LayerMask loot;
   

    public GameObject tkorlv;
    public Text found;
    public Text current;

    public int total;

    public string[] iName;
    public int[] iSlot;
    public string[] iType;
    public int[] iValue;
    public int[] size;
    
    public Loot[] line;


    public int[] slot; //0 null  1 Weapon  2 Secondary  3 Armor  4 Other 5 potions

    // Use this for initialization
    void Start () {
		
	}
    void Update() {
        
        if (line[0] == null) {
            tkorlv.SetActive(false);
            
        } else {
            tkorlv.SetActive(true);
            found.text = iName[line[0].id] + " value: " + iValue[line[0].id];
            current.text = iName[slot[iSlot[line[0].id]]] + " value: " + iValue[slot[iSlot[line[0].id]]];
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (line[0] != null)
        {
            float d = Vector3.Distance(transform.position, line[0].gameObject.transform.position);
            if (d > 2.5f) { Trash(); }
        }
        col = Physics2D.OverlapCircle(transform.position, 0.4f, loot);

        if (col != null)
        {


            Loot g = col.gameObject.GetComponent<Loot>();
            if (g.pickable == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (line[i] == null)
                    {
                        line[i] = g;
                        g.pickable = false;
                        i = 11;
                    }
                }
            }
        }
            
        

    }
    public void Take() {
        Loot g = line[0];
        slot[iSlot[line[0].id]] = line[0].id;
        for (int i = 0; i < 9; i++)
        {
            if (line[i + 1] != null)
            {
                line[i] = line[i + 1];
            }
            else { line[i] = null; }
            
        }
        Destroy(g.gameObject);
    }
    public void Trash()
    {
        Loot g = line[0];
        for (int i = 0; i < 9; i++)
        {
            if (line[i + 1] != null)
            {
                line[i] = line[i + 1];
            }
            else { line[i] = null; }

        }
        g.pickable = true;
    }

}
