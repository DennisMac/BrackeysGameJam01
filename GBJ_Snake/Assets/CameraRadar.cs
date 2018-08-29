using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRadar : MonoBehaviour {
    Transform target;
	// Use this for initialization
	void Start ()
    {
        target = FindObjectOfType<FlightController>().transform;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
        else
        {
            FlightController fc = FindObjectOfType<FlightController>();
            if (fc != null) target = fc.gameObject.transform;
        }




       
		
	}
}
