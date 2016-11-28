using UnityEngine;
using System.Collections;

public class MessageTrigger : MonoBehaviour {
    public GameObject messageToTrigger;
    public float timeMessageStays  = 5.0f;
    private bool countDown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown && !GameManager.instance.gamePaused) {
            timeMessageStays -= Time.deltaTime;
            if (timeMessageStays <= 0) {
                messageToTrigger.SetActive(false);
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            messageToTrigger.SetActive(true);
            countDown = true;
        }
    }
}
