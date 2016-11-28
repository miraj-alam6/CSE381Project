using UnityEngine;
using System.Collections;


//Source for this code http://answers.unity3d.com/questions/476810/flip-a-mesh-inside-out.html

public class InsideOut : MonoBehaviour {
    public int[] Tris;
    int temp;


    void Start()
    {
        
        Tris = this.GetComponent<MeshFilter>().mesh.triangles;
        //Debug.Log(Tris.Length);
        for (int i = 0; i < Tris.Length/3; i++)
        {
            if (Tris[3 * i] != null)
            {
                temp = Tris[3 * i + 1];
                Tris[3 * i + 1] = Tris[3 * i + 2];
                Tris[3 * i + 2] = temp;
            }
        }

        this.GetComponent<MeshFilter>().mesh.triangles = Tris;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
