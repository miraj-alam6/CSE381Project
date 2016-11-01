using UnityEngine;
using System.Collections;

public class FPSShoot : MonoBehaviour {
    public GameObject bullet; //Put the brain into this variable 
    public float coolDown = 1;
    public float currentCoolDown = 0;
    public float bulletImpulse = 100;
    //public BrainUIBar coolDownUI;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (currentCoolDown <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentCoolDown = coolDown;
               // if (coolDownUI)
               // {
               //     coolDownUI.UpdateVitalBar(coolDown, coolDown - currentCoolDown);
               // }
                //  print("You have thrown a brain");
               // GameObject brainBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
               // brainBullet.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
            }
        }
        else {
            currentCoolDown -= 1 * Time.deltaTime;
            if (currentCoolDown < 0) {
                currentCoolDown = 0;
            }
           // if (coolDownUI) {
           //     coolDownUI.UpdateVitalBar(coolDown, coolDown - currentCoolDown);
           // }
        }
    }
}
