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
        public IEnumerator GetNearestPointY_Success()
        {
            // ASSIGN
            GameObject gameObject = new GameObject("TestGrid");
            gameObject.AddComponent<Transform>();
            Grid g = gameObject.AddComponent<Grid>();

            // ACT
            Vector3 input = new Vector3(2,5,10);
            Vector3 result = g.GetNearestPointOnGrid(input);

            // ASSERT 
            Assert.AreEqual(0.00f,result.y);

            yield return null;
        }

    }
}
