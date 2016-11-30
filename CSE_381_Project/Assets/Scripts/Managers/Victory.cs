using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Victory : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.finishLevel();

        }
    }
}
