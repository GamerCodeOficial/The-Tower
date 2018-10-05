using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NewGame() {

        LoadGame();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void Quit() {
        Application.Quit();
    }
}
