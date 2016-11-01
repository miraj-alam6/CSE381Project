using UnityEngine;

using System.Collections;

public class MainCamera : MonoBehaviour {
    // public Player mainPlayer;
    /*   private Vector3 offset;
       public float cameraDistance = 4;
       public float cameraZoomSpeed = 10;
       public float inverseCameraZoomSpeed;
       private float yOffSetMax = 0;
       private float xOffSetMax = 0;
       private float zOffSetMax = 0;
       private float yRotRadians;
       // Use this for initialization
       void Start () {
           /////////////////////////
           // PLACE HOLDER CODE
           if (GameObject.FindGameObjectWithTag("Player"))
           {
               if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())
               {
        //           mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
               }
           }
           offset = transform.position - mainPlayer.transform.position;
           xOffSetMax = offset.x;
           yOffSetMax = offset.y;
           zOffSetMax = offset.z;
           //Debug.Log(xOffSetMax + "," + yOffSetMax + "," + zOffSetMax );
           inverseCameraZoomSpeed = 0.01f * cameraZoomSpeed;
           // PLACE HOLDER CODE
           /////////////////////////
       }

       // Update is called once per frame
       void Update () {
           //Debug.Log(transform.eulerAngles.y);
           yRotRadians = -transform.eulerAngles.y * Mathf.Deg2Rad;
           Vector3 trueOffset = new Vector3(cameraDistance * Mathf.Sin(yRotRadians), yOffSetMax, -cameraDistance * Mathf.Cos(yRotRadians));
          // transform.position = mainPlayer.transform.position + trueOffset;

       }
       public void MoveCamera(float xAxis, float yAxis) {
           transform.Rotate(0, xAxis, 0); // keeping yAxis messed things up
           if (yAxis > 0  && cameraDistance >= 0f || yAxis < 0 && cameraDistance < 10.0f) {

                   cameraDistance += -yAxis * inverseCameraZoomSpeed;
           }
           //transform.Rotate(yAxis,xAxis,0, Space.World);
          //transform.RotateAround(mainPlayer.transform.position, 0.01f);
       }



       public float getYRot() {
           return yRotRadians;
       }
       */
}


/*
NOTES TO SELF:
There were a few places where you just threw in negatives to fix the problem. Make sure
you understand why you threw those negatives in.
*/
