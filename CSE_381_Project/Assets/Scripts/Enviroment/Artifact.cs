using UnityEngine;
using System.Collections;

public class Artifact : MonoBehaviour {
    public string artifactName = "Default";
    Rigidbody rb;
    public bool springOutAttempt; //Once trigger exits, the artifact needs to start trying to go back out
    public bool active = true;
    public float springDistance;
    public float currentDistance;
    //The speed for the object edging closer to the player
    public float springInSpeed = 5;
    //The speed for the object snapping back to default position
    public float springOutSpeed = 5;
    public float velocityLimit = 9;
    public bool isHeld = false;
    public bool extremeCase = false; // when current distance becomes 0, things become really weird.
    public Vector3 lastConfig; //need this to make sure when you pick up an object from last,
    //you get a viable configuration rather than a strange angle from the localEuler
    //make sure you can't drop the piece when the space is 0
    Collider col;
    public Vector3 velocityBeforeSleep;
    public bool hasSpecialMessage;
    public string specialMessage;
    //this boolean tells it to do it

    // Use this for initialization
    void Start() {
        GameManager.instance.addArtifact(this);
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void pauseArtifact() {
        //rb.useGravity = false;
        active = false;
        velocityBeforeSleep = rb.velocity;
        rb.Sleep();
    }

    public void unpauseArtifact()
    {
        //rb.useGravity = true;
        active = true;
        rb.WakeUp();
        rb.velocity = velocityBeforeSleep;
    }

    // Update is called once per frame
    void Update() {
        //The edge case of going straight up into structure causes weird physics that
        //should cause you to drop the object. But when dropping it, it gets a large amount of
        //force applied to it, so this must be cooled down
        
        if (!active) {
            return;
        }
        if (rb.velocity.magnitude > velocityLimit)
        {
            rb.velocity = new Vector3(0, rb.velocity.y + Physics.gravity.y * 3 * Time.deltaTime, 0);
        }
        else {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + Physics.gravity.y*3 * Time.deltaTime, rb.velocity.z);
        }
        if (rb.angularVelocity.magnitude > velocityLimit)
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
		//Wrap everything around with the active condition which becomes false when game is paused
        if (active && isHeld) {
            if (currentDistance == 0)
            {
                
                currentDistance = 2;
                transform.position = Camera.main.transform.position + Camera.main.transform.forward * currentDistance; 
            }
            if (springOutAttempt) {
                springOut();
            }

        }

    }

    public void turnOffConstraints()
    {
        if (rb) { 
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void turnOnConstraints()
    {
        if (rb) { 
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void OnTriggerStay(Collider other) {
        //Wrap everything around with the active condition which becomes false when game is paused

        if (active && isHeld)
        {
            if (!other.tag.Equals("Player") && !other.isTrigger) {
                springIn();
                springOutAttempt = false;
            }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("Player"))
        {
            springOutAttempt = true;
        }
        
    }
    
    //This is part of the spring effect where the piece try to go back out to its
    //normal position
    public void springOut() {
        //Debug.Log("Yo");
        currentDistance += springOutSpeed * Time.deltaTime;
        if (currentDistance >= springDistance)
        {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * springDistance;
            springOutAttempt = false;
        }
        else {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * currentDistance;
        }
        
    }

    //This is part of the spring effect where if it collides with something, it tries to get closer 
    //to the camera to make sure it doesn't collide with and pass thru things.
    public void springIn()
    {
        //Debug.Log("Ho");
        currentDistance -= springInSpeed * Time.deltaTime;
        if (currentDistance <= -1.0f)
        {
            currentDistance = -1.0f;
          //  Camera.main.gameObject.GetComponent<PickUp>().dropObject();
            

           // currentDistance = 2;
           // transform.position = Camera.main.transform.position + Camera.main.transform.forward * currentDistance;
        }
        else {

            transform.position = Camera.main.transform.position + Camera.main.transform.forward * currentDistance;
        }

    }

    //Pick up should give the value for this based on the distance that the piece is
    public void setSpringDistance(float f) {
        currentDistance = springDistance = f;
    }

    //If true is passed in, make it a trigger, if false is
    //passed is, make sure it is not a trigger
    //Make sure this is called before the object is parent in order to ensure that it doesn't 
    //collide and have weird behavior
    public void setColliderTrigger(bool status) {
        col.isTrigger = status;
    }

    public void setIsHeld(bool status) {
        isHeld = status;
        if (isHeld) {
            if (hasSpecialMessage)
            {
                GameManager.instance.currentLevel.updateLevel(specialMessage);
            }
            else { 
                GameManager.instance.currentLevel.updateLevel("pick_up_artifact");
            }
        }
    }

    public void extremeCaseDrop() {

    }
}
