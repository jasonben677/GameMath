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
        // 
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
    private bool CheckCollision(Transform _a, Transform _b)
    {
        Vector2 aSize = new Vector2();

        aSize.x = (a_s.sprite.rect.width * _a.localScale.x / (a_s.sprite.pixelsPerUnit * 2));
        aSize.y = (a_s.sprite.rect.height * _a.localScale.y / (a_s.sprite.pixelsPerUnit * 2));

        aMax_X = a.transform.position.x + (aSize.x);
        aMin_X = a.transform.position.x - (aSize.x);
        aMax_Y = a.transform.position.y + (aSize.y);
        aMin_Y = a.transform.position.y - (aSize.y);

        //Debug.DrawLine(new Vector2(aMin_X, aMin_Y), new Vector2(aMax_X, aMax_Y), Color.green);

        Vector2 bSize = new Vector2();

        bSize.x = (b_s.sprite.rect.width * _b.localScale.x / (b_s.sprite.pixelsPerUnit * 2));
        bSize.y = (b_s.sprite.rect.height * _b.localScale.y / (b_s.sprite.pixelsPerUnit * 2));

        bMax_X = b.transform.position.x + (bSize.x);
        bMin_X = b.transform.position.x - (bSize.x);
        bMax_Y = b.transform.position.y + (bSize.y);
        bMin_Y = b.transform.position.y - (bSize.y);

        //Debug.DrawLine(new Vector2(bMax_X, bMax_Y), new Vector2(bMin_X, bMin_Y), Color.green);

        // a.xMax >= b.xMin && b.xMax >= a.xMin 
        bool collisionX = aMax_X >= bMin_X&& bMax_X >= aMin_X;
        //Debug.Log("x : " + collisionX);
        bool collisionY = aMax_Y >= bMin_Y && bMax_Y >= aMin_Y;
        //Debug.Log("y : " + collisionY);
        return collisionX && collisionY;
    }
}
