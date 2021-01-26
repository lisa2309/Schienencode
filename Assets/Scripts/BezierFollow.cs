using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/*
Quelle: https://www.youtube.com/watch?v=11ofnLOE8pw
Modified: rotation
*/
/// <summary>
/// Bezier Curve for the train on the rail
/// </summary>
/// @author Florian Vogel & Bjarne Bensel
public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// </summary>
    public List<Transform> routes;

    /// <summary>
    /// 
    /// </summary>
    private int routeToGo;

    /// <summary>
    /// 
    /// </summary>
    private float tParam;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 catPosition;

    /// <summary>
    /// 
    /// </summary>
    private float speedModifier;

    /// <summary>
    /// 
    /// </summary>
    public bool coroutineAllowed;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 rotationVector;

    /// <summary>
    /// inizializing for the Train
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel
    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    /// <summary>
    /// starts the train could be placed in other funktion in the future
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel
    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    /// <summary>
    /// Moves the train on the rail
    /// p0:
    /// p1:
    /// p2:
    /// p3:
    /// </summary>
    /// <param name="routeNumber">next Point on the Bezier Curve</param>
    /// @author Florian Vogel & Bjarne Bensel
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            
            catPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            rotationVector = catPosition - transform.position;
            transform.position = catPosition;
            transform.rotation = Quaternion.LookRotation(rotationVector);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Count - 1)
        {
            routeToGo = 0;
        }
        coroutineAllowed = true;
    }

}

