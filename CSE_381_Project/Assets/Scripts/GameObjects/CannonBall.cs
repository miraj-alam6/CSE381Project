using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

	PickUp puScript;
    FPSController player;
	public float travelSpeed;
	public float maxDistance;
    public float artifactImpactMagnitude;
    public float playerImpactMagnitude;
    public bool paused;

    Vector3 fireDirection;
    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        this.GetComponent<Rigidbody>().velocity = fireDirection * travelSpeed;
		paused = false;
		puScript =  Camera.main.GetComponent<PickUp> ();
        player = Camera.main.transform.parent.GetComponent<FPSController>();
	}

	// Update is called once per frame
	void Update () {
		if (!paused) {
			//CannonBall is at max distance, destroy itself
			if (Vector3.Distance(startPosition, transform.position) >= maxDistance) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Player")) {
            Vector3 forceDirection = (other.gameObject.transform.position - transform.position);
            float facingCheck = Vector3.Dot(fireDirection.normalized, forceDirection.normalized);
            if (facingCheck >= 0)
            {
                forceDirection.y = 0.0f;
                forceDirection.Normalize();
                forceDirection *= playerImpactMagnitude;
                if(puScript.heldObject != null)
                    puScript.dropObject();
                player.push(forceDirection);

                Destroy(this.gameObject);
            }
        }


		if (other.tag.Equals ("Artifact")) {
            Vector3 forceDirection = (other.gameObject.transform.position - transform.position);
            
            //Check if the artifact collided with the front of the cannonBall, not the back 
            float facingCheck = Vector3.Dot(fireDirection.normalized, forceDirection.normalized);
            if (facingCheck >= 0)
            {
                //Drop the artifact if it is held
                bool isHeld = other.gameObject.GetComponent<Artifact>().isHeld;
                if (isHeld)
                    puScript.dropObject();
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

                //forceDirection is the direction of the collision, set y to 0 to avoid edge case direct-up forces and normalize
                forceDirection.y = 0.0f;
                forceDirection.Normalize();
                //Multiply forceDirection by impactMagnitude 
                rb.AddForce(forceDirection * artifactImpactMagnitude, ForceMode.Impulse);
                Destroy(this.gameObject);
            }
        }
	}

    public void setFireDirection(Vector3 vec)
    {
        fireDirection = vec;
    }
}

