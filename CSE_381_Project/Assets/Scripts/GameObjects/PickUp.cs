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

    //Distance you can pick up objects from (the length of the raycast which allows you to pick up objects)
    public float rayLength = 3.0f;

    //Distance the object is held from the player
    public float holdDistance = 3.0f;
    public float dropDistance = 0.1f;
    //Speed at which the piece is rotated by QERF key presses
    public float rotateSpeed = 200.0f;

    float heldRotX;
    float heldRotY;
    float heldRotZ;


    //TODO: Move this into a class called artifact:
    public float startRotX;
    public float startRotY;
    public float startRotZ;

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
            print(getRotations());
        }
        //Pick up/drop object
        if (Input.GetKeyDown(KeyCode.T))
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
                    pickUpObject(hit);
                }
            }
            //If holding an object, drop it 
            else
            {
                dropObject();
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
            heldObject = hitObject;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            //This vec3 is the camera position + a distance of magnitude 'holdDistance' in front of the camera
            heldObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
            heldObject.transform.parent = Camera.main.transform;
            //This is bad: Because it is a rigid body, it's rotation gets affected
            //heldObject.transform.localRotation = new Quaternion(heldObject.transform.localRotation.x, heldObject.transform.localRotation.y, heldObject.transform.localRotation.z, heldObject.transform.localRotation.w);
            heldObject.transform.localRotation = Quaternion.Euler(0,0,0);
            heldObject = hitObject;
            if (heldObject.GetComponent<Artifact>()) {
                heldObject.GetComponent<Artifact>().turnOnConstraints();
            }
            //Need to prevent Gimbal Lock I think, so I'm using my own
            //x y and z that are maintained
            heldRotX = heldObject.transform.localEulerAngles.x;
            heldRotY = heldObject.transform.localEulerAngles.y;
            heldRotZ = heldObject.transform.localEulerAngles.z;
        }
    }

    void dropObject()
    {
        if (heldObject.GetComponent<Artifact>())
        {
            heldObject.GetComponent<Artifact>().turnOffConstraints();
        }
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.transform.parent = null;
        heldObject = null;
    }

    void handleRotations()
    {
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            heldRotZ -= rotateSpeed * Time.deltaTime;
           // Debug.Log("" + (rotateSpeed * Time.deltaTime) + "," +
           //  +
           // (heldObject.transform.localEulerAngles.x - rotateSpeed * Time.deltaTime) + "," + heldRotX);

           
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);
        } else if (!Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q))
        {
            heldRotZ += rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

        }

        if (Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.F))
        {
            heldRotY -= rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);
        }
        else if (!Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.F))
        {
            heldRotY += rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

        }
        /*
        if (Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.B))
        {
            heldRotZ -= rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

        }
        else if (!Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.V))
        {
            heldRotZ += rotateSpeed * Time.deltaTime;
            heldObject.transform.localRotation =
                Quaternion.Euler(heldRotX,
                heldRotY,
                heldRotZ);

        }
        */
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
}
