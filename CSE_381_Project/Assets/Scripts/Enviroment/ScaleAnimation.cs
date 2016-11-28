using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour {

    public float smallScale;
    public float bigScale;
    public bool bigDirection;
    public float step;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //this is for scaling up
        if (bigDirection) {
            transform.localScale = 
                new Vector3(transform.localScale.x + step * Time.deltaTime,
                transform.localScale.y, transform.localScale.z + step * Time.deltaTime);
            if (transform.localScale.x > bigScale) {
                bigDirection = false;
            }
        }
        //this is for scaling down
        else
        {
            transform.localScale =
                new Vector3(transform.localScale.x - step * Time.deltaTime,
                transform.localScale.y, transform.localScale.z - step * Time.deltaTime);
            if (transform.localScale.x < smallScale)
            {
                bigDirection = true;
            }
        }
	}
}
