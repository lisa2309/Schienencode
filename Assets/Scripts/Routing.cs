using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
///     Calculates Route from Start to Finish, Finish Prefab has to be tagged as Finish
/// </summary>
/// @author Florian Vogel & Bjarne Bensel 
public class Routing : MonoBehaviour
{
    List<GameObject> rails;
    GameObject buffer;
    List<GameObject> route = new List<GameObject>();
    bool finished = false;

    /// <summary>
    ///     This Should be triggered when Player is finished. Generates the Route and starts the Train.
    /// </summary>
    /// <param name="start">
    ///     GameObject which is the Startpoint for the Route.
    /// </param>
    /// @author Florian Vogel & Bjarne Bensel 
    void GenerateRoute(GameObject start)
    {
        // Find GameObject Train
        GameObject train = GameObject.FindGameObjectWithTag("Train");
        if (train == null)
        {
            Debug.LogError("Train not found");
        }

        // Find Gameobject Finish
        GameObject finish = GameObject.FindGameObjectWithTag("Finish");
        if (finish == null)
        {
            Debug.LogError("Finish not found");
        }

        //buffer = GameObject.FindGameObjectWithTag("Start");
        //if (buffer == null)
        //{
        //    Debug.LogError("Start not found");
        //}

        // Add Start Gemobject to Route
        route.Add(start);

        //route.Add(buffer);
        //buffer.getdirektion

        // Find all Rail Parts
        rails = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rail"));

        // Add Finish Gameobject to Rails
        rails.Add(finish);

        Debug.Log("Größe rails List: " + rails.Count);


        int boardcounter = 0;
        finished = false;

        // Algorithm to build route, Limit iterations to terminate if unsuccessful to find route
        while (!finished && boardcounter < 10 * rails.Count)
        {
            boardcounter++;
            Debug.Log("While durchgang");

            // Find next rail in list of rails
            foreach (GameObject rail in rails)
            {
                Debug.Log("rail komponente: " + rail.transform);
                Debug.Log("x: " + rail.transform.position.x + " = " + buffer.transform.position.x + " + " + getDirectionX(buffer));

                // check for next rail in x direction, distance for straigth = 4, distance for curve = 6
                if (rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer) * 4) || rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer) * 6))
                {
                    Debug.Log("z: " + rail.transform.position.z + " = " + buffer.transform.position.z + " + " + getDirectionZ(buffer));
                    // check for next rail in z direction, distance for straigth = 4, distance for curve = 6
                    if (rail.transform.position.z == (buffer.transform.position.z + getDirectionZ(buffer) * 4) || rail.transform.position.z == (buffer.transform.position.z + getDirectionZ(buffer) * 6))
                    {
                        // check if next rail is directly connected
                        if (Vector3.Distance(rail.transform.GetChild(0).Find("Route").Find("Point0").position, buffer.transform.GetChild(0).Find("Route").Find("Point3").position) < 0.5f)
                        {
                            buffer = rail;
                            route.Add(rail);

                            // check if finish is reached
                            if (rail == finish)
                            {
                                Debug.Log("Finish found");
                                finished = true;

                                // construct route and assign to train
                                List<Transform> routepoints = new List<Transform>();
                                foreach (GameObject routePoint in route)
                                {
                                    routepoints.Add(routePoint.transform.GetChild(0).Find("Route"));
                                }
                                train.GetComponent<BezierFollow>().routes = routepoints;
                                train.GetComponent<BezierFollow>().coroutineAllowed = true;
                            }
                            break;
                        }
                    }
                }

            }

        }
    }

    /// <summary>
    ///     Returns the neutral number in x direction (positive or negative) based on GameObject Orientation
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>+1, -1 or 0 based on Prefab orientation</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private int getDirectionX(GameObject obj)
    {
        Debug.Log("schienewinkel: " + (int)obj.transform.localEulerAngles.y + " Name: " + obj.name);

        if (obj.name == "Straight270Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 0)
            {
                return 1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 180)
            {
                return -1;
            }
        }
        else if (obj.name == "CurveL0Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 0)
            {
                return -1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 180)
            {
                return 1;
            }
        }
        else if (obj.name == "CurveR0Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 0)
            {
                return -1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 180)
            {
                return 1;
            }
        }
        else if (obj.name == "RailStart" || obj.name == "RailEnd")
        {
            if ((int)obj.transform.localEulerAngles.y == 270)
            {
                return 1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 90)
            {
                return -1;
            }
        }
        return 0;
    }

    /// <summary>
    ///     Returns the neutral number in z direction (positive or negative) based on GameObject Orientation
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>+1, -1 or 0 based on GameObject orientation</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private int getDirectionZ(GameObject obj)
    {
        if (obj.name == "Straight270Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 270)
            {
                return 1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 90)
            {
                return -1;
            }
        }
        else if (obj.name == "CurveL0Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 270)
            {
                return -1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 90)
            {
                return 1;
            }
        }
        else if (obj.name == "CurveR0Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 270)
            {
                return -1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 90)
            {
                return 1;
            }
        }
        else if (obj.name == "RailStart" || obj.name == "RailEnd")
        {
            if ((int)obj.transform.localEulerAngles.y == 180)
            {
                return 1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 0)
            {
                return -1;
            }
        }
        return 0;
    }
}
