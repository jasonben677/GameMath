using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMain : MonoBehaviour
{
    public GameObject control;
    private Player pl;
    public Astar astar;
    private bool m_bAstar = false;

    // Start is called before the first frame update
    void Start()
    {
        WayPointTerrian wpt = new WayPointTerrian();
        wpt.Init();

        astar = new Astar();
        astar.Init(wpt);
        pl = control.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh, 1000.0f, 1 << LayerMask.NameToLayer("Terrain")))
            {
                m_bAstar = Astar.m_Instance.PerformAStar(control.transform.position, rh.point);
                pl.mCurrentPath = 0;
                Debug.Log(m_bAstar);
            }

        }
        if (m_bAstar)
        {
            List<Vector3> path = Astar.m_Instance.GetPath();
            int iFinal = path.Count - 1;
            int i;
            for (i = iFinal; i >= pl.mCurrentPath; i--)
            {
                Vector3 sPos = path[i];
                Vector3 cPos = control.transform.position;
                if (Physics.Linecast(cPos, sPos, 1 << LayerMask.NameToLayer("Wall")))
                {
                    continue;
                }
                pl.mCurrentPath = i;
                pl.SetTarget(sPos);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_bAstar)
        {
            List<Vector3> path = Astar.m_Instance.GetPath();
            Gizmos.color = Color.blue;
            int iCount = path.Count - 1;
            int i;
            for (i = 0; i < iCount; i++)
            {
                Vector3 sPos = path[i];
                sPos.y += 1.0f;
                Vector3 ePos = path[i + 1];
                ePos.y += 1.0f;
                Gizmos.DrawLine(sPos, ePos);

            }

        }
    }
}
