using UnityEngine;
using System.Collections;
using System;
public class Level2 : Level
{
    GameObject resetter;

    int brokenPotsCounter = 0;

    public Level2()
    {
        
    }
    public override bool updateLevel(string message)
    {
        if (message.Equals("activated_ship")) {
            resetter.SetActive(true);
        }
        return true;
    }


}
