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
            if (!messagesActivated[0])
            {
                messagesToActivate[0].gameObject.SetActive(true);
                messagesActivated[0] = true;
            }
            if (!messagesActivated[1])
            {
                messagesToActivate[1].gameObject.SetActive(true);
                messagesActivated[1] = true;
            }
        }

        if (message.Equals("second_artifact"))
        {
            if (!messagesActivated[2])
            {
                messagesToActivate[2].gameObject.SetActive(true);
                messagesActivated[2] = true;
            }
        }

        if (message.Equals("level_done"))
        {
            
            GameManager.instance.finishLevel();
            
        }


        return true;
    }


}
