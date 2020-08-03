using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB3D : MonoBehaviour
{
    public GameObject a;
    public GameObject b;

    private MeshRenderer a_s;
    private MeshRenderer b_s;

    private float aMax_X;
    private float aMin_X;
    private float aMax_Y;
    private float aMin_Y;
    private float aMax_Z;
    private float aMin_Z;


    private float bMax_X;
    private float bMin_X;
    private float bMax_Y;
    private float bMin_Y;
    private float bMax_Z;
    private float bMin_Z;

    private void Awake()
    {
        a_s = a.GetComponent<MeshRenderer>();
        b_s = b.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(a.transform, b.transform))
        {
            Debug.LogError("collision");
        }
        else
        {
            Debug.Log("No collision");
        }
    }

    /// <summary>
    /// 3d AABB test
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool CheckCollision(Transform a, Transform b)
    {
        aMax_X = a.position.x + (a_s.bounds.size.x / 2);
        aMin_X = a.position.x - (a_s.bounds.size.x / 2);
        aMax_Y = a.position.y + (a_s.bounds.size.y / 2);
        aMin_Y = a.position.y - (a_s.bounds.size.y / 2);
        aMax_Z = a.position.z + (a_s.bounds.size.z / 2);
        aMin_Z = a.position.z - (a_s.bounds.size.z / 2);

        bMax_X = b.position.x + (b_s.bounds.size.x / 2);
        bMin_X = b.position.x - (b_s.bounds.size.x / 2);
        bMax_Y = b.position.y + (b_s.bounds.size.y / 2);
        bMin_Y = b.position.y - (b_s.bounds.size.y / 2);
        bMax_Z = b.position.z + (b_s.bounds.size.z / 2);
        bMin_Z = b.position.z - (b_s.bounds.size.z / 2);


        bool collisionX = aMax_X >= bMin_X && bMax_X >= aMin_X;
        bool collisionY = aMax_Y >= bMin_Y && bMax_Y >= aMin_Y;
        bool collisionZ = aMax_Z >= bMin_Z && bMax_Z >= aMin_Z;

        return collisionX && collisionY && collisionZ;

    }
}
