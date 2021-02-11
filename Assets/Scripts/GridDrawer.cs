using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridDrawer : MonoBehaviour
{
    public Color lineColor;
    public Terrain terrain;
    private Mesh gridMesh;

    void Start()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        
        if (meshFilter.sharedMesh is null) {
            meshFilter.sharedMesh = new Mesh();
        }
        gridMesh = meshFilter.sharedMesh;
    }

    private void Update()
    {
        int sizeX = (int)terrain.terrainData.size.x;
        int sizeZ = (int)terrain.terrainData.size.z;
        var terrainPos = terrain.transform.position;
        transform.position = terrainPos;

        gridMesh.Clear();

        Color[] colors = new Color[(sizeX + sizeZ) * 2];
        Vector3[] vertices = new Vector3[(sizeX + sizeZ) * 2];
        int[] lines = new int[(sizeX + sizeZ) * 2];

        for (int i = 0; i < sizeX; i++)
        {
            var index = 2*i;

            vertices[index] = new Vector3(0,terrainPos.y,i);
            vertices[index+1] = new Vector3(sizeX,terrainPos.y,i);

            lines[index] = index;
            lines[index+1] = index+1;

            colors[index] = lineColor;
            colors[index+1] = lineColor;

        }

        for (int i = 0; i < sizeZ; i++)
        {
            var index = 2*sizeX+2*i;

            vertices[index] = new Vector3(i,terrainPos.y,0);
            vertices[index+1] = new Vector3(i,terrainPos.y,sizeZ);

            lines[index] = index;
            lines[index+1] = index+1;


            colors[index] = lineColor;
            colors[index+1] = lineColor;
        }

        gridMesh.vertices = vertices;
        gridMesh.colors = colors;
        gridMesh.SetIndices(lines, MeshTopology.Lines, 0, true);
    }
}
