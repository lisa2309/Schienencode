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

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(182, 0, 56), Quaternion.AngleAxis(-90, Vector3.up));
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(192, 0, 56), Quaternion.AngleAxis(-90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(192, 0, 50), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(196, 0, 50), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 50), Quaternion.AngleAxis(-90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;

			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		// Straight
		[UnityTest]
		public IEnumerator RoutingTestStraight0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(202, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestStraight90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(194, 0, 64), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(194, 0, 60), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestStraight180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(206, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(202, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestStraight270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/Straight270Final.prefab", typeof(GameObject)), new Vector3(194, 0, 60), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(194, 0, 64), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		// CurveL
		[UnityTest]
		public IEnumerator RoutingTestCurveL0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 60), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(198, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(194, 0, 54), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveL90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(194, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(194, 0, 62), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveL180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(196, 0, 54), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(196, 0, 60), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 60), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveL270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(192, 0, 60), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveL0Final.prefab", typeof(GameObject)), new Vector3(198, 0, 60), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		//CurveR
		[UnityTest]
		public IEnumerator RoutingTestCurveR0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 52), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 58), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(196, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveR90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(196, 0, 54), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(202, 0, 54), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(202, 0, 58), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveR180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 60), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(198, 0, 54), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(202, 0, 54), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestCurveR270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(206, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		//SwitchL0
		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Straigth0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(204, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(196, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Curve0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 62), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(196, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Straigth90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 54), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(198, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 62), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Curve90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(204, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(198, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 62), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Straigth180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(196, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Curve180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 50), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(200, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Straigth270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(202, 0, 62), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(202, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(202, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL0Curve270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(196, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL0Final.prefab", typeof(GameObject)), new Vector3(202, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(202, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		//SwitchL1
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Straigth0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(206, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(202, 0, 58), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Curve0()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(202, 0, 52), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(202, 0, 58), Quaternion.AngleAxis(0, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(198, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Straigth90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(204, 0, 52), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(204, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 60), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Curve90()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(204, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 60), Quaternion.AngleAxis(180, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Straigth180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(196, 0, 54), Quaternion.AngleAxis(270, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(200, 0, 54), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 54), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Curve180()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 60), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(200, 0, 54), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(204, 0, 54), Quaternion.AngleAxis(270, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Straigth270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(200, 0, 62), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(200, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}

		[UnityTest]
		public IEnumerator RoutingTestSwitchL1Curve270()
		{
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(206, 0, 58), Quaternion.AngleAxis(90, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(200, 0, 58), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(200, 0, 54), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject gameObject = new GameObject("TestGridY");
			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileStraigthGT()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 0, 3 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileStraigthLT()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 1, 1 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileStraigthEQ()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 2, 3 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileCurveGT()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(188, 0, 52), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 0, 1 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileCurveLT()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(188, 0, 52), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 1, 3 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1IFWhileCurveEQ()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(188, 0, 52), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject station = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trainstations/TrainStation.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			station.transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber = 2;
			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.If;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 2, 2 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();

			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestSwitchL1For()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(198, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			GameObject ifswitch = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchR0Final.prefab", typeof(GameObject)), new Vector3(190, 0, 56), Quaternion.AngleAxis(180, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(186, 0, 56), Quaternion.AngleAxis(90, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/SwitchL1Final.prefab", typeof(GameObject)), new Vector3(194, 0, 56), Quaternion.AngleAxis(0, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(188, 0, 50), Quaternion.AngleAxis(180, Vector3.up));
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/CurveR0Final.prefab", typeof(GameObject)), new Vector3(194, 0, 50), Quaternion.AngleAxis(90, Vector3.up));

			ifswitch.GetComponent<SwitchScript>().mode = SwitchMode.For;
			ifswitch.GetComponent<SwitchScript>().comparationValues = new int[] { 0, 0, 2 };

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();
			LogAssert.Expect(LogType.Log, " drivepasts: 2 < 2");
			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
		[UnityTest]
		public IEnumerator RoutingTestTunnel()
		{
			GameObject o = new GameObject("MissionProver");
			MissionProver m = o.AddComponent<MissionProver>();
			GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Trains/LocFinal.prefab", typeof(GameObject)));

			GameObject start = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailStart.prefab", typeof(GameObject)), new Vector3(40, 0, 16), Quaternion.AngleAxis(270, Vector3.up));
			GameObject tunnelIn = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Tunnel/TunnelIn.prefab", typeof(GameObject)), new Vector3(46, 0, 16), Quaternion.AngleAxis(270, Vector3.up));
			GameObject end = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Rails/RailEnd.prefab", typeof(GameObject)), new Vector3(62, 0, 26), Quaternion.AngleAxis(180, Vector3.up));
			GameObject tunnelOut = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Final/Tunnel/TunnelOut.prefab", typeof(GameObject)), new Vector3(62, 0, 22), Quaternion.AngleAxis(0, Vector3.up));
			end.GetComponent<Highscore>().enabled = false;
			start.GetComponent<StartRailScript>().enabled = false;
			tunnelIn.GetComponent<InTunnelScript>().relatedOutTunnelNumber = tunnelOut.GetComponent<OutTunnelScript>().OutTunnelNumber;

			GameObject gameObject = new GameObject("TestGridY");

			Routing r = gameObject.AddComponent<Routing>();
			r.GenerateRoute();
			LogAssert.Expect(LogType.Log, "Track completed");

			yield return null;
		}
	}
}
