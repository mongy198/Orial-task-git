using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Manager : MonoBehaviour
{
    [SerializeField]
    Transform player;
    float mouseX, mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        //set camera to follow player and rotate with mouse input
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -25, 25);
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        transform.position = player.position+Vector3.up;
    }
}
