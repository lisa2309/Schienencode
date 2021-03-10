using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is a major control-instance. It proves when a mission is completed and on which state it is.
/// It also handles the display-management of the PopUp-Panels and coordinates the different Prefab-settings.
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde
public class MissionProver : MonoBehaviour
{
    /// <summary>
    /// bool which is true when Prefabs should be deletable
    /// </summary>
    public static bool deleteOn;

    /// <summary>
    /// bool which is true when a PopUpPanel is open
    /// </summary>
    public static bool panelisOpen;

    /// <summary>
    /// bool which is true when objects are instantiated by the database
    /// </summary>
    public static bool buildOnDB;
    
    /// <summary>
    /// Mission-object of the loaded mission
    /// </summary>
    public Mission mission;

    /// <summary>
    /// UI-Text-object which is displayed in the missionfield of the inventory for Player 1
    /// </summary>
    public Text missiontext;
    
    /// <summary>
    /// UI-Text-object which is displayed in the missionfield of the inventory for Player 2
    /// </summary>
    public Text missiontextP2;

    /// <summary>
    /// UI-Text-object which is displayed after the train enters the End of the route
    /// </summary>
    public Text finalText;
    
    /// <summary>
    /// UI-Text-object which is displayed in the error-PopUp as caption
    /// </summary>
    public Text errorCaption;

    /// <summary>
    /// UI-Text-object which is displayed in the error-PopUp as error-message
    /// </summary>
    public Text errorText;

    /// <summary>
    /// Counter of registered TrainStations
    /// </summary>
    private int stationCounter = 0;
    
    /// <summary>
    /// Counter of registered OutTunnels
    /// </summary>
    private int tunnelCounter = 0;

    /// <summary>
    /// ID of the current selected TrainStation
    /// </summary>
    private int currentStation;
    
    /// <summary>
    /// List of cargoAddition-values of the different registered TrainsStations
    /// Accessible via: cargoAdditions[stationNumber]
    /// </summary>
    private List<int> cargoAdditions;

    /// <summary>
    /// UI-Dropdown-object of a TrainStation for choosing the cargoAdditionNumber
    /// </summary>
    public Dropdown ddStation;
    
    /// <summary>
    /// UI-Dropdown-object of an If-Switch for choosing the related TrainStation-Cargo
    /// </summary>
    public Dropdown ddSwitchValue;
    
    /// <summary>
    /// UI-Dropdown-object of an If-Switch for choosing a ComparationValue
    /// </summary>
    public Dropdown ddSwitchCompare;
    
    /// <summary>
    /// UI-Dropdown-object of a While-Switch for choosing the related TrainStation-Cargo
    /// </summary>
    public Dropdown ddWhileSwitchValue;
    
    /// <summary>
    /// UI-Dropdown-object of a While-Switch for choosing a ComparationValue
    /// </summary>
    public Dropdown ddWhileSwitchCompare;
    
    /// <summary>
    /// UI-Dropdown-object of a Unchosen-Switch for choosing the SwitchMode
    /// </summary>
    public Dropdown ddGeneralSwitch;
    
    /// <summary>
    /// UI-Dropdown-object of an InTunnel for choosing the related OutTunnel (tunnelNumber)
    /// </summary>
    public Dropdown ddInTunnelOpenOuts;
    
    /// <summary>
    /// UI-Inputfield-object of an If-Switch to enter the Value to compare with 
    /// </summary>
    public InputField inputIfSwitch;
    
    /// <summary>
    /// UI-Inputfield-object of a While-Switch to enter the Value to compare with 
    /// </summary>
    public InputField inputWhileSwitch;

    /// <summary>
    /// UI-Inputfield-object of For-Switch to enter the Value to compare with 
    /// </summary>
    public InputField inputForSwitch;

    /// <summary>
    /// UI-Text-object of a TrainStation to set the StationNumber in the Caption of the relevant PopUp-Panel 
    /// </summary>
    public Text stationCaption;
    
    /// <summary>
    /// UI-Text-object of a OutTunnel to set the TunnelNumber in the Caption of the relevant PopUp-Panel 
    /// </summary>
    public Text outTunnelCaption;

    /// <summary>
    /// SwitchScript-Object of the currently selected Switch
    /// </summary>
    private SwitchScript currentSwitch;

    /// <summary>
    /// StationScript-Object of the currently selected TrainStation
    /// </summary>
    private StationScript currentStationBody;

    /// <summary>
    /// InTunnelScript-Object of the currently selected InTunnel
    /// </summary>
    private InTunnelScript currentInTunnel;

    /// <summary>
    /// Sprite-object of the Trash-Button in white
    /// </summary>
    public Sprite DeleteImageWhite;

    /// <summary>
    /// Sprite-object of the Trash-Button in black
    /// </summary>
    public Sprite DeleteImageBlack;

