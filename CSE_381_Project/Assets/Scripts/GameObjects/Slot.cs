using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Slot : MonoBehaviour {
	public float angleUncertainty = 15f;
	public float speed = 10.0f;
	private bool isInserting = false;
    //These are needed to make up for weird stuff with the rotations
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    Transform insertTarget;

	GameObject artifactObject;
	Vector3 matchedConfig;

	public Obelisk parentObelisk;
    public string[] artifactNames;
    //First 3 coordinates refer to the rotation for the artificat, 4th coordinate is the index
    //of the artifact in the list of possible artifacts.
    public Vector4[] configurations;
    public int actionNumber;
    public bool occupied = false;
    public float arrowMoveX;
    public float arrowMoveY;
    public float arrowMoveZ;
    // Use this for initialization
    void Start () {
		//If more children are added to slot, THIS MUST BE CHANGED
		insertTarget = this.transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isInserting) {
            //#MIRAJ added this to deal with pausing
            if (artifactObject.GetComponent<Artifact>().active) { 
			    moveArtifactIntoSlot();
            }
        }
	}

    public bool CheckConfiguration(float nameIndex, Vector3 artifactVec) {
		foreach (Vector4 v in configurations) {
            Debug.Log("Useful Exact:" + artifactVec.x  +  "," + artifactVec.y + ","+ artifactVec.z + "VS:" +
                v.x  +"," + v.y + "," + v.z);
			if (v.w == nameIndex) {
				float totalAngleDifference = 0.0f;
				totalAngleDifference += getAngleDifference (v.x, artifactVec.x);
				totalAngleDifference += getAngleDifference (v.y, artifactVec.y);
				totalAngleDifference += getAngleDifference (v.z, artifactVec.z);

                //print (totalAngleDifference);
                //If the total difference is less than or equal to the allowed uncertainty, the artifact can be inserted
                if (angleUncertainty >= totalAngleDifference)
                {
                    matchedConfig = v;
                    if (parentObelisk is ArrowObelisk) {
                        if (
                        ((ArrowObelisk)parentObelisk).attemptEffect(new Vector3(arrowMoveX, arrowMoveY
                            , arrowMoveZ)))
                        {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    return true;

                }
                else {
                    totalAngleDifference = 0.0f;
                    totalAngleDifference += getAngleDifference(v.x + offsetX, artifactVec.x);
                    totalAngleDifference += getAngleDifference(v.y + offsetY, artifactVec.y);
                    totalAngleDifference += getAngleDifference(v.z + offsetZ, artifactVec.z);
                    matchedConfig = v;
                    if (angleUncertainty >= totalAngleDifference)
                    {
                        matchedConfig = v;
                        if (parentObelisk is ArrowObelisk)
                        {
                            if (
                           ((ArrowObelisk)parentObelisk).attemptEffect(new Vector3(arrowMoveX, arrowMoveY
                            , arrowMoveZ)))
                            {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                        return true;
                    }

                }
            }
		}
		//If it matches none of the preset configurations, it cannot be inserted
		return false;
    }

	public void handleArtifact (GameObject heldObject){
        if (occupied) {
            return;
        }
		Vector3 artifactAngles = heldObject.transform.localRotation.eulerAngles;
		string name = heldObject.GetComponent<Artifact> ().artifactName;

		//Returns -1 if it does not find the name
		int nameIndex = System.Array.IndexOf(artifactNames, name);
		//If the name is not found, return
		if (nameIndex < 0) {
            SoundManager.instance.rejectPiece();
            //print ("bad name index");
            return;
		}
		//Returns if artifact angles do not match any of the presets (that share it's name-index)
		if(!CheckConfiguration (nameIndex, artifactAngles)) {
            //print ("bad config index");
            SoundManager.instance.rejectPiece();
            return;
		}
        
        SoundManager.instance.acceptPiece();
		//Valid artifact name/angles 
		insertArtifact(heldObject);
        
	}

	void insertArtifact(GameObject artifact){
		//Have player 'drop' the artifact, de-parenting it and clearing appropriate data fields
		artifact.transform.localEulerAngles = matchedConfig;
        //Debug.Log(matchedConfig);
		artifact.transform.parent.GetComponent<PickUp> ().dropObject ();
        artifact.GetComponent<Artifact>().lastConfig = matchedConfig;
		artifact.GetComponent<Rigidbody> ().isKinematic = true;
		artifact.transform.parent = this.transform;
		artifactObject = artifact;
		isInserting = true;
        occupied = true;
		
	}


	float getAngleDifference(float angle1, float angle2){
		float larger, smaller;

		//Separate the 2 input angles by which is greater
		if (angle1 > angle2) {
			larger = angle1;
			smaller = angle2;
		} else {
			larger = angle2;
			smaller = angle1;
		}
		//This works without larger/smaller, but they're used for consistency
		float diff1 = Mathf.Abs(larger - smaller);
		//This requires larger/smaller
		float diff2 = 360 - larger + smaller;

		//Return the minimum of the two differences
		return Mathf.Min (diff1, diff2);
	}

	public void moveArtifactIntoSlot (){
		//insert target, artifact object
		artifactObject.transform.position = Vector3.MoveTowards (artifactObject.transform.position, 
			insertTarget.transform.position, Time.deltaTime * speed);
		if (Vector3.Distance (artifactObject.transform.position, insertTarget.transform.position) <= 0.1) {
			isInserting = false;
            parentObelisk.processStateChange(0);
        }
	}

    public void artifactRemoved() {
        occupied = false;
        //foregoing the possibily of level design where you can not go into certain areas with arrows,
        //because the opposite of taking pieces out would have to be prevented as well, and don't
        //feel like dealing with all the intricacies.
        if (parentObelisk is ArrowObelisk) {
            //don't put minueses here, because two negatives will make a positive
            ((ArrowObelisk)parentObelisk).attemptEffect(new Vector3(arrowMoveX, arrowMoveY, arrowMoveZ));
        }
        parentObelisk.processStateChange(1);
    }


    /*public void OnTriggerStay(Collider other) {
        if (Input.GetButtonDown("Fire1")) {
			parentObelisk.processStateChange(0);
            //parent.processStateChange(actionNumber);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("Let me borrow");
			parentObelisk.processStateChange(1);
            //parent.processStateChange(-actionNumber);
        }
    }*/

	public bool getIsInserting(){
		return isInserting;
	}
}
