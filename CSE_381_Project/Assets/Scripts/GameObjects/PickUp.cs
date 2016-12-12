using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{

    //Added this comment to update the git 
    //Used for raycasting to pick up objects
	Vector3[] rayCasts = {new Vector3 (0.5f, 0.5f, 0f), new Vector3 (0.4f, 0.4f, 0f), new Vector3 (0.6f, 0.4f, 0f),
		new Vector3 (0.4f, 0.6f, 0f), new Vector3 (0.6f, 0.6f, 0f), new Vector3 (0.4f, 0.5f, 0f), 
		new Vector3 (0.6f, 0.5f, 0f)};

    //Object currently being held
    public GameObject heldObject = null;
    Artifact heldArtifact = null; //this is just the artifact script for the held object
	GameObject obelisk = null;
	GameObject slot = null;
    public AudioClip rotatePieceSound;
    public AudioClip correctRoationSound;
    public AudioClip wrongRoationSound;

    //Distance you can pick up objects from (the length of the raycast which allows you to pick up objects)
    public float rayLength = 3.0f;

	//How much the player must be facing an obelisk to insert an artifact, 1 is directly facing, -1 is directly away
	public float amountFacingObelisk = 0.85f;

    //Distance the object is held from the player
    public float holdDistance = 3.0f;
    public float dropDistance = 0.1f;
    public float minRemoveDistance;

    //Speed at which the piece is rotated by QERF key presses
    public float rotateSpeed = 200.0f;
   
    float heldRotX;
    float heldRotY;
    float heldRotZ;

    float primaryRotateGoalY; 
    //When you are doing a primary rotation, prevent any other rotation from happening
    public bool primaryRotating;
    public bool primaryRotatingClockwise; //this is
    public bool active = true;
    public bool hasUltimatePower = false;
    public GameObject theUltimateArtifact;
    void Start() {
        GameManager.instance.setPlayerPickup(this);
    }

    void Update()
    {
        if (!active) {
            return;
        }
        //If the heldObject is too far from the player, drop it
        checkDistance();
        if (heldObject != null)
        {        
            //Rotates the held artifact based on current key inputs
            handleRotations();
            //Stops velocity collection when object bumps into things
            heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //print(getRotations());
        }
        //Pick up/drop object
        if (Input.GetButtonDown("Fire2"))
        {
            //If not holding an object, attempt to pick up an object being looked at
            if (heldObject == null)
            {
				
				RaycastHit hit = new RaycastHit();
				for(int i = 0; i < rayCasts.Length; i++){
					RaycastHit tempHit;
					Ray tempRay = Camera.main.ViewportPointToRay (rayCasts [i]);
					Physics.Raycast (tempRay, out tempHit, rayLength);
					if (tempHit.transform != null) {
						if (tempHit.transform.gameObject.tag.Equals ("Artifact")) {
							if (hit.transform == null) {
								hit = tempHit;
							} else {
								float hitDistance = Vector3.Distance (Camera.main.transform.position, hit.transform.position);
								float tempHitDistance = Vector3.Distance (Camera.main.transform.position, tempHit.transform.position);
								//print ("Current best: " + hitDistance);
								//print ("New hit: " + tempHitDistance);
								if (hitDistance > tempHitDistance)
									hit = tempHit;
							}
						}
					}
				}
				if (hit.transform != null)
					pickUpObject (hit);
            }
            //If holding an object, drop it 
            else
            {
                dropObject();
            }
        }

		if (Input.GetButtonDown("Fire1")) {
           
         //   print(Vector3.Dot(Camera.main.transform.forward,
         //           (obelisk.transform.position - Camera.main.transform.position).normalized));
            if (heldObject != null && slot != null) {
                //TODO: probably would be more accurate to do this with the insert position rather than
                //the obelisk position
                float dotProduct = Vector3.Dot(Camera.main.transform.forward, 
					(obelisk.transform.position - Camera.main.transform.position).normalized);
				if (dotProduct >= amountFacingObelisk) {
                    //Facing obelisk, inside valid place trigger, while holding an artifact currently
                    insertArtifact ();
				}
			}
		}

        if (hasUltimatePower)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Should summon Juno's artifact");
                pickUpObject(theUltimateArtifact);
            }
        }
    }

    void checkDistance()
    {
        //Don't do this function anymore
        /*
        if (heldObject != null)
        {   //Check the distance from camera to held object, if it's greater than the max hold distance plus a buffer of 0.5, drop it
            if (Vector3.Distance(Camera.main.transform.position, heldObject.transform.position) > holdDistance + dropDistance)
            {
                dropObject();
            } else if (Vector3.Distance(Camera.main.transform.position, heldObject.transform.position) < holdDistance - dropDistance)
            {
                dropObject();
            }
        }
        */
    }

    void pickUpObject(RaycastHit hit)
    {
        GameObject hitObject = hit.transform.gameObject;
        
        //This is checking it is an artifact pickup-able object
        if (!hitObject.tag.Equals("Artifact"))
        {
            return;
        }
        
        else
		{
            primaryRotating = false;
            if (hitObject.transform.parent != null && hitObject.transform.parent.tag.Equals("Slot")){

				Slot artifactSlot = hitObject.transform.parent.GetComponent<Slot>();
                //If it is not inserting
                float removeDistance = Vector3.Distance(Camera.main.transform.position, hitObject.transform.position);
				if (!artifactSlot.getIsInserting() && removeDistance > minRemoveDistance) {
                    SoundManager.instance.removePiece();
					heldObject = hitObject;
                    heldArtifact = heldObject.GetComponent<Artifact>();
                    heldArtifact.turnOnConstraints();
                    heldArtifact.setColliderTrigger(true);
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
					heldObject.GetComponent<Rigidbody> ().isKinematic = true;
                    //Do this before parenting to make sure you don't get a weird angle
                    
                    heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
					heldObject.transform.parent = Camera.main.transform;
                    heldArtifact.setIsHeld(true);
					//heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
					heldObject = hitObject;

                    heldArtifact.setSpringDistance(holdDistance);
                    heldRotX = heldArtifact.lastConfig.x;
                    heldRotY = heldArtifact.lastConfig.y;
                    heldRotZ = heldArtifact.lastConfig.z;
                    //for some reason, if I don't do localEulerAngles here, it messes up
                    //if i try to just use localAngles and use a Quaternion
                    heldObject.transform.localEulerAngles = heldArtifact.lastConfig;
                    artifactSlot.artifactRemoved();
					
				}
			}else {
            	heldObject = hitObject;
                heldObject.GetComponent<Artifact>().turnOnConstraints();
                heldObject.GetComponent<Artifact>().setColliderTrigger(true);
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                //This vec3 is the camera position + a distance of magnitude 'holdDistance' in front of the camera
                heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
            	heldObject.transform.parent = Camera.main.transform;
                heldObject.GetComponent<Artifact>().setIsHeld(true);
                heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
            	heldObject = hitObject;
                heldObject.GetComponent<Artifact>().setSpringDistance(holdDistance);

                heldRotX = 0;
                heldRotY = 0;
                heldRotZ = 0;

            }
        }
    }


    void pickUpObject(GameObject hitObject)
    {

        if (heldObject) {
            return;
        }
        //This is checking it is an artifact pickup-able object
        if (!hitObject.tag.Equals("Artifact")) //possibly take this out
        {
            return;
        }

        else
        {
            primaryRotating = false;
            if (hitObject.transform.parent != null && hitObject.transform.parent.tag.Equals("Slot"))
            {

                Slot artifactSlot = hitObject.transform.parent.GetComponent<Slot>();
                //If it is not inserting
                float removeDistance = Vector3.Distance(Camera.main.transform.position, hitObject.transform.position);
                if (!artifactSlot.getIsInserting() && removeDistance > minRemoveDistance)
                {
                    SoundManager.instance.removePiece();
                    heldObject = hitObject;
                    heldArtifact = heldObject.GetComponent<Artifact>();
                    heldArtifact.turnOnConstraints();
                    heldArtifact.setColliderTrigger(true);
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    //Do this before parenting to make sure you don't get a weird angle

                    heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
                    heldObject.transform.parent = Camera.main.transform;
                    heldArtifact.setIsHeld(true);
                    //heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
                    heldObject = hitObject;

                    heldArtifact.setSpringDistance(holdDistance);
                    heldRotX = heldArtifact.lastConfig.x;
                    heldRotY = heldArtifact.lastConfig.y;
                    heldRotZ = heldArtifact.lastConfig.z;
                    //for some reason, if I don't do localEulerAngles here, it messes up
                    //if i try to just use localAngles and use a Quaternion
                    heldObject.transform.localEulerAngles = heldArtifact.lastConfig;
                    artifactSlot.artifactRemoved();

                }
            }
            else {
                heldObject = hitObject;
                heldObject.GetComponent<Artifact>().turnOnConstraints();
                heldObject.GetComponent<Artifact>().setColliderTrigger(true);
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                //This vec3 is the camera position + a distance of magnitude 'holdDistance' in front of the camera
                heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
                heldObject.transform.parent = Camera.main.transform;
                heldObject.GetComponent<Artifact>().setIsHeld(true);
                heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
                heldObject = hitObject;
                heldObject.GetComponent<Artifact>().setSpringDistance(holdDistance);

                heldRotX = 0;
                heldRotY = 0;
                heldRotZ = 0;

            }
        }
    }

    public void dropObject()
    {
        primaryRotating = false;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Artifact>().setColliderTrigger(false);
        heldObject.GetComponent<Artifact>().turnOffConstraints();
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.transform.parent = null;
        heldObject.GetComponent<Artifact>().setIsHeld(false);
        heldObject = null;
    }

    void handleRotations()
    {
        if (!primaryRotating)
        {
            if (Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.F))
            {
                heldRotZ -= rotateSpeed * Mathf.Cos(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;
                heldRotX += rotateSpeed * Mathf.Sin(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;

                heldObject.transform.localRotation =
                    Quaternion.Euler(heldRotX,
                    heldRotY,
                    heldRotZ);
            }
            else if (!Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.F))
            {
                heldRotZ += rotateSpeed * Mathf.Cos(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;
                heldRotX -= rotateSpeed * Mathf.Sin(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;


                heldObject.transform.localRotation =
                    Quaternion.Euler(heldRotX,
                    heldRotY,
                    heldRotZ);

            }

            if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                SoundManager.instance.rotatePiece();
                primaryRotateGoalY = heldRotY + 90;
                primaryRotating = true;
                primaryRotatingClockwise = true;
            }
            else if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
            {
                SoundManager.instance.rotatePiece();
                primaryRotateGoalY = heldRotY - 90;
                primaryRotating = true;
                primaryRotatingClockwise = false;

            }

        }

        else {
            //This is the code for the primary rotation "animation"
            int direction = -1;
            if (primaryRotatingClockwise) {
                direction = 1;
            }
            float step = rotateSpeed * direction;
            heldRotY += step * Time.deltaTime;
            heldRotX /= 1.1f;
            heldRotZ /= 1.1f;
            if (direction == 1  && heldRotY >= primaryRotateGoalY  ||
                 direction == -1 && heldRotY <= primaryRotateGoalY)
            {
                heldRotY = primaryRotateGoalY;
                heldRotX = heldRotZ = 0;
                primaryRotating = false;

                heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);
            }
            
            heldObject.transform.localRotation =
            Quaternion.Euler(heldRotX,
            heldRotY,
            heldRotZ);
            
            //heldRotY += rotateSpeed * Time.deltaTime;
            
        }
    }

    Vector3 getRotations() {
        //Returns local rotation values in a vec3 if heldObject exists, -1's for all values otherwise
        //These values are used for a comparison with a structure's target rotation
        if (heldObject != null)
        {
            return heldObject.transform.localRotation.eulerAngles;
        }else
        {
            return new Vector3(-1, -1, -1);
        }
    }

	void OnTriggerStay(Collider other){
		//If player is within obelisk collider
		if(other.tag.Equals("Slot")){
			slot = other.gameObject;
			obelisk = slot.transform.parent.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if (slot != null && other.gameObject.Equals (slot)) {
			slot = null;
			obelisk = null;
		}
	}

	void insertArtifact(){
		Slot slotScript = slot.GetComponent<Slot> ();
		slotScript.handleArtifact (heldObject);
	}
}
