using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEventHandler : MonoBehaviour {

	public GameObject mainScreen;
	public GameObject levelSelectScreen;
	public GameObject howToPlayScreen;
	private GameObject currentScreen;

	// Initialized to main screen display, others disabled
	void Start () {
		mainScreen.SetActive (true);
		levelSelectScreen.SetActive (false);
		howToPlayScreen.SetActive (false);
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

	public void LoadNewGame(){
		print ("CLICKED");
		SceneManager.LoadScene ("_scenes/Level1");
	}
}
