using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMesh : MonoBehaviour
{
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        targetObject.GetComponent<MeshFilter>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        targetObject.GetComponent<MeshRenderer>().materials = GetComponent<MeshRenderer>().materials;
    }
}
