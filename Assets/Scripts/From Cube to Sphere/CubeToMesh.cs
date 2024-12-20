using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToMesh : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Surface[] surfaces;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[1];    //For a cube, replace 1 by 6, one for each face of the cube
        }
        
        surfaces = new Surface[1];  //For a cube, replace 1 by 6, one for each face of the cube

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        for (int i = 0; i < 1; i++) //For a cube, replace 1 by 6, one for each face of the cube
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                //meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            surfaces[i] = new Surface(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach (Surface face in surfaces) 
        { 
            face.ConstructMesh();
        }
    }
}
