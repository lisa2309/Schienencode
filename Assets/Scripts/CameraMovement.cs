using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

/// <summary>
/// In the beginning, the camera is positioned correctly. 
/// In the game, this class takes care of zooming in closer and keeping the player within his game world boundary. 
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller 
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Maximum x position from the camera
    /// </summary>
    private const int maxCameraXPosition = 80;

    /// <summary>
    /// Minimum x position from the camera
    /// </summary>
    private const int minCameraXPosition = 22;

    /// <summary>
    /// Maximum z position from the camera
    /// </summary>
    private const int maxCameraZPositionPlayer1 = 30;

    /// <summary>
    /// Minimum z position from the camera
    /// </summary>
    private const int minCameraZPositionPlayer1 = 0;

    /// <summary>
    /// Maximum z position from the camera
    /// </summary>
    private const int maxCameraZPositionPlayer2 = -5;

    /// <summary>
    /// Minimum z position from the camera
    /// </summary>
    private const int minCameraZPositionPlayer2 = -35;
    /// <summary>
    /// Maximum zoom position from the camera 
    /// </summary>
    private const int maxFieldOfView = 59;

    /// <summary>
    /// Minimum zoom position from the camera 
    /// </summary>
    private const int minFieldOfView = 20;

    /// <summary>
    /// This is the start camera position from player one
    /// </summary>
    private Vector3 playerOneCamera = new Vector3(50.2f, 41.1f, 11f);
    
    /// <summary>
    /// This is the start camera position from player two
    /// </summary>
    private Vector3 playerTwoCamera = new Vector3(50.2f, 41.1f, -46f);
    
    /// <summary>
    /// Object from Player
    /// </summary>
    private Player player;

    /// <summary>
    /// Ensures that the player can zoom and stay within his game world boundary.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
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
            if (Camera.main.fieldOfView == minFieldOfView)
            {
                Vector3 cameraPosition = Camera.main.transform.position + new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
                if (player.isServer)
                {
                    if (cameraPosition.z > minCameraZPositionPlayer1 && cameraPosition.z < maxCameraZPositionPlayer1)
                    {
                        if (cameraPosition.x > minCameraXPosition && cameraPosition.x < maxCameraXPosition)
                        {
                            Camera.main.transform.position = cameraPosition;
                        }
                    }
                } 
                else if (!player.isServer)
                {
                    if (cameraPosition.z > minCameraZPositionPlayer2 && cameraPosition.z < maxCameraZPositionPlayer2)
                    {
                        if (cameraPosition.x > minCameraXPosition && cameraPosition.x < maxCameraXPosition)
                        {
                            Camera.main.transform.position = cameraPosition;
                        }
                    }
                }
                
            }    
        }
    }

    /// <summary>
    /// Set the camera to the right position, when the player use zoom out
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    public void MaxFieldCameraView()
    {
        player = FindObjectOfType<Player>();
        if (player.isServer)
        {
            Camera.main.transform.position = playerOneCamera;
        }
        else if (!player.isServer)
        {
            Camera.main.transform.position = playerTwoCamera;
        }        
        Camera.main.fieldOfView = maxFieldOfView;
    }

}