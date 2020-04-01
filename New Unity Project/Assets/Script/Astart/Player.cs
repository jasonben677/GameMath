using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AIData Data;
    public int mCurrentPath;
    // Start is called before the first frame update
    void Start()
    {
        Data = new AIData();
        Data.speed = 0;
        Data.arriveRange = 5.0f;
        Data.maxSpeed = 2.5f;
        Data.maxRotate = 5.0f;
        Data.goal = gameObject;
        Data.target = Vector3.zero;
        mCurrentPath = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Vector3 t)
    {
        Data.target = t;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2.0f);

        if (Data != null)
        {
            Gizmos.DrawWireSphere(Data.target, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Data.arriveRange);
        }
    }

}
