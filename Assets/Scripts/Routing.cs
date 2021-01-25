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
    private List<Transform> routepoints;
    bool finished = false;

    public bool straight = true;

    /// <summary>
    ///     This Should be triggered when Player is finished. Generates the Route and starts the Train.
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void GenerateRoute()
    {
        routepoints = new List<Transform>();
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

        buffer = GameObject.FindGameObjectWithTag("Start");
        if (buffer == null)
        {
            Debug.LogError("Start not found");
        }

        // Add Start Gemobject to Route
        routepoints.Add(buffer.transform.GetChild(0).Find("Route"));

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
                        Debug.Log("1: " + rail.transform.GetChild(0).Find("Route").Find("Point0").position + " 2: " + buffer.transform.GetChild(0).Find("Route").Find("Point3").position);
                        if (((buffer.name == "SwitchR0Final" || buffer.name == "SwitchR1Final") && straight && Vector3.Distance(rail.transform.GetChild(0).Find("Route").Find("Point0").position, buffer.transform.GetChild(1).Find("Route").Find("Point3").position) < 0.5f) || ((rail.name == "SwitchL0Final" || rail.name == "SwitchL1Final") && Vector3.Distance(rail.transform.GetChild(1).Find("Route").Find("Point0").position, buffer.transform.GetChild(0).Find("Route").Find("Point3").position) < 0.5f) || (Vector3.Distance(rail.transform.GetChild(0).Find("Route").Find("Point0").position, buffer.transform.GetChild(0).Find("Route").Find("Point3").position) < 0.5f))
                        {
                            Debug.Log("next rail found: " + rail.name);
                            
                            if ((rail.name == "SwitchR0Final" || rail.name == "SwitchR1Final") && straight)
                            {
                                routepoints.Add(rail.transform.GetChild(1).Find("Route"));
                            }
                            else if((rail.name == "SwitchL0Final" || rail.name == "SwitchL1Final"))
                            {
                                if (Vector3.Distance(rail.transform.GetChild(0).Find("Route").Find("Point0").position, buffer.transform.GetChild(0).Find("Route").Find("Point3").position) < 0.5f) {
                                    routepoints.Add(rail.transform.GetChild(0).Find("Route"));
                                } else
                                {
                                    routepoints.Add(rail.transform.GetChild(1).Find("Route"));
                                }
                            }
                            else
                            {
                                routepoints.Add(rail.transform.GetChild(0).Find("Route"));
                            }

                           buffer = rail;

                            // check if finish is reached
                            if (rail == finish)
                            {
                                Debug.Log("Finish found");
                                finished = true;
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
    private double getDirectionX(GameObject obj)
    {
        //Debug.Log("schienewinkel: " + (int)obj.transform.localEulerAngles.y + " Name: " + obj.name);

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
        else if (obj.name == "SwitchR0Final")
        {
            if (straight)
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
            else
            {
                if ((int)obj.transform.localEulerAngles.y == 0)
                {
                    return 0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return 1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 180)
                {
                    return -0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return -1;
                }
            }
        }
        else if (obj.name == "SwitchR1Final")
        {
            if (straight)
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
            else
            {
                if ((int)obj.transform.localEulerAngles.y == 0)
                {
                    return 0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return -1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 180)
                {
                    return -0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return 1;
                }
            }
        }
        else if (obj.name == "SwitchL0Final" || obj.name == "SwitchL1Final")
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
        return 0;
    }

    /// <summary>
    ///     Returns the neutral number in z direction (positive or negative) based on GameObject Orientation
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>+1, -1 or 0 based on GameObject orientation</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private double getDirectionZ(GameObject obj)
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
        else if (obj.name == "SwitchR0Final")
        {
            if (straight)
            {
                if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return -1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return 1;
                }
            }
            else
            {
                if ((int)obj.transform.localEulerAngles.y == 0)
                {
                    return 1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return -0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 180)
                {
                    return -1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return 0.5;
                }
            }
        }
        else if (obj.name == "SwitchR1Final")
        {
            if (straight)
            {
                if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return -1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return 1;
                }
            }
            else
            {
                if ((int)obj.transform.localEulerAngles.y == 0)
                {
                    return -1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 90)
                {
                    return -0.5;
                }
                else if ((int)obj.transform.localEulerAngles.y == 180)
                {
                    return 1;
                }
                else if ((int)obj.transform.localEulerAngles.y == 270)
                {
                    return 0.5;
                }
            }
        }
        else if (obj.name == "SwitchL0Final" || obj.name == "SwitchL1Final")
        {
            if ((int)obj.transform.localEulerAngles.y == 90)
            {
                return 1;
            }
            else if ((int)obj.transform.localEulerAngles.y == 270)
            {
                return -1;
            }
        }
        return 0;
    }
}
