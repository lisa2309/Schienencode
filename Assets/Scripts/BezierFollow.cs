using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/*

Modified: rotation
*/
/// <summary>
/// Bezier Curve for the train on the rail
/// </summary>
/// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
/// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// List of route subobjects, must contain route script and has 4 children to calculate beziercurve. Filled by routing script
    /// </summary>
    public List<Transform> routes;

    /// <summary>
    /// iteration parameter to iterate through the routes list
    /// </summary>
    private int routeToGo;

    /// <summary>
    /// variable to calculate the bezier shape (x of f(x))
    /// </summary>
    private float tParam;

    /// <summary>
    /// calculated position of the train
    /// </summary>
    private Vector3 trainPosition;

    /// <summary>
    /// used to controll train speed
    /// </summary>
    private float speedModifier;

    /// <summary>
    /// allows starting of goByTheRoute();
    /// </summary>
    public bool coroutineAllowed;

    /// <summary>
    /// rotation of the train
    /// </summary>
    private Vector3 rotationVector;

    /// <summary>
    /// inizializing for the Train
    /// </summary>
    /// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
    /// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
    private void Start()
    {
        routes = null;
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = false;
    }

    /// <summary>
    /// starts the train could be placed in other funktion in the future
    /// </summary>
    /// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
    /// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }

    }

    /// <summary>
    /// Moves the train on the rail
    /// p0: entrance point 
    /// p1: shape modifier point
    /// p2: shape modifier point
    /// p3: exit point
    /// </summary>
    /// <param name="routeNumber">next Point on the Bezier Curve</param>
    /// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
    /// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
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
            
            trainPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            rotationVector = trainPosition - transform.position;
            transform.position = trainPosition;
            transform.rotation = Quaternion.LookRotation(rotationVector);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Count - 1)
        {
            routeToGo = 0;
        }else
        {
            coroutineAllowed = true;
        }

    }

}

