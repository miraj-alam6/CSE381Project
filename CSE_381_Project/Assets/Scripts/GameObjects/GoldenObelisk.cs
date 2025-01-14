using UnityEngine;
using System.Collections;
using System;

//This class assumes all the affected structures have their default behavior in
//index 0 path, and all the activated behaviors in index 1 path.
public class GoldenObelisk : Obelisk
{

    public Slot slot;
	public GoldenDoorScript gdScript;

    public void activateObelisk()
    {
        SoundManager.instance.activateObelisk();
		gdScript.isActivated = true;

    }

    public void deactivateObelisk()
    {
		gdScript.isActivated = false;
    }

    public override void processStateChange(int actionNumber)
    {
        if (actionNumber == 0) {
			activateObelisk();
        }
        else if(actionNumber == 1)
        {
			deactivateObelisk();
        }
    }
}
