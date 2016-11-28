using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<MovingStructure> movingStructures;
    public List<Artifact> artifacts;
    public FPSController playerController;
    public PickUp playerPickup;
    public bool allowedToPause = true;
    public bool gamePaused = false;
    public GameObject canvas;
    public Level currentLevel;
    private float elapsedTime; //Time that has been elasped in the level in seconds I think.
    //if a single game manager is used throughout the game, then make sure
    //to reset the elapsed time at start of the level
    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        }

        else if(instance != this) {
            Destroy(this.gameObject);
        }
	}

    void Start() {

    }

    //Call this in between levels if we chooose to not destroy on load
    public void resetAllFields() {
        playerController = null;
        playerPickup = null;
        movingStructures = new List<MovingStructure>();
    }

    public void setPlayerController(FPSController player) {
        playerController = player;
    }

    public void setPlayerPickup(PickUp pickup)
    {
        playerPickup = pickup;
    }

    public void addMovingStructure(MovingStructure structure)
    {
        movingStructures.Add(structure);
    }
    public void addArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
    }
    // Update is called once per frame
    void Update () {
        if (allowedToPause) {
            elapsedTime += Time.deltaTime;
            if (Input.GetButtonDown("Pause")) {
                if (gamePaused){
                    unpauseGame();
                }
                else{
                    pauseGame();
                }
            }
        }
    }

    public void pauseGame() {
        playerController.active = false;
        playerPickup.active = false;

        //deactivate all movingStructures
        for (int i = 0; i < movingStructures.Count; i++) {
            movingStructures[i].activated = false;
        }

        //pause all artifacts
        for (int i = 0; i < artifacts.Count; i++)
        {
            artifacts[i].pauseArtifact();
        }
        Cursor.lockState = CursorLockMode.None;
        canvas.GetComponent<PauseMenuButtonEventHandler>().LoadPauseScreen();
        gamePaused = true;
    }
    public void unpauseGame()
    {
        playerController.active = true;
        playerPickup.active = true;
        for (int i = 0; i < movingStructures.Count; i++)
        {
            movingStructures[i].activated = true;
        }

        //unpause all artifacts
        for (int i = 0; i < artifacts.Count; i++)
        {
            artifacts[i].unpauseArtifact();
        }
        Cursor.lockState = CursorLockMode.Locked;
        canvas.GetComponent<PauseMenuButtonEventHandler>().ClosePauseScreen();
        gamePaused = false;
    }

    //Need this to close pause screen through a button, and prevent infinite recursion
    public void unpauseLogic()
    {
        playerController.active = true;
        playerPickup.active = true;
        for (int i = 0; i < movingStructures.Count; i++)
        {
            movingStructures[i].activated = true;
        }

        //unpause all artifacts
        for (int i = 0; i < artifacts.Count; i++)
        {
            artifacts[i].unpauseArtifact();
        }
        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
    }


    public void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /*
    public void setCurrentLevel() {
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName.Equals("_scenes/Level0") || levelName.Equals("Level0"))
        {
            currentLevel = new Level0();
        }

    }
    
    public Level getCurrentLevel() {
        return currentLevel;
    }
    */
}
