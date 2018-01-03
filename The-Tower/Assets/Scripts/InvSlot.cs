using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvSlot : MonoBehaviour {
    public Inventory inv;
    public Image im;
    public Sprite[] ims;
    public int id;

	// Use this for initialization
	void Start () {
        ims = Resources.LoadAll<Sprite>("Graphics/Icons/ItemIcons");
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        im.sprite = ims[inv.slot[id]];
	}
}
