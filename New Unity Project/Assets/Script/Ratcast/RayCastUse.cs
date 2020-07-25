using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastUse : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, 1<<10))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                if(hit.collider.tag == "Terrian")
                    transform.position = hit.point;
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin+(ray.direction*500f), Color.green);
            }
        }

    }
}
