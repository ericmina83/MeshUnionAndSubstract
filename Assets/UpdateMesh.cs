using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMesh : MonoBehaviour
{
    public MyCube targetCube;
    public Mesh mesh;
    List<Material> materials = new List<Material>();

    // Update is called once per frame
    void Update()
    {
        // update mesh
        if (mesh != null)
            Destroy(mesh);
        mesh = GetComponent<MeshFilter>().sharedMesh = Instantiate(targetCube.mesh);

        // update materials
        foreach (var material in materials)
            Destroy(material);
        materials.Clear();
        foreach (var material in targetCube.GetComponent<MeshRenderer>().materials)
            materials.Add(Instantiate(material));
        GetComponent<MeshRenderer>().materials = materials.ToArray();
    }
}
