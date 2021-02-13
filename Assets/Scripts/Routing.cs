using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Calculates Route from Start to Finish, Finish Prefab has to be tagged as Finish
/// </summary>
/// @author Florian Vogel & Bjarne Bensel 
public class Routing : MonoBehaviour
{
    /// <summary>
    /// Prefabname of straight rail
    /// </summary>
    private const string RAILSTRAIGHT = "Straight270Final";

    /// <summary>
    /// Prefabname of Curve rail Left
    /// </summary>
    private const string RAILCURVELEFT = "CurveL0Final";

    /// <summary>
    /// Prefabname of Curve rail Right
    /// </summary>
    private const string RAILCURVERIGHT = "CurveR0Final";

    /// <summary>
    /// Prefabname of Switch rail Left
    /// </summary>
    private const string RAILSWITCHLEFT = "SwitchR0Final";

    /// <summary>
    /// Prefabname of Switch rail Right
    /// </summary>
    private const string RAILSWITCHRIGHT = "SwitchR1Final";

    /// <summary>
    /// Prefabname of Collection rail Right
    /// </summary>
    private const string RAILCOLLECTRIGHT = "SwitchL0Final";

    /// <summary>
    /// Prefabname of Collection rail Left
    /// </summary>
    private const string RAILCOLLECTLEFT = "SwitchL1Final";

    /// <summary>
    /// Prefabname of start rail
    /// </summary>
    private const string RAILSTART = "RailStart";

    /// <summary>
    /// Prefabname of End rail
    /// </summary>
    private const string RAILEND = "RailEnd";

    /// <summary>
    /// Prefabname of Trainstation
    /// </summary>
    private const string TRAINSTATION = "TrainStation";

    /// <summary>
    /// Array to count the amount of drive pasts
    /// </summary>
    int[] drivePast;

    /// <summary>
    /// Subelement of Rail Prefab corresponds to route where Route script is attached
    /// </summary>
    private const string BEZIERSHAPE = "Route";

    /// <summary>
    /// Subelement of route begin of BEZIERSHAPE
    /// </summary>
    private const string ENTRANCEPOINT = "Point0";

    /// <summary>
    /// Subelement of route end of BEZIERSHAPE
    /// </summary>
    private const string EXITPOINT = "Point3";


    /// <summary>
    /// List of all gameobject with tag "rail"
    /// </summary>
    List<GameObject> rails;

    /// <summary>
    /// Buffer to store last found rail
    /// </summary>
    private GameObject buffer;

    /// <summary>
    /// Calculated Route of bezier shapes
    /// </summary>
    private List<Transform> routePoints;

    /// <summary>
    /// Route found and Calculating finished
    /// </summary>
    private bool finished = false;

    /// <summary>
    /// Buffer for Route on last Switch
    /// </summary>
    private bool straight = true;

    /// <summary>
    /// This Should be triggered when Player is finished. Generates the Route and starts the Train.
    /// Variables:
    /// train: Gameobject with tag "Train" must contain BezierFollow script. The thing that Drives arround.
    /// finish: Gameobject with tag "finish". End of the route
    /// abortCounter: counts number of while iteration to abort infinit search of route.
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void GenerateRoute()
    {
        routePoints = new List<Transform>();
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
        routePoints.Add(buffer.transform.GetChild(0).Find(BEZIERSHAPE));

        //route.Add(buffer);
        //buffer.getdirektion

        // Find all Rail Parts
        rails = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rail"));

        // Add Finish Gameobject to Rails
        rails.Add(finish);

        Debug.Log("Größe rails List: " + rails.Count);

        drivePast = new int[rails.Count];

        int abortCounter = 0;
        finished = false;

        // Algorithm to build route, Limit iterations to terminate if unsuccessful to find route
        while (!finished && abortCounter < 10 * rails.Count)
        {
            abortCounter++;
            Debug.Log("While durchgang");

            // Find next rail in list of rails
            int i = 0; //counter for foreach
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
                        Debug.Log("1: " + rail.transform.GetChild(0).Find(BEZIERSHAPE).Find(ENTRANCEPOINT).position + " 2: " + buffer.transform.GetChild(0).Find(BEZIERSHAPE).Find(EXITPOINT).position);
                        if (((buffer.name.Contains(RAILSWITCHLEFT) || buffer.name.Contains(RAILSWITCHRIGHT)) && straight && Vector3.Distance(rail.transform.GetChild(0).Find(BEZIERSHAPE).Find(ENTRANCEPOINT).position, buffer.transform.GetChild(1).Find(BEZIERSHAPE).Find(EXITPOINT).position) < 0.5f) || ((rail.name.Contains(RAILCOLLECTRIGHT) || rail.name.Contains(RAILCOLLECTLEFT)) && Vector3.Distance(rail.transform.GetChild(1).Find(BEZIERSHAPE).Find(ENTRANCEPOINT).position, buffer.transform.GetChild(0).Find(BEZIERSHAPE).Find(EXITPOINT).position) < 0.5f) || (Vector3.Distance(rail.transform.GetChild(0).Find(BEZIERSHAPE).Find(ENTRANCEPOINT).position, buffer.transform.GetChild(0).Find(BEZIERSHAPE).Find(EXITPOINT).position) < 0.5f))
                        {
                            Debug.Log("next rail found: " + rail.name);
                            drivePast[i]++;
                            if ((rail.name.Contains(RAILSWITCHLEFT) || rail.name.Contains(RAILSWITCHRIGHT)) && switchGoStraight(rail,i))
                            {
                                routePoints.Add(rail.transform.GetChild(1).Find(BEZIERSHAPE));
                            }
                            else if((rail.name.Contains(RAILCOLLECTRIGHT) || rail.name.Contains(RAILCOLLECTLEFT)))
                            {
                                if (Vector3.Distance(rail.transform.GetChild(0).Find(BEZIERSHAPE).Find(ENTRANCEPOINT).position, buffer.transform.GetChild(0).Find(BEZIERSHAPE).Find(EXITPOINT).position) < 0.5f) {
                                    routePoints.Add(rail.transform.GetChild(0).Find(BEZIERSHAPE));
                                } else
                                {
                                    routePoints.Add(rail.transform.GetChild(1).Find(BEZIERSHAPE));
                                }
                            }
                            else
                            {
                                routePoints.Add(rail.transform.GetChild(0).Find(BEZIERSHAPE));
                            }

                           buffer = rail;

                            // check if finish is reached
                            if (rail == finish)
                            {
                                Debug.Log("Finish found");
                                finished = true;
                                train.GetComponent<BezierFollow>().routes = routePoints;
                                train.GetComponent<BezierFollow>().coroutineAllowed = true;
                            }
                            break;
                        }
                    }
                }
                i++;
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
        Debug.Log("schienewinkel: " + (int)obj.transform.localEulerAngles.y + " Name: " + obj.name);

