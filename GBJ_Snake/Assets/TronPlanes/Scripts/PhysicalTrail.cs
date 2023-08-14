using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTrail : MonoBehaviour {

    public Transform target;
    public Material mat;
    Mesh mesh;
    List<Vector3> trailVerts = new List<Vector3>();

    // Use this for initialization
    void Start ()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = mat;
        mesh = GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        // make changes to the Mesh by creating arrays which contain the new values
        mesh.vertices = new Vector3[] { target.position - 0.5f * Vector3.up, target.position + 0.5f * Vector3.up, new Vector3(1, 1, 0) };

        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };

    }


    float updateTime = 1f;
    float timeSinceLast = 0f;

	// Update is called once per frame
	void Update ()
    {
        timeSinceLast += Time.deltaTime;
        if(timeSinceLast>updateTime)
        { 

            }

    }
}
