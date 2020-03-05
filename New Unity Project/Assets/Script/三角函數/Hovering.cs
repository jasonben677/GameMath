using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    //週期
    public float cposX = 0.3f;
    public float cposy = 0.1f;
    public float cposz = 0.2f;
    //

    //周長
    public float radius = 1f;
    //

    /// <summary>
    /// 傾斜比重(越大幅度越小)
    /// </summary>
    public float rValue = 3.0f;

    private void FixedUpdate()
    {
        HoveringMove();
    }

    /// <summary>
    /// 漂浮運動
    /// </summary>
    private void HoveringMove()
    {
        float posX = Mathf.Sin(Time.time * (cposX * Mathf.PI));
        float posY = Mathf.Sin(Time.time * (cposy * Mathf.PI));
        float posZ = Mathf.Sin(Time.time * (cposz * Mathf.PI));

        Vector3 hover = new Vector3(posX, posY, posZ);

        transform.position = hover;
        transform.rotation = Quaternion.FromToRotation(transform.up, -hover + rValue * transform.up);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3.0f);
    }

}
