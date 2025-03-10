﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=3MoHJtBnn2U
public class WaterPlaneGenerator : MonoBehaviour {
    public float size = 1;
    public int gridSize = 16;

    private MeshFilter filter;

    void Start() {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }

    private Mesh GenerateMesh() {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();    // only stores x, z

        for(int x = 0; x <= gridSize; x++) {
            for(int y = 0; y <= gridSize; y++) {
                vertices.Add(new Vector3(-size * 0.5f + size * (x / ( (float) gridSize)), 0, -size * 0.5f + size * (y / ( (float) gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float) gridSize, y / (float) gridSize));
            }
        }

        List<int> triangles = new List<int>();
        int vertCount = gridSize + 1;

        for(int i = 0; i < vertCount * vertCount - vertCount; i++) {
            if((i + 1) % vertCount == 0) {
                continue;
            }
            triangles.AddRange(new List<int>() {
                i + 1 + vertCount, i + vertCount, i,    // first triangle
                i, i + 1, i + 1 + vertCount           // second triangle
            });
        }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}
