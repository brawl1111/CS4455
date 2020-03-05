using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralTriangleMesh : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMeshData();
        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeMeshData()
    {
        // create an array of vertices
        vertices = new Vector3[] { new Vector3(-5, 0, -5), new Vector3(-5, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 5), new Vector3(-5, 0, 5), new Vector3(5, 0, 5), new Vector3(5, 0, 0), new Vector3(0, 0, -5), new Vector3(5, 0, -5) };

        // create an array of integers
        //triBotLeft = { 0, 1, 2 };
        //triTopLeft = { 4, 3, 2 };
        //triTopRight = {2, 5, 6 };
        triangles = new int[] { 0,1,2, 4,3,2, 2,5,6, 2,8,7 };

    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

}
