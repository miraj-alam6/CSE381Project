using UnityEngine;
using System.Collections;
using System;

//This class assumes all the affected structures have their default behavior in
//index 0 path, and all the activated behaviors in index 1 path.
public class CompoundObelisk : Obelisk
{

    public Slot slot;
    public int activationGoal;
    public int currentProgressToActivate = 0;
    public MovingStructure[] affectedStructures;



    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateObelisk()
    {
        SoundManager.instance.activateObelisk();
        for (int i = 0; i < affectedStructures.Length; i++)
        {
            affectedStructures[i].setupActivePoints(1);
        }

    }

    public void deactivateObelisk()
    {

        for (int i = 0; i < affectedStructures.Length; i++)
        {
            affectedStructures[i].setupActivePoints(0);
        }

    }

    public void addOne()
    {
        currentProgressToActivate++;
        if (currentProgressToActivate >= activationGoal) {
            currentProgressToActivate = activationGoal;
            activateObelisk();
        }
    }

    public void subtractOne()
    {
        if (currentProgressToActivate >= activationGoal)
        {
            currentProgressToActivate = activationGoal - 1;
            deactivateObelisk();
        }
        else { 
            currentProgressToActivate--;
        }
    }

    public override void processStateChange(int actionNumber)
    {
        if (actionNumber == 0)
        {
            addOne();
        }
        else if (actionNumber == 1)
        {
            subtractOne();
        }
    }
}
