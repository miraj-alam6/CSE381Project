using UnityEngine;
using System.Collections;

public class SwitchPathByTrigger : MonoBehaviour {

    public MovingStructure platformParent;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            platformParent.setupActivePoints(1);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            platformParent.setupActivePoints(0);
        }

    }
    // Update is called once per frame
    void Update () {
	
	}
}