    /// <summary>
    /// Button-object of the Trash-Button (for enabling deletion-mode)
    /// </summary>
    public Button DeleteButton;
    
    
    /// <summary>
    /// Creates a new stationNumber an registers a new TrainStation
    /// </summary>
    /// <returns>the stationNumber of the new registered TrainStation</returns>
    /// @author Bastian Badde
    public int RegisterNewStation()
    {
        cargoAdditions.Add(1);
        return stationCounter++;
        //carogoCounters = new int[stationCounter];
    }

    /// <summary>
    /// Sets the caption of the OutTunnel-PopUp-Panel
    /// </summary>
    /// @author Bastian Badde
    public void UpdateOutTunnel(int outTunnelNumber)
    {
        outTunnelCaption.text = "Tunnel-Ausgang: T" + outTunnelNumber;
    }
    
    /// <summary>
    /// Sets the settings used for appropriate displaying the PopUp of the current selected InTunnel
    /// </summary>
    /// <param name="inTunnel">the InTunnelScript-object of the currently selected InTunnel</param>
    /// @author Bastian Badde
    public void UpdateInTunnel(InTunnelScript inTunnel)
    {
        this.currentInTunnel = inTunnel;
        List<string> openTunnelStrings = new List<string>();
        int currentValue = 0;
        int tempValue = 0;
        foreach (int i in OutTunnelScript.GivenTunnelNumbers)
        {
            openTunnelStrings.Add("T" + i);
            if (i == inTunnel.relatedOutTunnelNumber) currentValue = tempValue;
            tempValue++;
        }
        ddInTunnelOpenOuts.options.Clear();
        ddInTunnelOpenOuts.AddOptions(openTunnelStrings);
        ddInTunnelOpenOuts.value = currentValue;
    }
    
    /// <summary>
    /// Raises the cargoValue of the relevant TrainStation by the relevant cargoAddition and updates the MissionField
    /// in the inventory
    /// </summary>
    /// <param name="stationNumber">the stationNumber of the relevant TrainStation</param> 
    /// @author Bastian Badde
    public void RaiseCounter(int stationNumber)
    {
        if (stationNumber > mission.cargos.Length)
        {
            Debug.Log("Inconsistent number of stations");
            return;
        }
        mission.cargoCounters[stationNumber]+= cargoAdditions[stationNumber];
        SetMissionField();
    }
    
    /// <summary>
    /// Sets the settings used for appropriate displaying the PopUp of the current selected Switch
    /// </summary>
    /// <param name="switchScript">the SwitchScript-object of the currently selected Switch</param>
    /// @author Bastian Badde
    public void UpdateSwitch(SwitchScript switchScript)
    {
        this.currentSwitch = switchScript;
        ddWhileSwitchCompare.value = ddSwitchCompare.value = switchScript.ComparationValues[1];
        ddWhileSwitchValue.value = ddSwitchValue.value = switchScript.ComparationValues[0];
        inputForSwitch.text = inputIfSwitch.text = inputWhileSwitch.text = switchScript.ComparationValues[2].ToString();
    }

    /// <summary>
    /// Sets the settings used for appropriate displaying the PopUp of the current selected TrainStation
    /// </summary>
    /// <param name="stationnumber">the stationNumber of the currently selected TrainStation</param>
    /// <param name="station">the StationScript-object of the currently selected TrainStation</param>
    /// @author Bastian Badde
    public void UpdateStation(int stationnumber, StationScript station)
    {
        this.currentStation = stationnumber;
        this.currentStationBody = station;
        this.stationCaption.text = "Bahnhof C" + (stationnumber + 1);
    }

    /// <summary>
    /// Resets the settings for the currently selected Switch.
    /// Called by the ResetButton of the Switch-PopUps.
    /// </summary>
    /// @author Bastian Badde
    public void ResetSwitchClicked()
    {
        currentSwitch.ResetSwitch();
        ClosePanel();
        currentSwitch.OpenPanel();
    }
    
    /// <summary>
    /// Updates the Dropdown-Value of the TrainStation-PopUp based on the currently selected TrainStation
    /// </summary>
    /// @author Bastian Badde
    public void UpdateStationSettings()
    {
        ddStation.value = cargoAdditions[currentStation] - 1;
    }
    
    /// <summary>
    /// Enters the settings of the currently selected TrainStation-PopUp.
    /// Called by the AcceptButton of the TrainStation-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void AcceptButtonClicked()
    {
        //Debug.Log("CCL: " + carogoCounters.Count + " CurrSt: " + currentStation);
        currentStationBody.cargoAdditionNumber = cargoAdditions[currentStation] = ddStation.value + 1;
        ClosePanel();
    }
   
