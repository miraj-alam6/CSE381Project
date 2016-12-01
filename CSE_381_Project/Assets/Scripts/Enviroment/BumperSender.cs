using UnityEngine;
using System.Collections;

//This is a class for bumper sender, this is for bumping
// with tags that have BumperReceiver tag
// this code is not general purpose. It is quite simple and mainly works
//based on how this level was designed.
public class BumperSender : MonoBehaviour {
    public MovingStructure structure; //this will get its path toggled if you bump into a bumper receiver
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        
        if (other.tag.Equals("BumperReceiver")) {
           
            structure.toggleDestination();
        }
    }


}
