using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMMotion : MonoBehaviour
{
    [SerializeField] GameObject mainObj;
    float speed = 1.5f * Mathf.PI;
    float range = 3.0f;
    void FixedUpdate()
    {
        //BasicSHM(mainObj);
        //DampingSpringMotion(mainObj);
        PendulumMotion(mainObj);
    }

    /// <summary>
    /// 簡諧運動
    /// </summary>
    /// <param name="_obj"></param>
    private void BasicSHM(GameObject _obj)
    {
        float posX = Mathf.Cos(speed * Time.time);
        _obj.transform.position = new Vector3(range * posX , 0, 0);
    }


    /// <summary>
    /// 有阻力彈簧運動
    /// </summary>
    /// <param name="_obj"></param>
    private void DampingSpringMotion(GameObject _obj)
    {
        float damp = Mathf.Pow(0.5f, Time.time);
        float posX = Mathf.Cos(speed * Time.time);
        _obj.transform.position = new Vector3(range * posX * damp, 0);
    }

    /// <summary>
    /// 鐘擺運動(有阻力版)
    /// </summary>
    /// <param name="_obj"></param>
    private void PendulumMotion(GameObject _obj)
    {
        float damp = Mathf.Pow(0.9f, Time.time);
        float maxRadians = 1.5f * Mathf.PI;
        float minRadians = 0.5f * Mathf.PI;
        float s = Mathf.Cos(speed * Time.time);

        //給阻力必須在設定軌跡就給
        float final = s * minRadians * damp + maxRadians;

        _obj.transform.position = new Vector3(range * Mathf.Cos(final), range * Mathf.Sin(final), 0);
    }

}
