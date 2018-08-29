using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFollower : MonoBehaviour {
    public float speed = 5;
    public float turnSpeed = 5;
    public float minSpeed = 5;
    public float maxSpeed = 50;
    Vector3 direction = Vector3.forward;

    public GameObject trailSectionPrefab;
    public GameObject crashPrefab;
    List<GameObject> trails = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;
        //layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            transform.position = hit.point + 0.5f * Vector3.up;
        }

        transform.position += (transform.rotation) * direction * speed*Time.deltaTime;
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal")* Time.deltaTime* turnSpeed);
        speed += Input.GetAxis("Vertical");
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        UpdateTrail();    }



    float updateTime = 1f;
    float timeSinceLast = 0f;
    Vector3 lastPoint;

    // Update is called once per frame
    void UpdateTrail()
    {
        //timeSinceLast += Time.deltaTime;
        if ((lastPoint-transform.position).sqrMagnitude>=1)
        {
            lastPoint = transform.position;
            GameObject go = Instantiate(trailSectionPrefab, transform.position, transform.rotation);
            trails.Add(go);
         }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Destroy(this.gameObject);
        Instantiate(crashPrefab, transform.position, transform.rotation);
    }

}