    /// <summary>
    /// Enters the settings of the currently selected IfSwitch-PopUp.
    /// Called by the AcceptButton of the IfSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void IfSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues = new []
            {ddSwitchValue.value, ddSwitchCompare.value, Int32.Parse(inputIfSwitch.text)};
        // Debug.Log("current cargo: " + mission.cargoCounters[ddSwitchValue.value]);
        // Debug.Log("current compareVal: " + (ddSwitchCompare.value));
        // Debug.Log("current inputVal: " + Int32.Parse(inputIfSwitch.text));
        ClosePanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected WhileSwitch-PopUp.
    /// Called by the AcceptButton of the WhileSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void WhileSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues = new []
            {ddWhileSwitchValue.value, ddWhileSwitchCompare.value, Int32.Parse(inputWhileSwitch.text)};
        // Debug.Log("current cargo: " + mission.cargoCounters[ddSwitchValue.value]);
        // Debug.Log("current compareVal: " + (ddSwitchCompare.value));
        // Debug.Log("current inputVal: " + Int32.Parse(inputIfSwitch.text));
        ClosePanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected ForSwitch-PopUp.
    /// Called by the AcceptButton of the ForSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void ForSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues[2] = Int32.Parse(inputForSwitch.text);
        ClosePanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected general Switch-PopUp.
    /// Called by the AcceptButton of the general Switch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void GeneralSwitchAcceptButtonClicked()
    {
        currentSwitch.ChangeSwitchMode(ddGeneralSwitch.value);
        ClosePanel();
        currentSwitch.OpenPanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected InTunnel-PopUp.
    /// Called by the AcceptButton of the InTunnel-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void InTunnelAcceptedButtonClicked()
    {
        currentInTunnel.relatedOutTunnelNumber = OutTunnelScript.GivenTunnelNumbers[ddInTunnelOpenOuts.value];
        ClosePanel();
    }
    
    /// <summary>
    /// Closes the current open PopUp-panel.
    /// </summary>
    /// @author Bastian Badde
    public void ClosePanel()
    {
        MissionProver.panelisOpen = false;
        GameObject panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {
                    panel.gameObject.SetActive(false);
            }
            panels.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Switches the deletOn-bool to the opposite and changes the color of the Trash-Sprite of the Delete-Button.
    /// </summary>
    /// @author Bastian Badde
    public void DeleteClicked()
    {
        deleteOn = !deleteOn;
        if (deleteOn)
        {
            //Debug.Log("Switch to white");
            DeleteButton.GetComponent<Image>().sprite = DeleteImageWhite;
        }
        else
        {
            //Debug.Log("Switch to black");
            DeleteButton.GetComponent<Image>().sprite = DeleteImageBlack;
        }
    }
    
    /// <summary>
    /// Sets the Text which is displayed after the train enters the RailEnd-object.
    /// </summary>
    /// <param name="text">text to be displayed</param>
    /// @author Bastian Badde
    public void SetFinalText(string text)
    {
        finalText.text = text;
    }

    /// <summary>
    /// Builds the text to be displayed in the MissionField of the inventory and displays it.
    /// </summary>
    /// @author Bastian Badde
    private void SetMissionField()
    {
        missiontext.text = "Mission:\n\n";
        int i = 1;
        foreach (int c in mission.cargos)
        {
            missiontext.text += "Cargo " + i + ": " + mission.cargoCounters[i-1] + "/" + c + "\n";
            missiontextP2.text += "Cargo " + i + ": " + mission.cargoCounters[i-1] + "/" + c + "\n";
            i++;
        }
    }

    /// <summary>
    /// Sets the Mission-object and then displays it.
    /// </summary>
    /// <param name="mission">the relevant Mission-object</param>
    /// @author Bastian Badde
    public void SetMission(Mission mission)
    {
        this.mission = mission;
        SetMissionField();
    }

    /// <summary>
    /// Displays an error-message in case something went wrong.
    /// </summary>
    /// <param name="caption">the caption of the error message</param>
    /// <param name="message">the error-message to display</param>
    /// @author Bastian Badde
    public void DisplayAlert(string caption, string message)
    {
        errorCaption.text = caption;
        errorText.text = message;
        ClosePanel();
        OpenErrorPanel();
    }
    
    /// <summary>
    /// Opens the error-popUp-Panel
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    public void OpenErrorPanel()
    {
        GameObject panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {

                if (panel.name != "panel08")
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
        }
    }
    
    /// <summary>
    /// Initialises the components with default-Values
    /// </summary>
    /// @author Bastian Badde
    void Start()
    {
        panelisOpen = false;
        deleteOn = false;
        cargoAdditions = new List<int>();
    }

    /// <summary>
    /// Does same as Start(), but can be executed manual for testing reasons.
    /// </summary>
    /// @author Bastian Badde
    public void StartManual()
    {
        panelisOpen = false;
        deleteOn = false;
        cargoAdditions = new List<int>();
    }
}
