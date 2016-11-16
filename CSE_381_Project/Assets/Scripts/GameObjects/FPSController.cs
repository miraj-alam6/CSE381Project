using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
//The above line raises a compiler side error if there is no Character controller on the game
//object that this script is attached to. Basically it forces a characte controller to show up
//every time you attach this script to a game object.

public class FPSController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed;
    //Vector3 velocity = Vector3.zero; //This is for our bookkeeping so that we can deal with gravity
    //moved this up here as opposed to later
    float verticalVelocity = 0; //Just need this for bookkeeping velocity because only thing physics based
    float rotPitch = 0;
    public float rotatePitchRange = 60.0f; // represents 60 degrees
                                           // Use this for initialization
    CharacterController cc;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc  = GetComponent<CharacterController>();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log("Staying");
        if (collisionInfo.gameObject.tag.Equals("MovePlat")) {

            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            //Possible idea for future, maybe have a counter that counts down while you stay
            //collided until it decides that you should be parented to the object. 
            //The purpose of this is to delay how quickly your character becomes the same speed
            //as the platform, thus the player needs to make sure they land on the platform more near
            //and that they won't automatically be safe and on top of the platform if they land on the
            //edge
            // IMPLEMENT EXTRA FEAUTURE HERE IN FUTURE
            Debug.Log("Colliding with platform");
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag.Equals("MovePlat"))
        {
            Debug.Log("Exited platform");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //How to Rotate
        //Left right rotation
        float rotYaw = Input.GetAxis("Mouse X") * mouseSensitivity; //
        transform.Rotate(0, rotYaw, 0); // we directly change Yaw of our character for us to move
        //to places. Rotate about the y axis.

        // Up down rotation
        //float rotPitch = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //However, do not directly change the pitch of the character because you don't want the
        //character to slant up when doing this.
        //Rotate the camera instead. However, limit how much you can rotate pitch because
        //one's head cannot realisticaly tilt so far that they flip.
        //float currentPitch = Camera.main.transform.rotation.eulerAngles.x;
        //float desiredPitch = currentPitch - rotPitch; //we're doing this instead of simply calling
        //rotate so that we can clamp and stuff
        rotPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity; //do -= instead of += 
                                                                 //here to fix inverted axis
        rotPitch = Mathf.Clamp(rotPitch, -rotatePitchRange, rotatePitchRange);

        // Camera.main.transform.Rotate(-rotPitch, 0, 0); //rotate about the x axis to look up and down
        Camera.main.transform.localRotation = Quaternion.Euler(rotPitch, 0, 0);
        //Note, you need to use localRotation above or else things are weird

        //How to Move
        float forwardSpeed = speed * Input.GetAxis("Vertical"); //to move forward AND backward(if negative)
        float strafeSpeed = speed * Input.GetAxis("Horizontal"); //to move right and left(if negative)

        
        // Vector3 speedVector = new Vector3(strafeSpeed, Physics.gravity.y, forwardSpeed); //forward movement is across z axis 
        //above is wrong because gravity is not constant.

        //Check if you are touching the ground.
        if (cc.isGrounded)
        {
            verticalVelocity = 0;
            //Flying cheat
            if (Input.GetKey(KeyCode.M))
            {
                verticalVelocity = jumpSpeed;
            }
            if (Input.GetButtonDown("Jump"))
            {

                verticalVelocity = jumpSpeed;
            }
        }

        //if not on ground, increase vertical velocity
        else {
            //Flying cheat
            if (Input.GetKey(KeyCode.M))
            {
                verticalVelocity = jumpSpeed;
            }
            else { 
                verticalVelocity += (Physics.gravity.y) * Time.deltaTime; //add 9.81 every second
            }
            //  Debug.Log(verticalVelocity);
        }
        //Handle jumping


        //Check jumping again
        if (cc.isGrounded)
        {
            verticalVelocity = 0;
            if (Input.GetButtonDown("Jump"))
            {

                verticalVelocity = jumpSpeed;
            }
        }
        Vector3 speedVector = new Vector3(strafeSpeed, verticalVelocity, forwardSpeed); //forward movement is across z axis 

        //Before moving, apply the rotation so that pressing forward and such will actually make
        //you go forward
        //IMPORTANT: this gives an error speedVector = speedVector * transform.rotation;
        //instead change the order of the factors. I'm guessing this is because of how
        //matrix operations work.
        speedVector = transform.rotation * speedVector;

        //cc.SimpleMove(speedVector); //Simple move is fine for when you have no jumping
        //Using Move will mean no more auto gravity, you need to write some code, the exact code is above
        //in this file 
        cc.Move(speedVector * Time.deltaTime); // Need to multiply by Time.deltaTime to make sure
        //the speed is not different on different framerates.
        // float forwardSpeed = Input.GetAxis("Vertical");
    }
}
