using UnityEngine;
using System.Collections;

public class CannonTrigger : MonoBehaviour {

    public CannonFire[] cannons;
    public bool onOrOff;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag.Equals("Player"))
        {
            foreach (CannonFire cf in cannons)
            {
                cf.isOn = onOrOff;
            }

        }
    }
}
