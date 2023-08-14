using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{

    public float lifeSpan = 5f;
    float elapsedTime = 0;
    public AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }
}
