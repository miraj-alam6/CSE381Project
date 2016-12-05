using UnityEngine;
using System.Collections;

public class ArrowMovingStructure : MovingStructure
{

    //Made these for this class specifically
    public Vector3 defaultPosition;
    public Vector3 goalPosition;

    //This is the accumulation of how far the goal will be for the moving structure 
    //TODO: make this private later.
    public Vector3 accumulatedGoalDistance;
    //int pathIndex :this says which path the platform will be following, as in which pair of points
    //The following should no longer be set in the inspector, still public for debugging purposes

    public bool moving = false;

    void Start()
    {
        GameManager.instance.addArrowMovingStructure(this);
        trans = GetComponent<Transform>();
 //       pathIndex = defaultPathIndex;
//        setupActivePoints(pathIndex);
        //Set up waiting times:
  //      pathGoalTimeLeft = new float[pathGoalWaitingTimes.Length];
   //     for (int i = 0; i < pathGoalWaitingTimes.Length; i++)
   //     {
   //         pathGoalTimeLeft[i] = pathGoalWaitingTimes[i];
   //     }

    }

    // Update is called once per frame
    void Update()
    {
      
        if (activated && moving)
        {
            move();
        }
    }



    //This function moves the structure from its current position, to a bit closer
    //towards its destination. It will be called every frame. The movement is smooth
    //Vector3.MoveTowards is used
    public new void  move()
    {
        //print("Why");

        trans.localPosition = Vector3.MoveTowards(transform.localPosition, goalPosition, Time.deltaTime * speed);
        /*
        //Each path has two points, figure out which point is your goal.
        if (!pathGotoLatter[pathIndex])
        {
            //now first check if you have reached your goal. 
            if (Vector3.Distance(trans.position, point1) <= 0.1)
            {
                if (pathGoalTimeLeft[pathIndex] <= 0)
                {
                    //Reset the waiting time, and change the goal to go to
                    pathGoalTimeLeft[pathIndex] = pathGoalWaitingTimes[pathIndex];
                    pathGotoLatter[pathIndex] = true;

                }
                pathGoalTimeLeft[pathIndex] -= Time.deltaTime;
            }

            //Now if you didn't reach your goal, move towards it
            else {
                trans.position = Vector3.MoveTowards(transform.position, point1, Time.deltaTime * speed);
            }

        }

        else if (pathGotoLatter[pathIndex])
        {
            //now first check if you have reached your goal. 
            if (Vector3.Distance(trans.position, point2) <= 0.1)
            {
                if (pathGoalTimeLeft[pathIndex] <= 0)
                {
                    //Reset the waiting time, and change the goal to go to
                    pathGoalTimeLeft[pathIndex] = pathGoalWaitingTimes[pathIndex];
                    pathGotoLatter[pathIndex] = false;

                }
                pathGoalTimeLeft[pathIndex] -= Time.deltaTime;
            }

            //Now if you didn't reach your goal, move towards it
            else {
                trans.position = Vector3.MoveTowards(transform.position, point2, Time.deltaTime * speed);
            }

        }

    */

    }

    public void changePath() {
        goalPosition = defaultPosition + accumulatedGoalDistance;
        moving = true;

    }



}
