﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEventHandler : MonoBehaviour {

	public GameObject mainScreen;
	public GameObject levelSelectScreen;
	public GameObject howToPlayScreen;
    public GameObject creditsScreen;
    private GameObject currentScreen;
    public Texture2D customCursor;
	// Initialized to main screen display, others disabled
	void Start () {
        turnOnCursor();
		mainScreen.SetActive (true);
		levelSelectScreen.SetActive (false);
		howToPlayScreen.SetActive (false);
        creditsScreen.SetActive(false);
        currentScreen = mainScreen;
        
	}

	public void LoadLevelSelectScreen(){
		currentScreen.SetActive (false);
		levelSelectScreen.SetActive (true);
		currentScreen = levelSelectScreen;
	}

	public void LoadMainScreen(){
		currentScreen.SetActive (false);
		mainScreen.SetActive (true);
		currentScreen = mainScreen;
	}

	public void LoadHowToPlayScreen(){
		currentScreen.SetActive (false);
		howToPlayScreen.SetActive (true);
		currentScreen = howToPlayScreen;
	}


    public void LoadCreditsScreen()
    {
        currentScreen.SetActive(false);
        creditsScreen.SetActive(true);
        currentScreen = creditsScreen;
    }
    public void LoadNewGame(){
		print ("CLICKED");
        turnOffCursor();
        SceneManager.LoadScene ("_scenes/Level1");
	}

	public void LoadLevel1() {
		turnOffCursor();
		SceneManager.LoadScene ("_scenes/Level1");
	}

	public void LoadLevel2() {
		turnOffCursor();
		SceneManager.LoadScene ("_scenes/Level3");
	}

	public void LoadLevel3() {
		turnOffCursor();
		SceneManager.LoadScene ("_scenes/Level4");
	}

	public void LoadLevel4() {
		turnOffCursor();
		SceneManager.LoadScene ("_scenes/Level2");
	}

	public void LoadLevel5() {
		turnOffCursor();
		SceneManager.LoadScene ("_scenes/Level5");
	}



    public void turnOnCursor()
    {
        Debug.Log("Why");
        Cursor.lockState = CursorLockMode.None;
       // Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }

    public void turnOffCursor()
    {

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }
}
