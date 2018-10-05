using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {
    public bool paused;
    public GameObject pausedPanel;

	// Use this for initialization
	void Start () {
        paused = false;
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
	}
    public void PauseGame() {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            pausedPanel.SetActive(false);
        }
        else
        {
            pausedPanel.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
    }
    public void GoToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
