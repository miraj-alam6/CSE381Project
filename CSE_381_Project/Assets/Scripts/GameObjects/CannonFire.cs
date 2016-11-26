using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	float timeCount = 0.0f;
	public float fireDelay;
	public GameObject cannonBall;
	bool paused;


	bool playerHit;
	bool artifactHit;

	void Start() {
		paused = false;
		playerHit = false;
		artifactHit = false;
	}

	// Update is called once per frame
	void Update () {
		if (!paused) {
			timeCount += Time.deltaTime;

			//If the time between fires is equal to or greater than the delay, then fire
			if (timeCount >= fireDelay) {
				Instantiate (cannonBall, transform.position, transform.localRotation);
				timeCount = 0.0f;
			}

			if (playerHit)
				//playerHitEvent ();

			if (artifactHit)
				artifactHitEvent ();
		}
	}

	public void playerHitEvent(Vector3 hitDirection, float timeOfHit){
		
	}

	public void artifactHitEvent (){
	
	}

}
