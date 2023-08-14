using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRunner : MonoBehaviour
{
    public float speed =2.5f;
    public float increment = 25f;
    Vector3 direction = Vector3.forward;
    bool changeDirection = false;
	// Use this for initialization
	void Start ()
    {
  
	}

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 10; //layerMask = ~layerMask;
        Vector3 position = transform.position;
        position += speed * direction;

        RaycastHit hit;
        if (Physics.Raycast(position + 100 * Vector3.up, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            position.y = hit.point.y + 0.5f;
            transform.position = position;
        }
        else
        {
            position = transform.position+Vector3.up;
            //if (position.x > 250f) position.x = 250f - speed;
            //if (position.x < -250f) position.x= -250f + speed;
            //if (position.z > 250f) position.z = 250f - speed;
            //if (position.z < -250f) position.z = - 250f + speed;
            //position.y = 40f;
            direction = -direction;
        }
        transform.position = position;

        if (Random.Range(0, 200) < 2)
        {
            changeDirection = true;
        }
        if (changeDirection)
        {
            if (transform.position.x % 25f <= speed && transform.position.z % 25f <= speed )
            {
                changeDirection = false;
                position = new Vector3( Mathf.Round(transform.position.x / 25f) * 25f, transform.position.y, Mathf.Round(transform.position.z /25f) * 25f);
                transform.position = position;

                switch (Random.Range(0, 2))
                {
                    case 0:
                        if (direction == Vector3.forward || direction == Vector3.back)
                        {
                            direction = Vector3.left;
                        }
                        else
                        {
                            direction = Vector3.forward;
                        }
                        break;
                    case 1:
                        if (direction == Vector3.forward || direction == -Vector3.forward)
                        {
                            direction = Vector3.right;
                        }
                        else
                        {
                            direction = Vector3.back;
                        }
                        break;                   
                }
                   
            }        

        }

    
    }
}
