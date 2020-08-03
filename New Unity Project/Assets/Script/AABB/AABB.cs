using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour
{
    public GameObject a;
    public GameObject b;

    private SpriteRenderer a_s;
    private SpriteRenderer b_s;

    private float aMax_X;
    private float aMin_X;
    private float aMax_Y;
    private float aMin_Y;

    private float bMax_X;
    private float bMin_X;
    private float bMax_Y;
    private float bMin_Y;


    private void Awake()
    {
        a_s = a.GetComponent<SpriteRenderer>();
        b_s = b.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(a.transform, b.transform))
        {
            a_s.color = Color.red;
            b_s.color = Color.red;
        }
        else
        {
            a_s.color = Color.white;
            b_s.color = Color.white;
        }
    }

    /// <summary>
    /// 2d AABB test
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool CheckCollision(Transform a, Transform b)
    {
        aMax_X = a.transform.position.x + (a_s.bounds.size.x/2);
        aMin_X = a.transform.position.x - (a_s.bounds.size.x / 2);
        aMax_Y = a.transform.position.y + (a_s.bounds.size.y / 2);
        aMin_Y = a.transform.position.y - (a_s.bounds.size.y / 2);

        Debug.DrawLine(new Vector2(aMin_X, aMin_Y), new Vector2(aMax_X, aMax_Y), Color.green);

        bMax_X = b.transform.position.x + (b_s.bounds.size.x / 2);
        bMin_X = b.transform.position.x - (b_s.bounds.size.x / 2);
        bMax_Y = b.transform.position.y + (b_s.bounds.size.y / 2);
        bMin_Y = b.transform.position.y - (b_s.bounds.size.y / 2);

        Debug.DrawLine(new Vector2(bMax_X, bMax_Y), new Vector2(bMin_X, bMin_Y), Color.green);

        // a.xMax >= b.xMin && b.xMax >= a.xMin 
        bool collisionX = aMax_X >= bMin_X&& bMax_X >= aMin_X;
        //Debug.Log("x : " + collisionX);
        bool collisionY = aMax_Y >= bMin_Y && bMax_Y >= aMin_Y;
        //Debug.Log("y : " + collisionY);
        return collisionX && collisionY;
    }
}
