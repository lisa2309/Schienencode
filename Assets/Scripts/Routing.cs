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
    private const string railStraight = "Straight270Final";

    /// <summary>
    /// Prefabname of Curve rail Left
    /// </summary>
    private const string railCurveLeft = "CurveL0Final";

    /// <summary>
    /// Prefabname of Curve rail Right
    /// </summary>
    private const string railCurveRight = "CurveR0Final";

    /// <summary>
    /// Prefabname of Switch rail Left
    /// </summary>
    private const string railSwitchLeft = "SwitchR0Final";

    /// <summary>
    /// Prefabname of Switch rail Right
    /// </summary>
    private const string railSwitchRight = "SwitchR1Final";

    /// <summary>
    /// Prefabname of Collection rail Right
    /// </summary>
    private const string railCollectRight = "SwitchL0Final";

    /// <summary>
    /// Prefabname of Collection rail Left
    /// </summary>
    private const string railCollectLeft = "SwitchL1Final";

    /// <summary>
    /// Prefabname of start rail
    /// </summary>
    private const string railStart = "RailStart";

    /// <summary>
    /// Prefabname of End rail
    /// </summary>
    private const string railEnd = "RailEnd";

    /// <summary>
    /// Prefabname of Trainstation
    /// </summary>
    private const string trainStation = "TrainStation";

    /// <summary>
    /// Prefabname of Tunnel entrance
    /// </summary>
    private const string tunnelIn = "TunnelIn";

    /// <summary>
    /// Prefabname of Tunnel exit
    /// </summary>
    private const string tunnelOut = "TunnelOut";

    /// <summary>
    /// Array to count the amount of drive pasts
    /// </summary>
    int[] drivePast;

    /// <summary>
    /// Subelement of Rail Prefab corresponds to route where Route script is attached
    /// </summary>
    private const string bezierShape = "Route";

    /// <summary>
    /// Subelement of route begin of BEZIERSHAPE
    /// </summary>
    private const string entrancePoint = "Point0";

    /// <summary>
    /// Subelement of route end of BEZIERSHAPE
    /// </summary>
    private const string exitPoint = "Point3";

    /// <summary>
    /// List of all gameobject with tag "rail"
    /// </summary>
    List<GameObject> rails;

    /// <summary>
    /// List of all gameobject with tag "rail" and Prefabname TUNNELOUT
    /// </summary>
    List<GameObject> tunnelExits;

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
    /// Bool for Route if last Switch was straight or not
    /// </summary>
    private bool straight = true;

    /// <summary>
    /// Maximum Value of driving past a single rail
    /// </summary>
    public int maxDrivepasts = 20;

    /// <summary>
    /// Bool to save if tunnel was found
    /// </summary>
    private bool tunnelFound = true;

	/// <summary>
    /// Button to start the train
    /// </summary>
	public GameObject startTrain;

	/// <summary>
    /// Button to delet rails
    /// </summary>
	public GameObject trash;

	/// <summary>
    /// Background for the deletebutton
    /// </summary>
	public GameObject trashBackground;

    /// <summary>
    /// This Should be triggered when Player is finished. Generates the Route and starts the Train.
    /// Variables:
    /// train: Gameobject with tag "Train" must contain BezierFollow script. The thing that Drives arround.
    /// finish: Gameobject with tag "finish". End of the route
    /// railFound: boolean if a rail was found. Abort search if no next rail was found
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void GenerateRoute()
    {
        GameObject missionProver = GameObject.FindGameObjectWithTag("MissionProver");
        bool drivepastsReached = false;
        routePoints = new List<Transform>();

        GameObject train = GameObject.FindGameObjectWithTag("Train");
        if (train == null)
        {
            Debug.LogError("Train not found");
        }

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

        routePoints.Add(buffer.transform.GetChild(0).Find(bezierShape));

        rails = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rail"));

        rails.Add(finish);

        Debug.Log("Größe rails List: " + rails.Count);

        drivePast = new int[rails.Count];

        tunnelExits = new List<GameObject>();
        foreach (GameObject rail in rails)
        {
            if (rail.name.Contains(tunnelOut))
            {
                tunnelExits.Add(rail);
            }
        }

        finished = false;

        bool railFound = true;
        while (!finished && railFound)
        {
            railFound = false;
            Debug.Log("While durchgang");

            int i = 0;
            foreach (GameObject rail in rails)
            {
                Debug.Log("rail komponente: " + rail.transform);
                Debug.Log("x: " + rail.transform.position.x + " = " + buffer.transform.position.x + " + " + getDirectionX(buffer));

                if (rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer) * 4) || rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer) * 6))
                {
                    Debug.Log("z: " + rail.transform.position.z + " = " + buffer.transform.position.z + " + " + getDirectionZ(buffer));
                    if (rail.transform.position.z == (buffer.transform.position.z + getDirectionZ(buffer) * 4) || rail.transform.position.z == (buffer.transform.position.z + getDirectionZ(buffer) * 6))
                    {
                        Debug.Log("1: " + rail.transform.GetChild(0).Find(bezierShape).Find(entrancePoint).position + " 2: " + buffer.transform.GetChild(0).Find(bezierShape).Find(exitPoint).position);
                        if (((buffer.name.Contains(railSwitchLeft) || buffer.name.Contains(railSwitchRight)) && straight && Vector3.Distance(rail.transform.GetChild(0).Find(bezierShape).Find(entrancePoint).position, buffer.transform.GetChild(1).Find(bezierShape).Find(exitPoint).position) < 0.5f) || ((rail.name.Contains(railCollectRight) || rail.name.Contains(railCollectLeft)) && Vector3.Distance(rail.transform.GetChild(1).Find(bezierShape).Find(entrancePoint).position, buffer.transform.GetChild(0).Find(bezierShape).Find(exitPoint).position) < 0.5f) || (Vector3.Distance(rail.transform.GetChild(0).Find(bezierShape).Find(entrancePoint).position, buffer.transform.GetChild(0).Find(bezierShape).Find(exitPoint).position) < 0.5f))
                        {
                            Debug.Log("next rail found: " + rail.name);
                            railFound = true;
                            drivePast[i]++;
                            if(drivePast[i] > maxDrivepasts)
                            {
                                railFound = false;
                                drivepastsReached = true;
                                Debug.Log("Maximum drivepast reached");                                
                                missionProver.GetComponent<MissionProver>().DisplayAlert("Fehler", "Die maximale Anzahl an überfahrenen Schienen wurde erreicht, prüfe auf Endlosschleifen");
                                resetStartbutton();
                                break;
                            }
                            if ((rail.name.Contains(railSwitchLeft) || rail.name.Contains(railSwitchRight)) && switchGoStraight(rail, i))
                            {
                                routePoints.Add(rail.transform.GetChild(1).Find(bezierShape));
                                buffer = rail;
                            }
                            else if ((rail.name.Contains(railCollectRight) || rail.name.Contains(railCollectLeft)))
                            {
                                if (Vector3.Distance(rail.transform.GetChild(0).Find(bezierShape).Find(entrancePoint).position, buffer.transform.GetChild(0).Find(bezierShape).Find(exitPoint).position) < 0.5f)
                                {
                                    routePoints.Add(rail.transform.GetChild(0).Find(bezierShape));
                                }
                                else
                                {
                                    routePoints.Add(rail.transform.GetChild(1).Find(bezierShape));
                                }
                                buffer = rail;
                            }
                            else if (rail.name.Contains(tunnelIn))
                            {
                                tunnelFound = false;
                                routePoints.Add(rail.transform.GetChild(0).Find(bezierShape));
                                foreach (GameObject tunnel in tunnelExits)
                                {
                                    if (rail.GetComponent<InTunnelScript>().relatedOutTunnelNumber == tunnel.GetComponent<OutTunnelScript>().OutTunnelNumber)
                                    {
                                        routePoints.Add(tunnel.transform.GetChild(0).Find(bezierShape));
                                        buffer = tunnel;
                                        tunnelFound = true;
                                        break;
                                    }
                                }
                                if (!tunnelFound)
                                {
                                    missionProver.GetComponent<MissionProver>().DisplayAlert("Fehler", "Tunnelausgang wurde nicht gefunden");
                                    Debug.Log("Tunnel exit not found");
                                    resetStartbutton();
                                    break;
                                }
                            }
                            else
                            {
                                routePoints.Add(rail.transform.GetChild(0).Find(bezierShape));
                                buffer = rail;
                            }

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
        if (finished)
        {
            Debug.Log("Track completed");
        }
        else if (!railFound && !drivepastsReached && tunnelFound)
        {
            Debug.Log("Track incomplete");
            missionProver.GetComponent<MissionProver>().DisplayAlert("Fehler", "Start und Ziel sind nicht verbunden");
            resetStartbutton();
        }
    }

    /// <summary>
    /// Returns the neutral number in x direction (positive or negative) based on GameObject Orientation
    /// </summary>
    /// <param name="obj">accurent rail, to find next rail in X position</param>
    /// <returns>+1, -1 or 0 based on Prefab orientation</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private double getDirectionX(GameObject obj)
    {
        Debug.Log("schienewinkel: " + (int)obj.transform.localEulerAngles.y + " Name: " + obj.name);

        if (obj.name.Contains(railStraight) || obj.name.Contains(trainStation))
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
        else if (obj.name.Contains(tunnelOut))
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
        else if (obj.name.Contains(railCurveLeft))
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
        else if (obj.name.Contains(railCurveRight))
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
        else if (obj.name.Contains(railStart) || obj.name.Contains(railEnd))
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
        else if (obj.name.Contains(railSwitchLeft))
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
        else if (obj.name.Contains(railSwitchRight))
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
        else if (obj.name.Contains(railCollectRight) || obj.name.Contains(railCollectLeft))
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
    /// Returns the neutral number in z direction (positive or negative) based on GameObject Orientation
    /// </summary>
    /// <param name = "obj" > accurent rail, to find next rail in y position</param>
    /// <returns>+1, -1 or 0 based on GameObject orientation</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private double getDirectionZ(GameObject obj)
    {
        if (obj.name.Contains(railStraight) || obj.name.Contains(trainStation))
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
        else if (obj.name.Contains(tunnelOut))
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
        else if (obj.name.Contains(railCurveLeft))
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
        else if (obj.name.Contains(railCurveRight))
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
        else if (obj.name.Contains(railStart) || obj.name.Contains(railEnd))
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
        else if (obj.name.Contains(railSwitchLeft))
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
        else if (obj.name.Contains(railSwitchRight))
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
        else if (obj.name.Contains(railCollectRight) || obj.name.Contains(railCollectLeft))
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

    /// <summary>
    /// Returns either to go straight or turn at a switch
    /// </summary>
    /// <param name="switchrail">curent switch</param>
    /// <param name="railCounter">position in rails array of current switch</param>
    /// railNumber: position of trainstation in rails array
    /// cargoCount: multiplier for how many cargo is added at the trainstation
    /// <returns>bool value to go straight (true) or not (false)</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    private bool switchGoStraight(GameObject switchrail, int railCounter)
    {
        if (switchrail.name.Contains(railSwitchLeft) || switchrail.name.Contains(railSwitchRight))
        {
            Debug.Log(switchrail.name + " ABCDE " + switchrail.GetComponent<SwitchScript>().mode);

            switch (switchrail.GetComponent<SwitchScript>().mode)
            {
                case SwitchMode.If:
                case SwitchMode.While:
                    {
                        int railNumber = getTrainStation(switchrail.GetComponent<SwitchScript>().ComparationValues[0]);
                        int cargoCount = rails[railNumber].transform.GetChild(1).GetComponent<StationScript>().cargoAdditionNumber;
                        Debug.Log(railNumber + " - " + drivePast[railNumber] + " - " + rails[railNumber].name + " cargo: " + cargoCount);
                        switch (switchrail.GetComponent<SwitchScript>().ComparationValues[1])
                        {
                            case 0: 
                                straight = !((drivePast[railNumber] * cargoCount) > switchrail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break;
                            case 1: 
                                straight = !((drivePast[railNumber] * cargoCount) < switchrail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break;
                            case 2:                     
                                straight = !((drivePast[railNumber] * cargoCount) == switchrail.GetComponent<SwitchScript>().ComparationValues[2]);
                                break;
                        }
                        break;
                    }
                case SwitchMode.For:
                    {
                        Debug.Log("in For mode");
                        Debug.Log(" drivepasts: " + drivePast[railCounter] + " < " + switchrail.GetComponent<SwitchScript>().ComparationValues[2]);
                        straight = !((drivePast[railCounter] - 1) < switchrail.GetComponent<SwitchScript>().ComparationValues[2]);
                        break;
                    }
                default:

                    straight = true;
                    break;

            }
            return straight;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Returns position of a trainstation in rails array 
    /// </summary>
    /// <param name="trainstationCounter">Number of Trainstation to find in rails array</param>
    /// foundStation: counter for all found trainstations
    /// counter: position in rails array
    /// <returns>integer value between -1 (for error) and sizeofrails</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    int getTrainStation(int trainstationCounter)
    {
        int foundStation = 0;
        int counter = 0;
        foreach (GameObject rail in rails)
        {
            if (rail.name.Contains(trainStation))
            {
                if (rail.transform.GetChild(1).GetComponent<StationScript>().stationNumber == trainstationCounter)
                {
                    return counter;
                }
                else
                {
                    foundStation++;
                }
            }
            counter++;
        }
        return -1;
    }

    private void resetStartbutton()
    {
        startTrain.SetActive(true);
        trash.SetActive(true);
        trashBackground.SetActive(true);
    }
}
