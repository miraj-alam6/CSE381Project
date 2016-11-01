using UnityEngine;
using System.Collections;

public class TriggerColliderPlatform : MonoBehaviour {
    public MovingPlatform platformParent;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("on platform");
            other.gameObject.transform.parent = platformParent.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.transform.parent = null;
            Debug.Log("left platform");
        }
    }
}
