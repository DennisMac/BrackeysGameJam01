using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlightController : MonoBehaviour
{
    float staggerDelay = 0f;
    bool attackingTarget = true;
    Transform target;
    public float speed = 5;
    public float turnSpeed = 5;
    public float liftMultiplier = 1f;
    public float minSpeed = 5;
    public float maxSpeed = 50;
    public float lift = 1f;
    Vector3 direction = Vector3.forward;

    public GameObject trailSectionPrefab;
    public GameObject crashPrefab;
    public GameObject bigCrashPrefab;
    List<GameObject> trails = new List<GameObject>();
    bool flyStraightForaBit = false;

    Rigidbody rbody;
    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if(go != null) target = go.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (staggerDelay >= 0) staggerDelay -= Time.deltaTime;

        float horz = 0;
        float vert = 0;
        float roll = 0;


        if (attackingTarget)
        {
            if (target != null)
            {

                float leadAmount = Random.Range(5f, 25f);
                Vector3 lead = target.position + target.rotation * Vector3.forward * leadAmount;
                Vector3 direction = Quaternion.Inverse(transform.rotation) * (lead - transform.position);
                if (direction.x > 0) horz = 1;
                if (direction.x < 0) horz = -1;
                if (direction.y > 0) vert = -1;
                if (direction.y < 0) vert = 1;
            }
            else
            {//reaquire target
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                if (go != null) target = go.transform;
            }
        roll = Random.Range(-.25f, .25f);

            if(Random.Range(0,1000) <1 && !flyStraightForaBit)
            {
                flyStraightForaBit = true;
                Invoke("StopFlyingStraight", 3f);
            }
            if (flyStraightForaBit)
            {
                horz = 0; vert = 0; roll = 0;
            }
        }


        //collision avoidance
        RaycastHit hit;
        if (attackingTarget && Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            attackingTarget = false;
            flyStraightForaBit = true;
            Invoke("AttackTarget", 0.2f);
            Invoke("StopFlyingStraight", 3f);
        }

        if(!attackingTarget)
        {
            roll = 0;
            horz = 1;
            vert = 1;
        }



        rbody.AddTorque(transform.up * horz * Time.deltaTime * turnSpeed * Spawner.difficulty);
        rbody.AddTorque(transform.right * vert * Time.deltaTime * turnSpeed * Spawner.difficulty);
        rbody.AddTorque(transform.forward * roll * Time.deltaTime * turnSpeed * Spawner.difficulty);

        rbody.AddForce(transform.forward * speed * Spawner.difficulty);
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

    private void StopFlyingStraight()
    {
        flyStraightForaBit = false;
    }
    private void AttackTarget()
    {
        attackingTarget = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (staggerDelay >= 0)
        {
            return;//dont keep crashing
        }
        else
        {
            staggerDelay = 2f;
            GetComponent<HealthBar>().TakeDamage(1f/Spawner.difficulty);
            Instantiate(crashPrefab, transform.position, transform.rotation);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 10) //Terrain
        {
            GetComponent<HealthBar>().TakeDamage(1f);
        }
        else
        {
            GetComponent<HealthBar>().TakeDamage(1f/Spawner.difficulty);

            if (collision.rigidbody.gameObject.tag == "Player")
            {
                Vector3 explosionPos = collision.contacts[0].point;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, 2);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(50, explosionPos, 10, 0);
                }
            }

        }
    }

    private void OnDestroy()
    {
        Instantiate(bigCrashPrefab, transform.position, transform.rotation);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 2);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(100, explosionPos, 10, 0);
        }

        if (gameObject.tag =="Enemy1")
            Spawner.Instance.SpawnEnemy1WithDelay();
        else
            Spawner.Instance.SpawnEnemy2WithDelay();


    }
}

