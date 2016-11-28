using UnityEngine;
using System.Collections;

//Message events need to start deactivaated, and then an action in gameplay will activate them
//This class will activate messages based on what stuff has happened in the game through
//the messages sent to current level
public class MessageEvent : MonoBehaviour
{
    public GameObject messageToTrigger;
    public float messageStartDelay = 1.0f;
    public float timeMessageStays = 5.0f;
    private bool messageCanAppear = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        messageStartDelay -= Time.deltaTime;
        if (messageStartDelay > 0 || timeMessageStays <= 0)
        {
            messageToTrigger.SetActive(false);
            return;
        }
        else {
            if (!GameManager.instance.gamePaused) { 
                messageToTrigger.SetActive(true);
                timeMessageStays -= Time.deltaTime;
            }
        }
        
    }

}
