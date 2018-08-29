using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {
    public float speed = 5;
    public float turnSpeed = 5;
    public float rollSpeed = 5f;
    public float liftMultiplier = 1f;
    public float minSpeed = 5;
    public float maxSpeed = 50;
    public float lift = 1f;
    Vector3 direction = Vector3.forward;
    public GameObject trailSectionPrefab;
    public GameObject crashPrefab;
    List<GameObject> trails = new List<GameObject>();
    private float thrustMultiplier=2;

    Rigidbody rbody;
    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        rbody.AddTorque( transform.up * Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed * Spawner.difficulty) ;
        rbody.AddTorque(transform.right * Input.GetAxis("Vertical") * Time.deltaTime * turnSpeed * Spawner.difficulty);
        rbody.AddTorque(transform.forward * Input.GetAxis("Roll") * Time.deltaTime * rollSpeed * Spawner.difficulty);

        rbody.AddForce(transform.forward * speed * (1 + Mathf.Clamp(thrustMultiplier * Input.GetAxis("Jump"), -1f, thrustMultiplier)) * Spawner.difficulty);
        //rbody.AddForce(lift * Vector3.Dot(rbody.velocity, transform.up) * transform.forward);


        rbody.velocity = Vector3.Dot(rbody.velocity, transform.forward) * transform.forward;



        UpdateTrail();

    }


    Vector3 lastPoint;
    Vector3 leftOffset = new Vector3(0.5f, 0, -2);
    Vector3 rightOffset = new Vector3(-0.5f, 0, -2);
    

    // Update is called once per frame
    void UpdateTrail()
    {
        //if (rbody.velocity.sqrMagnitude < 16) return;

        if ((lastPoint - transform.position).sqrMagnitude >= 2)
        {
            GameObject go = Instantiate(trailSectionPrefab, transform.position + transform.rotation * leftOffset, transform.rotation);
            trails.Add(go);
            go = Instantiate(trailSectionPrefab, transform.position + transform.rotation * rightOffset, transform.rotation);
            trails.Add(go);

            lastPoint = transform.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        GetComponent<HealthBar>().TakeDamage(Spawner.difficulty/10);
        Instantiate(crashPrefab, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 10) //Terrain
        {
            GetComponent<HealthBar>().TakeDamage(1f);
            Instantiate(crashPrefab, transform.position, transform.rotation);
        }
    }

    private void OnDestroy()
    {
        Spawner.Instance.SpawnPlayerWithDelay();
    }
}

