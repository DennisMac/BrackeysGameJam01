using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset, originalOffset;         //Private variable to store the offset distance between the player and camera
    public float speed = 1f;
    public float zoom=1;

    void Start()
    {
        originalOffset = transform.position - player.transform.position;
        offset = originalOffset;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            zoom *= 1.5f;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            zoom /= 10f;

        zoom = Mathf.Clamp(zoom, .25f, 5f);
        offset = originalOffset * zoom;

        if (player != null)
        {
            Vector3 wantedPosition = player.transform.position + player.transform.rotation * offset;
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * speed);
            transform.LookAt(player.transform.position, player.transform.up);
        }
        else
        {
            FlightController fc = FindObjectOfType<FlightController>();
            if (fc != null) player = fc.gameObject;
        }

    }
}