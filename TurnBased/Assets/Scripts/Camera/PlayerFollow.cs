using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFollow : MonoBehaviour
{
    //position of the player/hero aand camera
    public Transform playerTransform, target, cameraPosition;

    //public Vector3 cameraOffset; 

    //camera smoothness
    //[Range(0.01f, 1f)]public float cameraSmoothness = 0.12f;

    //mouse control
    float mouseX, mouseY;
    [Range(0.1f, 100f)]public float mouseSensitivity = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = this.transform;
        target = this.transform.parent;
        //cameraOffset = transform.position - playerTransform.position;

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraControl();

        //Vector3 newPos = target.position + cameraOffset;
        //Vector3 smoothedPosition = Vector3.Slerp(transform.position, newPos, cameraSmoothness);
        //transform.position = smoothedPosition;
    }

    private void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity ;

        //prevent mouse flip
        mouseY = Mathf.Clamp(cameraPosition.position.y, 0f, 90f);

        //focus
        transform.LookAt(target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //camera rotate
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else 
        {
            //camera and player rotate
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            playerTransform.rotation = Quaternion.Euler(0, mouseX, 0);
        }

        

    }
}
