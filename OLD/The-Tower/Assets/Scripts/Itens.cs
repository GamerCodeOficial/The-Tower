using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour {
    public string[] Name;
    public int[] ordem;
    public string[] type;


    public float[] dano;
    public float[] coolDown;

    public float[] boost;




	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
}
