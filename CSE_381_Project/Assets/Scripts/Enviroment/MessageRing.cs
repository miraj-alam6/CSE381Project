﻿using UnityEngine;
using System.Collections;

public class MessageRing : MonoBehaviour {
    public GameObject messageToShow;
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            messageToShow.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            messageToShow.SetActive(false);
        }
    }
}
