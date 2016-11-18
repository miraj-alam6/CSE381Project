using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Victory : MonoBehaviour {

    public Text victoryText;

	// Use this for initialization
	void Start () {
        victoryText.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            victoryText.gameObject.SetActive(true);

        }
    }
}
