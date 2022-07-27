using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

public enum CubeType
{
    UNION,
    SUBSTRACT,
    PRIMITIVE,
}

// [ExecuteInEditMode]
public class MyCube : MonoBehaviour
{
    public MyCube subCube1;
    public MyCube subCube2;
    public CubeType type;
    public Color color;
    public Mesh mesh;
    public bool selected;
    public Material[] materials;

    // Update is called once per frame
    void Update()
    {
        Model result = null;
        switch (type)
        {
            case CubeType.PRIMITIVE:
                var material = GetComponent<MeshRenderer>().material;
                if (selected)
                    material.color = Color.yellow;
                else
                    material.color = color;
                return;
            case CubeType.UNION:
                result = CSG.Union(subCube1.gameObject, subCube2.gameObject);
                break;
            case CubeType.SUBSTRACT:
                result = CSG.Subtract(subCube1.gameObject, subCube2.gameObject);
                break;
            default:
                Debug.Log("error cube: " + name);
                return;
        }

        // update mesh
        if (mesh)
            Destroy(mesh);
        mesh = GetComponent<MeshFilter>().sharedMesh = result.mesh;

        // update materials
        foreach (var m in GetComponent<MeshRenderer>().materials)// GetComponent<MeshRenderer>().materials)
            Destroy(m);
        materials = GetComponent<MeshRenderer>().materials = result.materials.ToArray();
    }
}
