using UnityEngine;
using System.Collections;
using System;
public class Level1 : Level
{


    public Level1()
    {

    }
    public override bool updateLevel(string message)
    {
        base.updateLevel(message);

        if (message.Equals("pick_up_artifact"))
        {

        }


        return true;
    }


}
