using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    
    private float lockAxisX = 0.0f;
    private float lockAxisZ = 0.0f;
    
	// Use this for initialization
	void Start ()
    {

        lockAxisX = transform.rotation.x;
        lockAxisZ = transform.rotation.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        //makes the object always face the target
		transform.LookAt(Camera.main.transform.position, Vector3.up);
        
        //ensures that rotation only happens on the y-axis
        transform.rotation = Quaternion.Euler(lockAxisX, transform.rotation.eulerAngles.y - 180f, lockAxisZ);
	}
}
