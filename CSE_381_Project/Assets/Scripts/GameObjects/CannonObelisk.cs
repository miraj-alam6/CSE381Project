using UnityEngine;
using System.Collections;
using System;

//This class assumes all the affected structures have their default behavior in
//index 0 path, and all the activated behaviors in index 1 path.
public class CannonObelisk : Obelisk
{

    public Slot slot;
    public CannonFire[] affectedCannons;



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
        for (int i = 0; i < affectedCannons.Length; i++)
        {
			affectedCannons[i].isOn = !affectedCannons[i].isOn;
        }

    }

    public void deactivateObelisk()
    {

        for (int i = 0; i < affectedCannons.Length; i++)
        {
            affectedCannons[i].isOn = !affectedCannons[i].isOn;
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
}
