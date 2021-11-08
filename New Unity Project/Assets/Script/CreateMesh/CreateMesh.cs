using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{

    private void Start()
    {
        this.GetComponent<MeshFilter>().mesh = this.Test02();
        Graphics.DrawMesh(this.Test02(), Vector3.zero, Quaternion.identity, this.GetComponent<MeshRenderer>().sharedMaterial, 0);
    }


    private void Update()
    {
    }
    private Mesh Test()
    {
        Mesh mesh = new Mesh();

        float width = 3;
        float height = 3;

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[3 * 2];

        vertices[0] = new Vector3(-(width / 2), (height / 2));
        vertices[1] = new Vector3((width / 2), (height / 2));
        vertices[2] = new Vector3(-(width / 2), -(height / 2));
        vertices[3] = new Vector3((width / 2), -(height / 2));

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        return mesh;
    }

    private Mesh Test02()
    {
        Mesh mesh = new Mesh();

        float width = 3;
        float height = 3;

        Vector3[] vertices = new Vector3[5];
        Vector2[] uv = new Vector2[5];
        int[] lines = new int[4*3];

        vertices[0] = new Vector3(-(width / 2), (height / 2));
        vertices[1] = new Vector3((width / 2), (height / 2));
        vertices[2] = Vector3.zero;
        vertices[3] = new Vector3(-(width / 2), -(height / 2));
        vertices[4] = new Vector3((width / 2), -(height / 2));

        //uv[0] = new Vector2(0, 1);
        //uv[1] = new Vector2(1, 1);
        //uv[2] = new Vector2(0, 0);
        //uv[3] = new Vector2(1, 0);
        //uv[4] = new Vector2(1, 0);

        lines[0] = 0;
        lines[1] = 1;

        lines[2] = 1;
        lines[3] = 4;

        lines[4] = 4;
        lines[5] = 3;

        lines[6] = 3;
        lines[7] = 0;

        lines[8] = 0;
        lines[9] = 4;

        lines[10] = 1;
        lines[11] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        //mesh.triangles = triangles;
        mesh.SetIndices(lines, MeshTopology.Lines, 0);

        return mesh;

    }

}
