using UnityEngine;
using System.Collections;

public class Artifact : MonoBehaviour {
    public string artifactName = "Default";
    Rigidbody rb;
	// Use this for initialization
	void Start () {
		
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOffConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void turnOnConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
    }
}
