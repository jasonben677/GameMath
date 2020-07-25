using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAndSpiral : MonoBehaviour
{
    public GameObject mainObj;
    private float Radius = 3f;
    private float Speed = 1.5f;
    GameObject[] objArray;
    // Start is called before the first frame update
    void OnEnable()
    {
        CopyObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GoCircleMutli();
        //GoCircle(mainObj, 0);
    }

    private void CopyObj()
    {
        GameObject C1 = Instantiate(mainObj);
        GameObject C2 = Instantiate(mainObj);
        GameObject C3 = Instantiate(mainObj);
        GameObject C4 = Instantiate(mainObj);
        GameObject C5 = Instantiate(mainObj);
        objArray = new GameObject[] 
        { 
            mainObj, C1, C2, C3, C4, C5 
        };
    }

    #region 圓周運動
    /// <summary>
    /// 繞圓周運動
    /// </summary>
    private void GoCircle(GameObject _obj, float _radians)
    {
        float posY = Mathf.Sin(Speed * Time.time + _radians);
        float posX = Mathf.Cos(Speed * Time.time + _radians);

        //Z軸變動會形成螺旋軌跡
        float posZ = Speed * Time.time ;

        _obj.transform.position = new Vector3(Radius * posX, Radius * posY, posZ);
    }

    /// <summary>
    /// 繞圓周運動(多個)
    /// </summary>
    private void GoCircleMutli()
    {
        for (int i = 0; i < objArray.Length; i++)
        {
            float radians = 2 * Mathf.PI * (i) / objArray.Length;
            GoCircle(objArray[i], radians);
        }
    }
    #endregion
}
