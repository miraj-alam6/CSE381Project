using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuButtonEventHandler : MonoBehaviour {

    public GameObject pauseScreenBackground;
    public GameObject pauseScreen;
    public GameObject helpScreen;
    private GameObject currentScreen;
    public Text levelNameText;
    public void Start() {
        currentScreen = pauseScreen;
        if (GameManager.instance.currentLevel is Level1){
            levelNameText.text = "Level 1 Artifacts and Obelisk";
        }
        if (GameManager.instance.currentLevel is Level4)
        {
            levelNameText.text = "Level 4: The Big Picture";
        }
        if (GameManager.instance.currentLevel is Level2)
        {
            levelNameText.text = "Level 2: Cannon Mayhem";
        }
        if (GameManager.instance.currentLevel is Level3)
        {
            levelNameText.text = "Level 3: The Great Door";
        }
        if (GameManager.instance.currentLevel is Level5)
        {
            levelNameText.text = "Level 5: The Power of Heka";
        }

    }

    public void ClosePauseScreen()
    {
        GameManager.instance.unpauseLogic();
        currentScreen = null;
        pauseScreen.SetActive(false);
        helpScreen.SetActive(false);
        pauseScreenBackground.SetActive(false);
        
    }

    public void LoadPauseScreen(){
        if (currentScreen) { 
		    currentScreen.SetActive (false);
        }
        pauseScreen.SetActive (true);
		currentScreen = pauseScreen;
        pauseScreenBackground.SetActive(true);
    }

	public void LoadMainScreen(){
        GameManager.instance.allowedToPause = false;
        SceneManager.LoadScene ("_scenes/Menu");
	}

	public void LoadHelpScreen(){
        if (currentScreen) { 
		    currentScreen.SetActive (false);
        }
        helpScreen.SetActive (true);
		currentScreen = helpScreen;
	}

    public void RestartLevel() {
        GameManager.instance.restartLevel();
    }

	public void Exit(){
        Application.Quit();
	}
}
