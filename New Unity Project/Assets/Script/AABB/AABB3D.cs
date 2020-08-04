using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB3D : MonoBehaviour
{
    public GameObject a;
    public GameObject b;

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
        acolor = a_s.material;
        bcolor = b_s.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(a.transform, b.transform))
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

    /// <summary>
    /// 3d AABB test
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool CheckCollision(Transform _a, Transform _b)
    {
        aMax_X = _a.position.x + (_a.localScale.x / 2);
        aMin_X = _a.position.x - (_a.localScale.x / 2);
        aMax_Y = _a.position.y + (_a.localScale.y / 2);
        aMin_Y = _a.position.y - (_a.localScale.y / 2);
        aMax_Z = _a.position.z + (_a.localScale.z / 2);
        aMin_Z = _a.position.z - (_a.localScale.z / 2);

        bMax_X = _b.position.x + (_b.localScale.x / 2);
        bMin_X = _b.position.x - (_b.localScale.x / 2);
        bMax_Y = _b.position.y + (_b.localScale.y / 2);
        bMin_Y = _b.position.y - (_b.localScale.y / 2);
        bMax_Z = _b.position.z + (_b.localScale.z / 2);
        bMin_Z = _b.position.z - (_b.localScale.z / 2);


        bool collisionX = aMax_X >= bMin_X && bMax_X >= aMin_X;
        bool collisionY = aMax_Y >= bMin_Y && bMax_Y >= aMin_Y;
        bool collisionZ = aMax_Z >= bMin_Z && bMax_Z >= aMin_Z;

        return collisionX && collisionY && collisionZ;

    }
}
