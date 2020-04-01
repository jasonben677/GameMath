using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public List<GameObject> neighbor;

    public void OnDrawGizmos()
    {
        if (neighbor != null && neighbor.Count > 0)
        {
            foreach (var item in neighbor)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, item.transform.position);
            }
        }
    }

}
