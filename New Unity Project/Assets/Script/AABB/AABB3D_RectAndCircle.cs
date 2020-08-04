using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB3D_RectAndCircle : MonoBehaviour
{
    public GameObject A;

    /// <summary>
    /// circle
    /// </summary>
    public GameObject circleB;

    private MeshRenderer a_s;
    private MeshRenderer b_s;

    private Material acolor;
    private Material bcolor;

    private float aMax_X;
    private float aMin_X;
    private float aMax_Y;
    private float aMin_Y;
    private float aMax_Z;
    private float aMin_Z;


    private float bCenterX;
    private float bCenterY;
    private float bCenterZ;
    private float bRadius;


    /// <summary>
    /// 圓心
    /// </summary>
    private Vector3 centerPoint = new Vector3();

    /// <summary>
    /// 最接近的點
    /// </summary>
    private Vector3 pClosest = new Vector3();


    private void Awake()
    {
        a_s = A.GetComponent<MeshRenderer>();
        b_s = circleB.GetComponent<MeshRenderer>();

        acolor = a_s.material;
        bcolor = b_s.material;

        bRadius = circleB.transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(A.transform, circleB.transform))
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
        aMax_X = a.position.x + (a.localScale.x / 2);
        aMin_X = a.position.x - (a.localScale.x / 2);
        aMax_Y = a.position.y + (a.localScale.y / 2);
        aMin_Y = a.position.y - (a.localScale.y / 2);
        aMax_Z = a.position.z + (a.localScale.z / 2);
        aMin_Z = a.position.z - (a.localScale.z / 2);

        centerPoint = b.position;

        pClosest.x = Mathf.Clamp(centerPoint.x, aMin_X, aMax_X);
        pClosest.y = Mathf.Clamp(centerPoint.y, aMin_Y, aMax_Y);
        pClosest.z = Mathf.Clamp(centerPoint.z, aMin_Z, aMax_Z);

        return Vector3.Distance(pClosest, centerPoint) <= bRadius;
    }
}
