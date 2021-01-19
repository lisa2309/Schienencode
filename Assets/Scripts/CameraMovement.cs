﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

/// <summary>
/// In the beginning, the camera is positioned correctly. 
/// In the game, this class takes care of zooming in closer and keeping the player within his game world boundary. 
/// @author Ronja Haas & Anna-Lisa Müller 
/// </summary>
public class CameraMovement : MonoBehaviour
{
    private const int maxCameraXPosition = 80;
    private const int minCameraXPosition = 22;
    private const int maxCameraZPosition = 30;
    private const int minCameraZPosition = 0;
    private const int maxFieldOfView = 59;
    private const int minFieldOfView = 20;
    private Vector3 playerOneCamera = new Vector3(50.2f, 41.1f, 10.6f);

    /// <summary>
    /// Positions the camera correctly 
    /// @author Ronja Haas & Anna-Lisa Müller 
    /// </summary>
    void Start()
    {
        MaxFieldCameraView();   
    }

    /// <summary>
    /// Ensures that the player can zoom and stay within his game world boundary.
    /// @author Ronja Haas & Anna-Lisa Müller 
    /// </summary>
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView == maxFieldOfView)
            {
                Camera.main.fieldOfView = minFieldOfView; 
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView == minFieldOfView)
            {
                MaxFieldCameraView();
            }
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 cameraPosition = Camera.main.transform.position + new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            if (cameraPosition.z > minCameraZPosition && cameraPosition.z < maxCameraZPosition) 
            {
                if (cameraPosition.x > minCameraXPosition && cameraPosition.x < maxCameraXPosition)
                {
                    Camera.main.transform.position = cameraPosition;
                }   
            }  
        }
    }

    /// <summary>
    /// Set the camera to the right position, when the player use zoom out
    /// @author Ronja Haas & Anna-Lisa Müller 
    /// </summary>
    void MaxFieldCameraView()
    {
        Camera.main.transform.position = playerOneCamera;
        Camera.main.fieldOfView = maxFieldOfView;
    }

}