using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<MovingStructure> movingStructures;
    public List<Artifact> artifacts;
    public FPSController playerController;
    public PickUp playerPickup;
    public bool allowedToPause = true;
    public bool gamePaused = false;
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
        gamePaused = false;
    }
}
