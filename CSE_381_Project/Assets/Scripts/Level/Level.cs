using UnityEngine;
using System.Collections;

public abstract class Level: MonoBehaviour{

    public bool levelDone = false;
    public bool hintsOn = true;
    public MessageEvent[] messagesToActivate;
    public bool[] messagesActivated;

    //IMPORTANT MISTAKE to keep in mind, if you put stuff in a constructor it
    //will always be called for Monobehavior stuff. Thus only
    //put initialization stuff in the Start function
    public Level() {
        //don't use this        
    }

    void Start() {

        messagesActivated = new bool[messagesToActivate.Length];
        for (int i = 0; i < messagesToActivate.Length; i++)
        {
            messagesActivated[i] = false;
        }
    }
    public virtual bool updateLevel(string message) {
        if (message.Equals("restart"))
        {
 
        }
        return true;
    }
}
