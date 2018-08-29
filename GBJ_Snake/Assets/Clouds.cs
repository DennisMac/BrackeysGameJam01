using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {
    public int count = 100;
    public GameObject[] cloudPrefabs;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(Random.Range(-100, 100), Random.Range(5, 25), Random.Range(-100, 100));
            Quaternion rotation = Quaternion.Euler(0,Random.Range(0, 360), 0);

            Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)], position, rotation);


        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
