using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }

        else if(instance != this) {
            Destroy(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
