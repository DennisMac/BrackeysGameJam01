using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().velocity = 100 * transform.up;
        Invoke("DestroyMe", 0.5f);
	}

    void DestroyMe()
    {
        
        Destroy(this.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        HealthBar hb = other.GetComponent<HealthBar>();
        if(hb!= null) hb.TakeDamage(1f/Spawner.difficulty);
        Destroy(this.gameObject);
    }
}
