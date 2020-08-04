using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB3D_TwoCircle : MonoBehaviour
{
    public GameObject circleA;
    public GameObject circleB;

    private MeshRenderer a_m;
    private MeshRenderer b_m;

    private Material acolor;
    private Material bcolor;

    private float aRadius;
    private float bRadius;

    private void Awake()
    {
        a_m = circleA.GetComponent<MeshRenderer>();
        b_m = circleB.GetComponent<MeshRenderer>();

        acolor = a_m.material;
        bcolor = b_m.material;

        aRadius = circleA.transform.localScale.x / 2;
        bRadius = circleB.transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(circleA.transform, circleB.transform))
        {
            acolor.color = Color.red;
            bcolor.color = Color.red;
        }
        else
        {
            acolor.color = Color.white;
            bcolor.color = Color.white;
        }
    }


    private bool CheckCollision(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position) <= (aRadius + bRadius);
    }

}
