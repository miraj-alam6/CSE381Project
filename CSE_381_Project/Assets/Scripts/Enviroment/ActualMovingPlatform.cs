using UnityEngine;
using System.Collections;

//This class doesn't work, maybe because kinematic collisions aren't correctly detected. Will
//use sensor instead
public class ActualMovingPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Entered");
        if (collisionInfo.gameObject.tag.Equals("Player"))
        {

            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            //Possible idea for future, maybe have a counter that counts down while you stay
            //collided until it decides that you should be parented to the object. 
            //The purpose of this is to delay how quickly your character becomes the same speed
            //as the platform, thus the player needs to make sure they land on the platform more near
            //and that they won't automatically be safe and on top of the platform if they land on the
            //edge
            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            Debug.Log("Colliding with platform start");
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log("Staying");
        if (collisionInfo.gameObject.tag.Equals("Player"))
        {

            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            //Possible idea for future, maybe have a counter that counts down while you stay
            //collided until it decides that you should be parented to the object. 
            //The purpose of this is to delay how quickly your character becomes the same speed
            //as the platform, thus the player needs to make sure they land on the platform more near
            //and that they won't automatically be safe and on top of the platform if they land on the
            //edge
            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            Debug.Log("Colliding with platform");
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Exited platform");
        }
    }
    */
}
