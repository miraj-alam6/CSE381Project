using UnityEngine;
using System.Collections;

public class TriggerColliderPlatform : MonoBehaviour {
    public MovingStructure platformParent;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Debug.Log("on platform");
            other.gameObject.transform.parent = platformParent.gameObject.transform;
        }


        //Only parent the artifact to this if it is not already parented AKA if it's not
        //already being held by the player
        if (other.tag.Equals("Artifact"))
        {
            if (other.gameObject.transform.parent == null) {
                other.gameObject.transform.parent = platformParent.gameObject.transform;
            }
            //Debug.Log("on platform");
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.transform.parent = null;
           // Debug.Log("left platform");
        }
        if (other.tag.Equals("Artifact"))
        {
            if (other.gameObject.transform.parent.tag != null &&
                !other.gameObject.transform.parent.tag.Equals("MainCamera") &&
                !other.gameObject.transform.parent.tag.Equals("Slot"))
            {
                other.gameObject.transform.parent = null;
            }
        }
    }
}
