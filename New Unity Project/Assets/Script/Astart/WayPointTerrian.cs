using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPointTerrian 
{
    public List<PathNode> m_NodeList;
    public GameObject[] m_Walls;
    public void Init()
    {
        m_Walls = GameObject.FindGameObjectsWithTag("Wall");
        m_NodeList = new List<PathNode>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("WayPoint");
        foreach (GameObject g in gos)
        {
            PathNode p = new PathNode();
            p.name = g.name;
            p.fF = p.fG = p.fH = 0.0f;
            p.parent = null;
            p.neighbor = new List<PathNode>();
            p.mPos = g.transform.position;
            p.goal = g;
            m_NodeList.Add(p);
        }
        LoadWP();
    }

    public void ClearAStarInfo()
    {
        foreach (PathNode node in m_NodeList)
        {
            node.parent = null;
            node.fF = 0.0f;
            node.fG = 0.0f;
            node.fH = 0.0f;
            node.nodeState = PathNodeState.Node_Null;
        }
    }

    public PathNode GetNodeFromPosition(Vector3 pos, int iFloor)
    {
        PathNode rnode = null;
        PathNode node;
        int iC = m_NodeList.Count;
        float max = 100000.0f;
        for (int i = 0; i < iC; i++)
        {
            node = m_NodeList[i];

            if (Physics.Linecast(pos, node.mPos, 1 << LayerMask.NameToLayer("Wall")))
            {
                continue;
            }
            Vector3 vec = node.mPos - pos;
            vec.y = 0.0f; // Optional
            float mag = vec.magnitude;
            if (mag < max)
            {
                max = mag;
                rnode = node;
            }
        }

        return rnode;
    }

    public void LoadWP()
    {
        for (int i = 0 ; i < m_NodeList.Count; i++)
        {
            int[] temp = new int[2];
            if (i == 0)
            {
                temp[0] = m_NodeList.Count - 1;
                temp[1] = i + 1;
            }
            else if(i == m_NodeList.Count - 1)
            {
                temp[0] = i - 1;
                temp[1] = 0;
            }
            else
            {
                temp[0] = i - 1;
                temp[0] = i + 1;
            }
            temp[0] = 1;
            temp[1] = 2;
            foreach (var item in temp)
            {
                m_NodeList[i].neighbor.Add(m_NodeList[item]);
            }
        }
    }


}
