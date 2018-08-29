using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    float reloadTime = 0.1f;
    float timeTillFire = 0;

    public void  Update()
    {
        timeTillFire -= Time.deltaTime;
        if (timeTillFire < 0)
        {
            if (Input.GetAxis("Fire1") > 0.5f)
            {
                timeTillFire = reloadTime;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }
    }
}
