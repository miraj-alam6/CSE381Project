using UnityEngine;
using System.Collections;

public class GoldenDoorHandler : MonoBehaviour {

	public GameObject goldenDoorLeft;
	public GameObject goldenDoorRight;
	public GoldenDoorScript gdLeftScript;
	public GoldenDoorScript gdRightScript;
	public Vector3 doorMovement;
	public float speed;

	Vector3 leftDoorGoal;
	Vector3 rightDoorGoal;

	void Start () {
		leftDoorGoal = goldenDoorLeft.transform.position + doorMovement*-1;
		rightDoorGoal = goldenDoorRight.transform.position + doorMovement;
	}


	// Update is called once per frame
	void Update () {
		if (gdLeftScript.isActivated && gdRightScript.isActivated) {
			//open doors
			print("Open sesame");
			goldenDoorLeft.transform.position = Vector3.MoveTowards (goldenDoorLeft.transform.position, leftDoorGoal,
				Time.deltaTime * speed);

			goldenDoorRight.transform.position = Vector3.MoveTowards (goldenDoorRight.transform.position, rightDoorGoal,
				Time.deltaTime * speed);

		}
	}
}
