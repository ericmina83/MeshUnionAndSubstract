using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public MyCube selectedCube;
    public LayerMask layerMask;

    public static BoxController instance;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layerMask.value))
            {
                selectedCube = hit.transform.gameObject.GetComponent<MyCube>();
                selectedCube.selected = true;
            }
            else
                selectedCube = null;
        }
        else if (Input.GetMouseButton(0))
        {
            if (selectedCube != null)
                selectedCube.transform.position = GetXZPlanePos(ray, selectedCube.transform.position.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedCube != null)
            {
                selectedCube.selected = false;
                selectedCube = null;
            }
        }
    }

    Vector3 GetXZPlanePos(Ray ray, float y)
    {
        return (y - ray.origin.y) / ray.direction.y * ray.direction + ray.origin;
    }
}
