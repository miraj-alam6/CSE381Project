using UnityEngine;
using System.Collections;
using System;

//This class assumes all the affected structures have their default behavior in
//index 0 path, and all the activated behaviors in index 1 path.
public class ArrowObelisk : Obelisk
{

    public Slot[] slots;
    public ArrowMovingStructure[] affectedStructures;
    public Vector3 lastEffect; //add or subtract this to the accumulatedEffect based on if
    //artifact was removed or placed
    public Vector3 accumulatedEffect;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setLastEffect(Vector3 v)
    {
        lastEffect = v;
    }

    public void activateObelisk()
    {
        SoundManager.instance.activateObelisk();
        for (int i = 0; i < affectedStructures.Length; i++)
        {
            accumulatedEffect += lastEffect;
            affectedStructures[i].accumulatedGoalDistance = accumulatedEffect;
            affectedStructures[i].goalPosition = affectedStructures[i].defaultPosition +
                affectedStructures[i].accumulatedGoalDistance;
            affectedStructures[i].moving = true;
        }

    }

    public void deactivateObelisk()
    {
        Debug.Log("Yeehaw");

        for (int i = 0; i < affectedStructures.Length; i++)
        {
            accumulatedEffect -= lastEffect;
            affectedStructures[i].accumulatedGoalDistance = accumulatedEffect;
            affectedStructures[i].goalPosition = affectedStructures[i].defaultPosition +
                affectedStructures[i].accumulatedGoalDistance;
            affectedStructures[i].moving = true;
        }

    }

    public override void processStateChange(int actionNumber)
    {
        if (actionNumber == 0)
        {
            activateObelisk();
        }
        else if (actionNumber == 1)
        {
            deactivateObelisk();
        }
    }

    //This will set up lastEffect, while activate/deactive will decide if the effect should be
    //subtracted or added to the current
    public bool attemptEffect(Vector3 v) {
        Vector3 tempaccumulationCheck;
        //check here if this temp value is valid or not
        //if it is return false, so that Slot knows not to let in the piece.
        //the false feauture check is NOT IMPLEMENTED YET
        lastEffect = v;
        return true;
    }
}
