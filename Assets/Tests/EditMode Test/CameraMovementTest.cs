using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CameraMovementTest
    {
        // Test if maxCameraZPosition is not less than -35
        [Test]
        public void CameraMovementMaxZValue()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 cameraPos = camera.transform.position;
            float zTest = cameraPos.z;
            // ASSERT
            Assert.IsTrue(-35 <= zTest);
        }

        // Test if maxCameraZPosition is not greater than 30
        [Test]
        public void CameraMovementMinZValue()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 cameraPos = camera.transform.position;
            float zTest = cameraPos.z;
            // ASSERT
            Assert.IsTrue(30 >= zTest);
        }

        // Test if maxCameraXPosition is not greater than 80
        [Test]
        public void CameraMovementMaxXValue()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 cameraPos = camera.transform.position;
            float xTest = cameraPos.x;
            // ASSERT
            Assert.IsTrue(xTest <= 80);
        }

        // Test if maxCameraZPosition is not less than 22
        [Test]
        public void CameraMovementMinXValue()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 cameraPos = camera.transform.position;
            float xTest = cameraPos.x;
            // ASSERT
            Assert.IsTrue(22 <= xTest );
        }


        // Test if the variable maxFieldOfView is not less than 20
        [Test]
        public void CameraMovementMinCameraFieldView()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            float fTest = camera.fieldOfView;
            // ASSERT
            Assert.IsTrue(20 <= fTest);
        }

        // Test if the variable maxFieldOfView is not greater than 59 
        [Test]
        public void CameraMovementMaxCameraFieldView()
        {
            // ASSIGN
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            float fTest = camera.fieldOfView;
            // ASSERT
            Assert.IsTrue(fTest <= 59);
        }


    }
}
