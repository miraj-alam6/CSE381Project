using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

	public float maxDistance;
	public float travelSpeed;
	Vector3 startPosition;
	Vector3 endPosition;
	PickUp puScript;

	GameObject cannon;
	public bool paused;

	// Use this for initialization
	void Start () {
		//cannon = transform.parent.gameObject;
		paused = false;
		puScript =  Camera.main.GetComponent<PickUp> ();
		startPosition = transform.localPosition;
		endPosition = transform.localPosition + new Vector3 (maxDistance, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused) {
			transform.Translate (Vector3.right * travelSpeed * Time.deltaTime);

			//CannonBall is at max distance, destroy itself
			if (transform.localPosition.x >= maxDistance) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Player")) {
			/*Get pickUp script reference from main camera on collision with player
			puScript.dropObject();
			cannon.playerHitEvent (transform.right, Time.time);
			Destroy (this.gameObject);
			*/
		}

		if (other.tag.Equals ("Artifact")) {
			/*bool isHeld = other.gameObject.GetComponent<Artifact> ().isHeld;
			if (isHeld)
				puScript.dropObject ();

			Rigidbody rb = other.gameObject.GetComponent<Rigidbody> ();
			Vector3 forceVector = new Vector3 (10.0f, 0, 0);
			rb.AddForce (Vector3.up * 5.0f, ForceMode.Acceleration);
			*/
		}
	}
}
