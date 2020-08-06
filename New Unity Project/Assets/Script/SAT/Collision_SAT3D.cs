using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_SAT3D : MonoBehaviour
{
    public Transform a;
    public Transform b;

    private MeshRenderer a_s;
    private MeshRenderer b_s;


    private void Awake()
    {
        a_s = a.GetComponent<MeshRenderer>();
        b_s = b.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision(ToObb(a), ToObb(b)))
        {
            a_s.material.color = Color.red;
            b_s.material.color = Color.red;
        }
        else
        {
            a_s.material.color = Color.white;
            b_s.material.color = Color.white;
        }
    }

    private Obb ToObb(Transform t)
    {
        return new Obb(t.position, t.localScale, t.rotation);
    }


    private bool CheckCollision(Obb _a, Obb _b)
    {
        if (Separted(_a, _b, _a.up))
        {
            return false;
        }
        if (Separted(_a, _b, _a.right))
        {
            return false;
        }
        if (Separted(_a, _b, _a.forward))
        {
            return false;
        }

        if (Separted(_a, _b, _b.up))
        {
            return false;
        }
        if (Separted(_a, _b, _b.right))
        {
            return false;
        }
        if (Separted(_a, _b, _b.forward))
        {
            return false;
        }

        // 每個面的法向量不夠做檢查的情況(a跟b做外積)
        if (Separted(_a, _b, Vector3.Cross(_a.up, _b.up)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.up, _b.right)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.up, _b.forward)))
        {
            return false;
        }

        if (Separted(_a, _b, Vector3.Cross(_a.right, _b.up)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.right, _b.right)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.right, _b.forward)))
        {
            return false;
        }

        if (Separted(_a, _b, Vector3.Cross(_a.forward, _b.up)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.forward, _b.right)))
        {
            return false;
        }
        if (Separted(_a, _b, Vector3.Cross(_a.forward, _b.forward)))
        {
            return false;
        }

        return true;
    }


    private bool Separted(Obb _a, Obb _b, Vector3 _axis)
    {
        float _aMax = float.MinValue;
        float _aMin = float.MaxValue;

        float _bMax = float.MinValue;
        float _bMin = float.MaxValue;


        for (int i = 0; i < _a.vertices.Length; i++)
        {
            float _adist = Vector3.Dot(_a.vertices[i], _axis);
            _aMax = (_adist > _aMax) ? _adist : _aMax;
            _aMin = (_adist < _aMin) ? _adist : _aMin;

            float _bdist = Vector3.Dot(_b.vertices[i], _axis);
            _bMax = (_bdist > _bMax) ? _bdist : _bMax;
            _bMin = (_bdist < _bMin) ? _bdist : _bMin;
        }

        float total = Mathf.Max(_aMax, _bMax) - Mathf.Min(_aMin, _bMin);
        float result = (_aMax - _aMin) + (_bMax - _bMin);
        return total > result ;
    }

}

public class Obb
{
    public Vector3[] vertices
    {
        get;
    }

    public Vector3 up
    {
        get;
    }
    public Vector3 right
    {
        get;
    }
    public Vector3 forward
    {
        get;
    }


    public Obb(Vector3 center, Vector3 size, Quaternion rotation)
    {
        Vector3 max = size/2;
        Vector3 min = -max;

        vertices = new Vector3[]
            {
                // 1
                center + rotation * new Vector3(min.x, max.y, max.z),

                // 2
                center + rotation * new Vector3(min.x, max.y, min.z),

                // 3
                center + rotation * new Vector3(max.x, max.y, min.z),

                // 4
                center + rotation * max,

                // 5
                center + rotation * new Vector3(min.x, min.y, max.z),

                // 6
                center + rotation * min,

                // 7
                center + rotation * new Vector3(max.x, min.y, min.z),

                // 8 
                center + rotation * new Vector3(max.x, min.y, max.z)
            };

        up = rotation * Vector3.up;
        right = rotation * Vector3.right;
        forward = rotation * Vector3.forward;

    }


}