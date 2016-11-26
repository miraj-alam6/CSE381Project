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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.transform.parent = null;
           // Debug.Log("left platform");
        }
<<<<<<< HEAD
        if (other.tag.Equals("Artifact"))
        {
            if (other.gameObject.transform.parent.tag != null &&
                !other.gameObject.transform.parent.tag.Equals("MainCamera") &&
                !other.gameObject.transform.parent.tag.Equals("Slot"))
            {
                other.gameObject.transform.parent = null;
            }
        }
=======
>>>>>>> d5b5554e00396d45d3ccdf08a525d44f2a149cd0
    }
}
