using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridTest
    {
        [UnityTest]
        public IEnumerator GetNearestPointX_Success()
        {
            // Test1
            // ASSIGN
            GameObject gameObject = new GameObject("TestGridX");
            Grid g = gameObject.AddComponent<Grid>();

            // ACT
            Vector3 input = new Vector3(3, 5, 3);
            Vector3 result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(4f,result.x);

            // Test2
            // ACT
            input = new Vector3(0, 0, 0);
            result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(0f, result.x);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetNearestPointY_Success()
        {
            // Test1
            // ASSIGN
            GameObject gameObject = new GameObject("TestGridY");
            Grid g = gameObject.AddComponent<Grid>();

            // ACT
            Vector3 input = new Vector3(3, 5, 3);
            Vector3 result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(0.00f, result.y);

            // Test2
            // ACT
            input = new Vector3(0, 0, 0);
            result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(0f, result.y);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetNearestPointZ_Success()
        {
            // Test1
            // ASSIGN
            GameObject gameObject = new GameObject("TestGridZ");
            Grid g = gameObject.AddComponent<Grid>();

            // ACT
            Vector3 input = new Vector3(3, 5, 3);
            Vector3 result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(4f, result.z);

            // Test2
            // ACT
            input = new Vector3(0, 0, 0);
            result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(0f, result.z);

            yield return null;
        }

    }
}
