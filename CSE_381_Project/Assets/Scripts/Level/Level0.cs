using UnityEngine;
using System.Collections;
using System;
public class Level0 : Level {
    
    int brokenPotsCounter = 0;
    
    public Level0()
    {
        
    }
    public override bool updateLevel(string message) {
        base.updateLevel(message);

        if (message.Equals("pick_up_artifact")) {
            if (!messagesActivated[0]) {
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

        if (message.Equals("third_artifact"))
        {
            if (!messagesActivated[3])
            {
                messagesToActivate[3].gameObject.SetActive(true);
                messagesActivated[3] = true;
            }
        }


        return true;
    }


}
