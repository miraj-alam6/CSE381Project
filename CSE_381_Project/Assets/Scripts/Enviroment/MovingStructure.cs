using UnityEngine;
using System.Collections;

public class MovingStructure : MonoBehaviour {
    //float speed: How quick the platform will be moving
    // TODO: Replace this with an array of speeds each corresponding
    // to the index for each path.
    public float speed;
    public Vector3 point1; //TODO: Make this private, public now to debug in editor
    public Vector3 point2;//TODO: Make this private, public now to debug in editor

    //Each index corresponds to a path which is the pair of locations
    //This is a striped array. Every pair of Vector4's comprises a path
    //THe w coordinate of each location represents which coordinates are actually used.
    //When a coordinate is "not used", its value is not gotten from this array but is instead
    //extracted from the position of the structure's transform.
    public Vector4[] pathLocations;
    public float[] pathSpeeds;
    public bool[] pathGotoLatter;
    public float[] pathGoalWaitingTimes;
    float[] pathGoalTimeLeft;
    //GameObject actualPlatform, commented out, because it is never used
    //public GameObject actualPlatform;


    //float[] locations
    //The following array will hold points for every location that the platform can
    //possibly move to. This is a striped array. It is striped on two different levels.
    //There are quintuplets that each represent two points. And two consecutive quintuplets
    //represent the two different points in one path. Once the platform reaches one of the points
    //in the path, it will move towards the other point in the pair, and this will keep repeating.
    public float[] locations;
    //How the 5-tuple works: xyzag. xyz: coordinates, 
    //a is a number that tells you which coordinates are active as in, which
    //coordinates to use value for from xyz. Values that we don't use xyz values for will get their
    //coordinate value from whatever it is in the transform of the platform.
    //a is a number that ranges from 0 to 7. It encodes which coordinates are used much like how
    //read/write/exec permissions are encoded in UNIX.
    //0: none, 1: X, 2: Y, 3: X, Y, 4: Z, 5: X, Z, 6: Y,Z, 7: X,Y,Z
    //g tells you 


    //int pathIndex :this says which path the platform will be following, as in which pair
    //of 5-tuples in the locations array is the active pair.
    public int pathIndex; 
    public bool forNow = true;
    public Transform trans;
    int firstPointStartIndex;
    int secondPointStartIndex;
    //The following boolean will be used to determine when to switch the path. If this is on,
    //the move function will decide to recalcualte where point1 and point2 is to correspond to
    //the new path. 
    private bool switchMovement = false;
    
    
    // Initializes the game object to get its Transform component
    //Also set up which two points will be used for the current path
    void Start() {
        trans = GetComponent<Transform>();
        setupActivePoints(pathIndex);
        //Set up waiting times:
        pathGoalTimeLeft = new float[pathGoalWaitingTimes.Length];
        for (int i = 0; i < pathGoalWaitingTimes.Length; i++)
        {
            pathGoalTimeLeft[i] = pathGoalWaitingTimes[i];
        }
        /*
        dest1.x = transform.position.x;
        dest1.y = transform.position.y;
        dest1.z = transform.position.z;

        dest2.x = transform.position.x + 10;
        dest2.y = transform.position.y;
        dest2.z = transform.position.z;
        */
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            setupActivePoints(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setupActivePoints(1);
        }
        move();
	}


    //Call this function once your platform/block switches its state so that you 
    //calcuate these things once per switching of state, instead of once per frame.
    public void setupActivePoints(int index) {

        setPathIndex(index);
    
        //First setup the actual points by checking the 4th coordinate to see which coordinates to
        //use custom values for. The rest will be gotten from the transform.
        //It is assumed that both points of the path will have same value for this 4th coordinate
        //thus this first boolean asserts that.

        if (pathLocations[pathIndex * 2].w != pathLocations[pathIndex * 2 + 1].w)
        {
            Debug.Log("ERROR in Setup: the W coordinate for your point must be the same for start and end");
            return;
        }

        //Get the coordinates for each point in the current path, so that move can use these points.
       
        point1 = setupSinglePoint(pathIndex * 2);
        point2 = setupSinglePoint(pathIndex * 2 + 1);

        //Set the speed
        speed = pathSpeeds[pathIndex];
    }

    public Vector3 setupSinglePoint(int index) {
        Vector3 point = new Vector3(0,0,0);

        switch ((int)pathLocations[index].w)
        {
            //Note: doing case 0 is very pointless, the structure you won't move at all.
            case 0:  //None of the coordinates will be set, get all of them from the transform
                point.x = trans.position.x;
                point.y = trans.position.y;
                point.z = trans.position.z;
                break;
            case 1: // X will be set; Y and Z are gotten from transform
                point.x = pathLocations[index].x;
                point.y = trans.position.y;
                point.z = trans.position.z;
                break;
            case 2:// Y will be set; X and Z are gotten from transform
                point.x = trans.position.x;
                point.y = pathLocations[index].y;
                point.z = trans.position.z;
                break;
            case 3:// X and Y will be set; Z is gotten from transform
                point.x = pathLocations[index].x;
                point.y = pathLocations[index].y;
                point.z = trans.position.z;
                break;
            case 4:
                point.x = trans.position.x;
                point.y = trans.position.y;
                point.z = pathLocations[index].z;
                break;
            case 5:
                point.x = pathLocations[index].x;
                point.y = trans.position.y;
                point.z = pathLocations[index].z;
                break;
            case 6:
                point.x = trans.position.x;
                point.y = pathLocations[index].y;
                point.z = pathLocations[index].z;
                break;
            case 7:
                point.x = pathLocations[index].x;
                point.y = pathLocations[index].y;
                point.z = pathLocations[index].z;
                break;
        }

        return point;
    }
   

    //This function moves the structure from its current position, to a bit closer
    //towards its destination. It will be called every frame. The movement is smooth
    //Vector3.MoveTowards is used
    public void move() {
        if (switchMovement)
        {
            switchMovement = false;
        }

        //If you don't have to switch your path, then move along your current path
        else {
            //Each path has two points, figure out which point is your goal with the 5th element
            //of each quintuplet.
            if (!pathGotoLatter[pathIndex]) {
                //now first check if you have reached your goal. 
                if (Vector3.Distance(trans.position, point1) <= 0.1)
                {
                    if (pathGoalTimeLeft[pathIndex] <= 0) {
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

        }
        
    }

    //This sets which path to use.
    //It is important to also call setupActivePoints() 
    //after calling setIndex if you actually want the platform start using the
    //new index. I did not make it that this function automatically calls setupActivePoints()
    //Because I want to make the functionality of the two functions be able to used independently
    //to be more modular. This might not be a good design decision, because, setting pathIndex
    //without resetting the active points will have strange behavior for goal, since goal always
    //uses the most recent boolean, which is often updated directly in the fields of the arrays,
    //while other stuff like path vertices are not updated real time per frame.
    private void setPathIndex(int index) {
        pathIndex = index;
    }


}
