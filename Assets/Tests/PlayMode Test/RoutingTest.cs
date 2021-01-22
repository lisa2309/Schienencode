using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RoutingTest
    {
        [UnityTest]
        public IEnumerator RoutingTestWithEnumeratorPasses()
        {
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(182,0,56), Quaternion.AngleAxis(-90, Vector3.up));
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(192, 0, 56), Quaternion.AngleAxis(-90, Vector3.up));
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(192, 0, 50), Quaternion.AngleAxis(180, Vector3.up));
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(196, 0, 50), Quaternion.AngleAxis(0, Vector3.up));
            GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 50), Quaternion.AngleAxis(-90, Vector3.up));

            GameObject gameObject = new GameObject("TestGridY");
            Routing r = gameObject.AddComponent<Routing>();
            r.GenerateRoute();

            LogAssert.Expect(LogType.Log, "Finish found");

            yield return null;
        }
    }
}
