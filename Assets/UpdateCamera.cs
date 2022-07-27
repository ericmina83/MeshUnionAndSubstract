using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamera : MonoBehaviour
{
    public Transform targetCamera;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = targetCamera.localRotation;
        transform.localPosition = targetCamera.localPosition;
    }
}
