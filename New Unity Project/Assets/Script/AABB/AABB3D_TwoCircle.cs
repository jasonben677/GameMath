using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB3D_TwoCircle : MonoBehaviour
{
    public GameObject circleA;
    public GameObject circleB;

    private MeshRenderer a_m;
    private MeshRenderer b_m;

    private float aRadius;
    private float bRadius;

    private void Awake()
    {
        a_m = circleA.GetComponent<MeshRenderer>();
        b_m = circleB.GetComponent<MeshRenderer>();

        aRadius = a_m.bounds.size.x / 2;
        bRadius = b_m.bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(circleA.transform, circleB.transform))
        {
            Debug.LogError("Collision");
        }
        else
        {
            Debug.Log("No Collision");
        }
    }


    private bool CheckCollision(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position) <= (aRadius + bRadius);
    }

}
