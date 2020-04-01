using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Astar
{
    public WayPointTerrian m_Terrain;

    public List<PathNode> m_OpenList;
    public List<PathNode> m_CloseList;

    List<Vector3> m_PathList;

    static public Astar m_Instance;

    public void Init(WayPointTerrian terrain)
    {
        m_Terrain = terrain;
        m_OpenList = new List<PathNode>();
        m_CloseList = new List<PathNode>();
        m_PathList = new List<Vector3>();
        m_Instance = this;
    }

    public List<Vector3> GetPath()
    {
        return m_PathList;
    }

    private PathNode GetBestNode()
    {
        PathNode rn = null;

        float fMax = 10000.0f;

        foreach (PathNode n in m_OpenList)
        {
            if (n.fF < fMax)
            {
                fMax = n.fF;
                rn = n;
            }
        }

        m_OpenList.Remove(rn);

        return rn;
    }

    private void BuildPath(Vector3 sPos, Vector3 ePos, PathNode sNode, PathNode eNode)
    {
        m_PathList.Clear();

        m_PathList.Add(sPos);

        if (sNode == eNode)
        {
            m_PathList.Add(sNode.mPos);
        }
        else
        {
            PathNode pNode = eNode;
            while (pNode != null)
            {
                m_PathList.Insert(1, pNode.mPos);
                pNode = pNode.parent;
            }
        }

        m_PathList.Add(ePos);
    }

    public bool PerformAStar(Vector3 sPos, Vector3 ePos)
    {
        int iSF = 0;
        int iEF = 0;
        if (sPos.y > 4)
        {
            iSF = 1;
        }
        if (ePos.y > 4)
        {
            iEF = 1;
        }
        PathNode sNode = m_Terrain.GetNodeFromPosition(sPos, iSF);
        PathNode eNode = m_Terrain.GetNodeFromPosition(ePos, iEF);
        if (sNode == null || eNode == null)
        {
            Debug.Log("Can not find node in AStar map");
            return false;
        }
        else if (sNode == eNode)
        {
            // Build Path.
            BuildPath(sPos, ePos, sNode, eNode);
            return true;
        }

        m_OpenList.Clear();
        m_CloseList.Clear();
        m_Terrain.ClearAStarInfo();
        PathNode nNode;
        PathNode cNode;
        m_OpenList.Add(sNode);

        while (m_OpenList.Count > 0)
        {
            Debug.Log("AAA");
            cNode = GetBestNode();
            if (cNode == null)
            {
                Debug.Log("Get Best Node error.");
                return false;
            }
            else if (cNode == eNode)
            {
                // Build Path.
                BuildPath(sPos, ePos, sNode, eNode);
                return true;
            }

            int inNei = cNode.neighbor.Count;
            Debug.Log(cNode.mPos + " : " + inNei);
            Vector3 vDist;
            for (int i = 0; i < inNei; i++)
            {
                nNode = cNode.neighbor[i];
                if (nNode.nodeState == PathNodeState.Node_Close)
                {
                    continue;
                }
                else if (nNode.nodeState == PathNodeState.Node_Open)
                {
                    vDist = cNode.mPos - nNode.mPos;
                    float fNewG = cNode.fG + vDist.magnitude;
                    if (fNewG < nNode.fG)
                    {
                        nNode.fG = fNewG;
                        nNode.fF = nNode.fG + nNode.fH;
                        nNode.parent = cNode;
                    }
                    continue;
                }
                nNode.nodeState = PathNodeState.Node_Open;
                vDist = cNode.mPos - nNode.mPos;
                nNode.fG = cNode.fG + vDist.magnitude;
                vDist = eNode.mPos - nNode.mPos;
                nNode.fH = vDist.magnitude;
                nNode.fF = nNode.fG + nNode.fH;
                nNode.parent = cNode;
                m_OpenList.Add(nNode);
                Debug.Log("Add open node");
            }

            cNode.nodeState = PathNodeState.Node_Close;
        }

        Debug.Log("BBB");
        return false;
    }
}
