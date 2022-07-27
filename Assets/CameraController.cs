using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float angleSpeed = 2.5f;
    public float movingSpeed = 0.3f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += (transform.forward * z + transform.right * x) * movingSpeed;

        if (Input.GetMouseButton(1))
        {
            float xMove = Input.GetAxis("Mouse X");
            float yMove = Input.GetAxis("Mouse Y");

            float eulerAnglesY = transform.localRotation.eulerAngles.y + xMove * angleSpeed;
            float eulerAnglesX = transform.localRotation.eulerAngles.x - yMove * angleSpeed;
            transform.localRotation = Quaternion.Euler(eulerAnglesX, eulerAnglesY, 0);
        }
    }
}
