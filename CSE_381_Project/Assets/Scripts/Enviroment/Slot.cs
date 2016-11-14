using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {
    public Obelisk parent;
    public string[] artifactNames;
    //First 3 coordinates refer to the rotation for the artificat, 4th coordinate is the index
    //of the artifact in the list of possible artifacts.
    public Vector4[] configurations;
    public int actionNumber;
	// Use this for initialization
	void Start () {
        if (actionNumber == -1) {
            Debug.Log("ERROR in Setup: actionNumber cannot be -1");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CheckConfiguration(string artifactName, Vector3 rotation) {
        int i;
        for (i = 0; i < artifactNames.Length; i++ ) {
            if (artifactNames[i].Equals(artifactName)) {

                break;
            }

        }
        if(i == artifactNames.Length) {
            return;
        }
        if (configurations[i].x == rotation.x && configurations[i].y == rotation.y
            && configurations[i].z == rotation.z)
        {
            parent.processStateChange(actionNumber);
        }
      //  else {
      //      return;
      //  }

    }


    public void OnTriggerStay(Collider other) {
        if (Input.GetButtonDown("Fire1")) {
            parent.processStateChange(actionNumber);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Let me borrow");
            parent.processStateChange(-actionNumber);
        }
    }

}
