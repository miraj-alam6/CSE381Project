using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    public float speed;
    public GameObject actualPlatform;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
        this.transform.Translate(new Vector3(1,0,0) * 2 *Time.deltaTime);
	}
}
