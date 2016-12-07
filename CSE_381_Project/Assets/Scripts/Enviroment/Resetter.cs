using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {
    public GameObject objectToReset;
    public Vector3 positionToResetTo;
    
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            objectToReset.transform.position = positionToResetTo;
            objectToReset.GetComponent<MovingStructure>().setupActivePoints(
                objectToReset.GetComponent<MovingStructure>().pathIndex);
        }
    }
}
