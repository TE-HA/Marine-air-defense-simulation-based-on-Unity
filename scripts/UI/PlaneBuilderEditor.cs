using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBuilderEditor : MonoBehaviour
{
    public int size = 1000;//边长
    public int segment = 5;//分段数
    public Material mat;//mesh材质
    private Vector3[] vertices;//顶点
    private Vector2[] uv;//纹理坐标
    private int[] triangles;//索引


    [ContextMenu("Create Mesh")]


    private void Start()
    {
        CreateMesh();
    }
    private void CreateMesh()
    {
        GameObject obj_cell = new GameObject();
        obj_cell.name = "cell";
        Mesh mesh = new Mesh();
        mesh.Clear();
        SetVertivesUV();//生成顶点和uv信息
        SetTriangles();//生成索引
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();//重置法线

        mesh.RecalculateBounds();   //重置范围
        obj_cell.AddComponent<MeshFilter>().mesh = mesh;
        obj_cell.AddComponent<MeshRenderer>();
        obj_cell.GetComponent<MeshRenderer>().material = mat;
    }

    /// <summary>
    /// 设置顶点信息
    /// </summary>
    private void SetVertivesUV()
    {
        vertices = new Vector3[(segment + 1) * (segment + 1)];
        uv = new Vector2[vertices.Length];
        int num = 0;
        float m = (float)size / (float)segment;
        for (int i = 0; i < segment + 1; i++)
            for (int j = 0; j < segment + 1; j++)
            {
                vertices[num] = new Vector3(j * m, 0, i * m);
                uv[num] = new Vector2((float)j / segment, (float)i / segment);
                num++;
            }

    }
    /// <summary>
    /// 设置索引
    /// </summary>
    private void SetTriangles()
    {
        triangles = new int[segment * segment * 6];
        int index = 0;//用来给三角形索引计数
        for (int i = 0; i < segment; i++)
            for (int j = 0; j < segment; j++)
            {
                int line = segment + 1;
                int self = j + (i * line);

                triangles[index] = self;
                triangles[index + 1] = self + line + 1;
                triangles[index + 2] = self + 1;
                triangles[index + 3] = self;
                triangles[index + 4] = self + line;
                triangles[index + 5] = self + line + 1;

                index += 6;
            }
    }
}