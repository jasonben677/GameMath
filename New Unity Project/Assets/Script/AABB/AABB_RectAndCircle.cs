using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB_RectAndCircle : MonoBehaviour
{
    public GameObject A;

    /// <summary>
    /// circle
    /// </summary>
    public GameObject B;

    private SpriteRenderer a_s;
    private SpriteRenderer b_s;

    private float aMax_X;
    private float aMin_X;
    private float aMax_Y;
    private float aMin_Y;

    private float bCenterX;
    private float bCenterY;
    private float bRadius;

    /// <summary>
    /// 最接近的點
    /// </summary>
    private Vector2 pClosest = new Vector2();

    private Vector2 dev = new Vector2();

    private void Awake()
    {
        a_s = A.GetComponent<SpriteRenderer>();
        b_s = B.GetComponent<SpriteRenderer>();
        bRadius = (b_s.sprite.rect.width * B.transform.localScale.x) / (b_s.sprite.pixelsPerUnit * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(A.transform, B.transform))
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

    private bool CheckCollision(Transform a, Transform b)
    {
        Vector2 aSize = new Vector2();

        aSize.x = (a_s.sprite.rect.width * a.localScale.x / (a_s.sprite.pixelsPerUnit * 2));
        aSize.y = (a_s.sprite.rect.height * a.localScale.y / (a_s.sprite.pixelsPerUnit * 2));


        aMax_X = a.transform.position.x + aSize.x;
        aMin_X = a.transform.position.x - aSize.x;
        aMax_Y = a.transform.position.y + aSize.y;
        aMin_Y = a.transform.position.y - aSize.y;

        Debug.DrawLine(new Vector2(aMin_X, aMin_Y), new Vector2(aMax_X, aMax_Y), Color.green);

        //圓心
        bCenterX = b.position.x;
        bCenterY = b.position.y;

        // 圓心指向方塊
        dev = a.position - b.position;
        pClosest.x = Mathf.Clamp(bCenterX, aMin_X, aMax_X);
        pClosest.y = Mathf.Clamp(bCenterY, aMin_Y, aMax_Y);

        return Vector2.Distance(b.position, pClosest) <= bRadius;
    }
}
