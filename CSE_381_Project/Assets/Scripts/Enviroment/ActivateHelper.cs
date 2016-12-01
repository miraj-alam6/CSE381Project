using UnityEngine;
using System.Collections;

public class ActivateHelper: MonoBehaviour {
    public GameObject gO;
    
    public void activate() {
        gO.SetActive(true);
    }
}
