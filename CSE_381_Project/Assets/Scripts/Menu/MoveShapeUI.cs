using UnityEngine;
using System.Collections;

public class MoveShapeUI : MonoBehaviour {
    Transform currentTrans;
    Vector3 goalTransPosition;
    public float speed;
    float startX;
    float startY;
    float goalX;
    float goalY;
    public float waitInterval;
    float timeCounter = 0;
	// Use this for initialization
	void Start () {
        currentTrans = GetComponent<Transform>();
        //Debug.Log("UI Element: " + currentTrans.position.x + "," + currentTrans.position.y);
        // currentTrans.position = new Vector3(startX, startY, 0);
        startX = currentTrans.position.x;
        startY = currentTrans.position.y;
        goalX = startX;
        goalY = -startY;
        goalTransPosition = new Vector3(goalX, goalY, 0);
        currentTrans.position = goalTransPosition; // start the shape at the goal already so that
            //the code for waiting begins from the first iteration.
       
        
	}
	
    private void moveToGoal(){
        //Debug.Log(Vector3.Magnitude(currentTrans.position - goalTransPosition));
        if (Vector3.Magnitude(currentTrans.position - goalTransPosition) >= 20)
        {
            currentTrans.position =
            Vector3.Lerp(currentTrans.position, new Vector3(goalX, goalY), speed * Time.deltaTime);
        }
        else {
            timeCounter += 1 * Time.deltaTime;
            if (timeCounter >= waitInterval) {
                timeCounter = 0;  
                currentTrans.position = new Vector3(startX, startY, 0);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        moveToGoal();
    }

 //   IEnumerator SmoothMovement(RectTransform goal) {
  //      while (Vector3.Distance(currentTrans.position, goal.position) > 0.01f)
   //     {
    //        currentTrans.position = Vector3.Lerp(currentTrans.position, goal.position,
    //            inverseMoveTime * Time.deltaTime);
    //        return null; // Causes loop to resume after the next update.
    //    }


//    }
}
