using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneInput : MonoBehaviour
{
    //float yaw = 0f;
    //float pitch = 0f;
    //float roll = 0f;
    public static void GetInput(ref float yaw, ref float pitch, ref float roll)
    {
        yaw = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        roll = Input.GetAxis("Roll");
    }
}
