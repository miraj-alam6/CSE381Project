using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
    public float speed;
    //public bool active;
	// Use this for initialization
	void Start () {
       	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.gamePaused) {
            transform.Rotate(Vector3.right, speed * Time.deltaTime); 
        }
	}
}
