using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float reloadTime = 0.1f;
    float timeTillFire = 0;
    public AudioClip[] clips;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void  Update()
    {
        timeTillFire -= Time.deltaTime;
        if (timeTillFire < 0)
        {
            if (Input.GetAxis("Fire1") > 0.5f)
            {
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
                timeTillFire = reloadTime;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }
    }
}
