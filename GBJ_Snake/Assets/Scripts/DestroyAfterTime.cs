using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float lifeSpan = 20f;
    float elapsedTime = 0;
    float delay = 0.5f;
    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
	
	void FixedUpdate ()
    {
        elapsedTime += Time.deltaTime;
        if (!boxCollider.enabled && elapsedTime > delay) boxCollider.enabled = true;
        if (elapsedTime >= lifeSpan)
        {
            Destroy(this.gameObject);
        }
	}
}
