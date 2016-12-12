using UnityEngine;
using System.Collections;
using System;
public class Level1 : Level
{

    int brokenPotsCounter = 0;

    public Level1()
    {

    }
    public override bool updateLevel(string message)
    {
        base.updateLevel(message);

        if (message.Equals("pick_up_artifact"))
        {
        }

        if (message.Equals("second_artifact"))
        {
        }

        if (message.Equals("level_done"))
        {
            GameManager.instance.finishLevel();
            
        }


        return true;
    }


}
