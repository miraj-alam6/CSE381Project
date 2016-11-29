using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour {

    public float smallScale;
    public float bigScale;
    public bool bigDirection;
    public float step;
    public bool active = true;
    // Use this for initialization
    void Start () {
        GameManager.instance.addScaleAnimation(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (!active) {
            return;
        }
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
