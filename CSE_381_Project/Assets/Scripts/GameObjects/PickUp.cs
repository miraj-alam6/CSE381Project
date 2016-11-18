using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    //Used for raycasting to pick up objects
    Ray ray;
    RaycastHit hit;
    Vector3 cameraCenter = new Vector3(0.5f, 0.5f, 0f); //screen center

    //Object currently being held
    GameObject heldObject = null;
	GameObject obelisk = null;
	GameObject slot = null;

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

    void Update()
    {
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
            
            //Draw ray for testing purposes
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            ray = Camera.main.ViewportPointToRay(cameraCenter);
            //If not holding an object, attempt to pick up an object being looked at
            if (heldObject == null)
            {
                //If the raycast hits, try to pick up the object (dependent on if it has a valid tag)
                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    // float rayDistance = Vector3.Distance(Camera.main.transform.position, hit.transform.position);
                    // if (rayDistance > 3) { 
                    pickUpObject(hit);
                   // }
                }
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
    }

    void checkDistance()
    {
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
			if(hitObject.transform.parent != null && hitObject.transform.parent.tag.Equals("Slot")){
				
				Slot artifactSlot = hitObject.transform.parent.GetComponent<Slot>();
                //If it is not inserting
                float removeDistance = Vector3.Distance(Camera.main.transform.position, hitObject.transform.position);
                if (!artifactSlot.getIsInserting() && removeDistance > minRemoveDistance) {

					heldObject = hitObject;
                    heldObject.GetComponent<Artifact>().turnOnConstraints();
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
					heldObject.GetComponent<Rigidbody> ().isKinematic = false;
					heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
					heldObject.transform.parent = Camera.main.transform;
					//heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
					heldObject = hitObject;
                    heldRotX = heldObject.transform.localEulerAngles.x;
                    heldRotY = heldObject.transform.localEulerAngles.y;
                    heldRotZ = heldObject.transform.localEulerAngles.z;
                       
                    artifactSlot.artifactRemoved();
					
				}
			}else {
            	heldObject = hitObject;
                heldObject.GetComponent<Artifact>().turnOnConstraints();
                heldObject.GetComponent<Rigidbody>().useGravity = false;
            	//This vec3 is the camera position + a distance of magnitude 'holdDistance' in front of the camera
            	heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
            	heldObject.transform.parent = Camera.main.transform;
            	heldObject.transform.localRotation = new Quaternion(0.0f, 0, 0, heldObject.transform.rotation.w);
            	heldObject = hitObject;

                heldRotX = 0;
                heldRotY = 0;
                heldRotZ = 0;

            }
        }
    }

    public void dropObject()
    {
        heldObject.GetComponent<Artifact>().turnOffConstraints();
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.transform.parent = null;
        heldObject = null;
    }

    void handleRotations()
    {
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            heldRotZ -= rotateSpeed * Mathf.Cos(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;
            heldRotX += rotateSpeed * Mathf.Sin(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;

            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);
        }
        else if (!Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q))
        {
            heldRotZ += rotateSpeed * Mathf.Cos(heldRotY * Mathf.Deg2Rad) * Time.deltaTime;
            heldRotX -= rotateSpeed * Mathf.Sin(heldRotY* Mathf.Deg2Rad) * Time.deltaTime;

            
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

        }

        if (Input.GetKeyDown(KeyCode.R) && !Input.GetKey(KeyCode.F))
        {
            heldRotX = heldRotZ = 0;
            heldRotY += 90;
            //heldRotY -= rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);
        }
        else if (!Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.F))
        {
            heldRotX = heldRotZ = 0;
            heldRotY -= 90;
            //heldRotY += rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

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
