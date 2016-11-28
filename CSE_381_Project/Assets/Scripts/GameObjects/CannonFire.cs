using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	float timeCount = 0.0f;
	public float fireDelay;
	public GameObject cannonBall;
	bool paused;

	void Start() {
		paused = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timeCount += Time.deltaTime;

            //If the time between fires is equal to or greater than the delay, then fire
            if (timeCount >= fireDelay)
            {
                //parentCannon.GetComponent<Animator> ().Play("Cube|Fire");
                Object cBallInstanceObject = Instantiate(cannonBall, transform.position, transform.localRotation);
                GameObject cBallInstance = (GameObject)cBallInstanceObject;
                cBallInstance.GetComponent<CannonBall>().setFireDirection(transform.forward);
                timeCount = 0.0f;
            }
        }
    }

}
