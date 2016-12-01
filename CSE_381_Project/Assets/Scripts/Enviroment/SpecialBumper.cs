using UnityEngine;
using System.Collections;

public class SpecialBumper : MonoBehaviour {
    public MovingStructure structure;
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerStay(Collider other) {
        if (other.tag.Equals("Ship")) {
            structure.activated = false;
        }
    }

    void OnTriggerExit(Collider other) {
        structure.activated = true;
    }
}