        if (obj.name.Contains(RAILSTRAIGHT) || obj.name.Contains(TRAINSTATION))
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
        else if (obj.name.Contains(RAILCURVELEFT))
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
        else if (obj.name.Contains(RAILCURVERIGHT))
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
        else if (obj.name.Contains(RAILSTART) || obj.name.Contains(RAILEND))
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
        else if (obj.name.Contains(RAILSWITCHLEFT))
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
        else if (obj.name.Contains(RAILSWITCHRIGHT))
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
        else if (obj.name.Contains(RAILCOLLECTRIGHT) || obj.name.Contains(RAILCOLLECTLEFT))
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
        if (obj.name.Contains(RAILSTRAIGHT) || obj.name.Contains(TRAINSTATION))
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
        else if (obj.name.Contains(RAILCURVELEFT))
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
        else if (obj.name.Contains(RAILCURVERIGHT))
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
        else if (obj.name.Contains(RAILSTART) || obj.name.Contains(RAILEND))
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
        else if (obj.name.Contains(RAILSWITCHLEFT))
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
        else if (obj.name.Contains(RAILSWITCHRIGHT))
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
        else if (obj.name.Contains(RAILCOLLECTRIGHT) || obj.name.Contains(RAILCOLLECTLEFT))
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

    private bool switchGoStraight(GameObject rail, int railCounter)
    {
        if (rail.name.Contains(RAILSWITCHLEFT) || rail.name.Contains(RAILSWITCHRIGHT))
        {
            Debug.Log(rail.name+" ABCDE "+rail.GetComponent<SwitchScript>().mode);
            
            switch (rail.GetComponent<SwitchScript>().mode)
            {
                case SwitchScript.SwitchMode.If:
                    {
                        int railNumber = getTrainStation(rail.GetComponent<SwitchScript>().ComparationValues[0]);
                        int cargoCount = rails[railNumber].transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber;
                        Debug.Log(railNumber+" - "+drivePast[railNumber]+" - "+rails[railNumber].name+" cargo: "+ cargoCount);
                        switch (rail.GetComponent<SwitchScript>().ComparationValues[1])
                        {
                            case 0: // >
                                straight = !((drivePast[railNumber] * cargoCount ) > rail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break; 
                            case 1: // <
                                straight = !((drivePast[railNumber] * cargoCount) < rail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break;
                            case 2: // ==                      
                                straight = !((drivePast[railNumber] * cargoCount) == rail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break;
                        }
                        break;
                    }
                case SwitchScript.SwitchMode.While:
                    {
                        Debug.Log("in While mode");
                        Debug.Log(" drivepasts: "+ drivePast[railCounter]+" < "+ rail.GetComponent<SwitchScript>().ComparationValues[2]);
                        straight = !((drivePast[railCounter] - 1) < rail.GetComponent<SwitchScript>().ComparationValues[2]);
                        break;
                    }
                default:
                    
                    straight = true;
                    break;

            }
            return straight;
        }
        else {
            return true;
        }
    }
    int getTrainStation(int i)
    {
        int x = 0;
        int counter = 0;
        foreach(GameObject rail in rails)
        {            
            if (rail.name.Contains(TRAINSTATION))
            {
                if (x == i)
                {
                    return counter;
                }
                else
                {
                    x++;
                }
            }
            counter++;
        }
        return -1;
    }

}
