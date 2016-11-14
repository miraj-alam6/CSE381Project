using UnityEngine;
using System.Collections;

public class Obelisk : MonoBehaviour {

    public Slot[] slots;
    public bool[] slotsActivated;
    public MovingStructure[] affectedStructures;
    
    //This will be a multiple of 5, representing how many pieces together
    //will reach one goal.
    public int multiArtifactGoal;
    int accumalatedArtifactGoal;
    public int defaultPathForMulti;

    //Quadruplet where x = activateOrDeactivate, y = ID of action, z = index of structure to affect,
    //and w is the path # to set it to
    //ID of action ranges from 1 to 4 normally. 5 means that it's an accumulated action.
    public Vector4[] stateChanges;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void processStateChange(int actionID) {
        if (actionID == -5)
        {

            if (accumalatedArtifactGoal >= multiArtifactGoal)
            {
                reverseStateChanges();
            }
            accumalatedArtifactGoal -= 5;
            
            return;
        }

        if (actionID == 5) {
            multiArtifactGoal += 5;
            if (accumalatedArtifactGoal >= multiArtifactGoal) {
                //Code for doing all state Changes
                doAllStateChanges();
                return;
            }
        }

        //This is the main part
        //If you reached here that means you put a piece in, and the rest of the
        //pieces must pop out.

        // this is the when taking a piece out
        if (actionID < 0)
        {
            for (int i = 0; i < stateChanges.Length; i++)
            {
                if (stateChanges[i].y == -actionID)
                {
                    /*I don't think i need this x, can use it for something else
                    if ((int)stateChanges[i].x == 0)
                    {
                        affectedStructures[(int)stateChanges[i].z].activated = false;
                    }
                    else {
                        affectedStructures[(int)stateChanges[i].z].activated = true;
                    }
                    */

                    affectedStructures[(int)stateChanges[i].z].setupActivePoints(
                        affectedStructures[(int)stateChanges[i].z].defaultPathIndex);
                }
            }
        }
        //this is when putting a piece in
        else {
            for (int i = 0; i < stateChanges.Length; i++)
            {
                if (stateChanges[i].y == actionID)
                {
                    /* I don't think i need this x, can use it for something else
                    if ((int)stateChanges[i].x == 0)
                    {
                        affectedStructures[(int)stateChanges[i].z].activated = false;
                    }
                    else {
                        affectedStructures[(int)stateChanges[i].z].activated = true;
                    }
                    */
                    affectedStructures[(int)stateChanges[i].z].setupActivePoints(
                        (int)stateChanges[i].w);
                }
            }
        }
        
        
    }

    
    public void doAllStateChanges() {
        for (int i = 0; i < stateChanges.Length; i++)
        {
            if ((int)stateChanges[i].x == 0)
            {
                affectedStructures[(int)stateChanges[i].z].activated = false;
            }
            else {
                affectedStructures[(int)stateChanges[i].z].activated = true;
            }
            
            affectedStructures[(int)stateChanges[i].z].setupActivePoints(
                (int)stateChanges[i].w);
        }
    }

    public void reverseStateChanges()
    {
        for (int i = 0; i < stateChanges.Length; i++)
        {
            if ((int)stateChanges[i].x == 0)
            {
                affectedStructures[(int)stateChanges[i].z].activated = true;
            }
            else {
                affectedStructures[(int)stateChanges[i].z].activated = false;
            }

            affectedStructures[(int)stateChanges[i].z].setupActivePoints(defaultPathForMulti);
        }
    }
}
