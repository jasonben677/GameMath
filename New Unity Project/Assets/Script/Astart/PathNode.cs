using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PathNodeState
{ 
    Node_Null = -1,
    Node_Open,
    Node_Close
}
[System.Serializable]
public class PathNode 
{
    public string name;
    public GameObject goal;
    public List<PathNode> neighbor;

    public Vector3 mPos;
    public PathNode parent;
    public float fF;
    public float fG;
    public float fH;

    public PathNodeState nodeState;

}
