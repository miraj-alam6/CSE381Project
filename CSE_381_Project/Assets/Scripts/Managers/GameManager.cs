using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<MovingStructure> movingStructures;
    public List<ArrowMovingStructure> arrowMovingStructures;
    public List<Artifact> artifacts;
    public List<ScaleAnimation> scaleAnimations;
    public List<CannonBall> cannonBalls;
    public List<CannonFire> cannons;
    public FPSController playerController;
    public PickUp playerPickup;
    public bool allowedToPause = true;
    public bool gamePaused = false;
    public GameObject canvas;
    public Level currentLevel;
    public float endLevelTime = 3.0f;
    public bool levelDone = false;
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
        artifacts = new List<Artifact>();
        scaleAnimations = new List<ScaleAnimation>();
        cannonBalls = new List<CannonBall>();
        cannons = new List<CannonFire>();
    }

    public void finishLevel()
    {
        allowedToPause = false;
        playerController.stopMovement = true;
        levelDone = true; 
    }
    public void goToNextLevel() {
        if (currentLevel is Level1)
        {
            Debug.Log("arite,what");
            SceneManager.LoadScene("_scenes/Level3");
        }
        else if (currentLevel is Level2)
        {
            SceneManager.LoadScene("_scenes/Level3");
        }

        else if (currentLevel is Level3)
        {
            SceneManager.LoadScene("_scenes/Level2");
        }
    }

    public void addCannonFire(CannonFire cannon) {
        cannons.Add(cannon);
    }

    public void addCannonBall(CannonBall ball)
    {
        cannonBalls.Add(ball);
    }
    public void removeCannonBall(CannonBall ball)
    {
        cannonBalls.Remove(ball);
    }
    public void addScaleAnimation(ScaleAnimation anim)
    {
        scaleAnimations.Add(anim);
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
    public void addArrowMovingStructure(ArrowMovingStructure structure)
    {
        arrowMovingStructures.Add(structure);
    }
    public void addArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
    }
    // Update is called once per frame
    void Update () {
        if (allowedToPause)
        {
            elapsedTime += Time.deltaTime;
            if (Input.GetButtonDown("Pause"))
            {
                if (gamePaused)
                {
                    unpauseGame();
                }
                else {
                    pauseGame();
                }
            }
        }
        else {

            if (levelDone)
            {
                if(Input.GetButton("Submit"))
                {
                    goToNextLevel();
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


        //deactivate all arrow moving structures
        for (int i = 0; i < arrowMovingStructures.Count; i++)
        {
            arrowMovingStructures[i].activated = false;
        }

        //pause all artifacts
        for (int i = 0; i < artifacts.Count; i++)
        {
            artifacts[i].pauseArtifact();
        }

        //pause all scale animations
        for (int i = 0; i < scaleAnimations.Count; i++)
        {
            scaleAnimations[i].active = false;
        }

        Cursor.lockState = CursorLockMode.None;
        canvas.GetComponent<PauseMenuButtonEventHandler>().LoadPauseScreen();
        gamePaused = true;
        //pause all cannonballs
        for (int i = 0; i < cannonBalls.Count; i++) {
            cannonBalls[i].pause();
        }
        //pause all cannons
        for (int i = 0; i < cannons.Count; i++)
        {
            cannons[i].active = false;
        }
        
    }
    public void unpauseGame()
    {
        playerController.active = true;
        playerPickup.active = true;
        //unpause all normal moving structures
        for (int i = 0; i < movingStructures.Count; i++)
        {
            movingStructures[i].activated = true;
        }

        //unpause all arrow moving structures
        for (int i = 0; i < arrowMovingStructures.Count; i++)
        {
            arrowMovingStructures[i].activated = true;
        }
        //unpause all artifacts
        for (int i = 0; i < artifacts.Count; i++)
        {
            artifacts[i].unpauseArtifact();
        }

        //unpause all scale animations
        for (int i = 0; i < scaleAnimations.Count; i++)
        {
            scaleAnimations[i].active = true;
        }
        //unpause all cannonballs
        for (int i = 0; i < cannonBalls.Count; i++)
        {
            cannonBalls[i].unpause();
        }
        //unpause all cannons
        for (int i = 0; i < cannons.Count; i++)
        {
            cannons[i].active = true;
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
