using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {
        Camera.main.fieldOfView = 59;
        Camera.main.transform.position = new Vector3(50.2f,41.1f,10.6f);
        // x = 50.19
        // y = 41.08
        // z = 10.6
    }
    

    void  Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 20)
            {
                Camera.main.fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView < 59)
            {
                Camera.main.fieldOfView++;
            }
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            //Vector3 cameraPosition = Camera.main.transform.position + new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            //if ()
            //{
            //    Camera.main.transform.position = cameraPosition;
            //}
            Camera.main.transform.position = Camera.main.transform.position + new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            //var pos = Camera.main.transform.localPosition;
            //pos.z += Input.GetAxis("Mouse ScrollWheel");
            //pos.z = Mathf.Clamp(pos.z, 20, 50);
            //Camera.main.transform.localPosition = pos;
        }
    }


}