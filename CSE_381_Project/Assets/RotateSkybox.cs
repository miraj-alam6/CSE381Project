using UnityEngine;
using System.Collections;

public class RotateSkybox : MonoBehaviour {
    public float rotateSpeed = 20;
    public float rotationCount = 0 ;
    Skybox sb;
    // Use this for initialization
    void Start () {
        rotateSpeed = rotateSpeed / 10; // this is so numbers can be set quite high in the editor
        sb = GetComponent<Skybox>();
    }
	
	// Update is called once per frame
	void Update () {
        
        rotationCount += Time.deltaTime * rotateSpeed;
        if (rotationCount > 360) {
            rotationCount -= 360;
        }
        //RenderSettings.skybox.SetFloat("_Rotation", rotationCount);
        sb.material.SetFloat("_Rotation", rotationCount);
    }
}
