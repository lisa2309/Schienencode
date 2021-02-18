﻿
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a trainstation-prefab which have influence to the route of the train, so the player can define
/// which path the train should chose
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde
public class StationScript : MonoBehaviour
{
    /// <summary>
    /// ID of the station
    /// </summary>
    private int _stationNumber;

    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private MissionProver _prover;
    
    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// Given value by the user to increase the cargovalue of the station with each time the train passes the station.
    /// 1 by default
    /// </summary>
    public int cargoAdditionNumber;


    /// <summary>
    /// Called by Collision. Increases the cargovalue of the station by the cargoAdditionNumber
    /// </summary>
    /// <param name="other">the collider-Object of the other object of the collision</param>
    /// @author Ahmed L'harrak & Bastian Badde
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("+++++++++++Collision with:" + other.name);
        _prover.RaiseCounter(_stationNumber);
    }
    
   
    /// <summary>
    /// Opens the relevant PopUp-Panel, when the train-station is clicked by mouse
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    void OnMouseDown()
    {
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
        {
            _prover.UpdateStation(this._stationNumber, this);
            OpenPanel();
        }
    }

    /// <summary>
    /// Opens the relevant popUp-Panel
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    public void OpenPanel()
    {
        panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {

                if (panel.name != "panel01")
                {
                    panel.gameObject.SetActive(false);
                }
                else
                {
                    if (!panel.gameObject.activeSelf)
                    {
                        MissionProver.panelisOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
            _prover.UpdateStationSettings();

        }
    }

    /// <summary>
    /// Initialises the components with default-Values
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    void Start()
    {
        cargoAdditionNumber = 1;
        _prover = FindObjectOfType<MissionProver>();
        this._stationNumber = _prover.RegisterNewStation();
        //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
        //popUpPanel = GameObject.Find("PopUpPanel");
    }
}
