using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControl : MonoBehaviour {
    public int andar;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetFloat("Hp", 20);
        PlayerPrefs.SetFloat("Dex", 5);
        PlayerPrefs.SetFloat("Str", 5);
        PlayerPrefs.SetFloat("Def", 5);

        PlayerPrefs.SetInt("Andar", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("Save", 1);
        if (PlayerPrefs.GetInt("Save") != 0 && PlayerPrefs.GetInt("Save") != 1) {
            PlayerPrefs.SetFloat("Hp", 20);
            PlayerPrefs.SetFloat("Dex", 5);
            PlayerPrefs.SetFloat("Str", 5);
            PlayerPrefs.SetFloat("Def", 5);

            PlayerPrefs.SetInt("Andar", 1);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Save", 0);

        }
        //PlayerPrefs.SetInt("Save", 0);  //For reestarting Progress

        print(PlayerPrefs.GetInt("Save"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play() {
        if (PlayerPrefs.GetInt("Save") == 1)
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }
        
    }
    public void LoadGame()
    {
        GoToScene("Fase"+ PlayerPrefs.GetInt("Andar"));
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Save", 1);
        SceneManager.LoadScene("NewGame");
    }
    public void GoToScene(string f) {
        SceneManager.LoadScene(f);
    }
    public void Erase() {
        PlayerPrefs.SetInt("Save", 0);
    }
}
