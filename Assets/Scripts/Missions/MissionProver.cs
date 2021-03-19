using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    /// Bool which is true when Prefabs should be deletable
    /// </summary>
    public static bool deleteOn;

    /// <summary>
    /// Bool which is true when a PopUpPanel is open
    /// </summary>
    public static bool panelIsOpen;

    /// <summary>
    /// Bool which is true when objects are instantiated by the database
    /// </summary>
    public static bool buildOnDB;
    
    /// <summary>
    /// Mission-object of the loaded mission
    /// </summary>
    public Mission mission;

    /// <summary>
    /// UI-Text-object which is displayed in the missionfield of the inventory for Player 1
    /// </summary>
    public Text missionText;
    
    /// <summary>
    /// UI-Text-object which is displayed in the missionfield of the inventory for Player 2
    /// </summary>
    public Text missionTextP2;

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
    /// Counter of registered Switches
    /// </summary>
    private int switchCounter = 0;
    
    /// <summary>
    /// Counter of registered InTunnels
    /// </summary>
    private int inTunnelCounter = 0;
    
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
    public List<int> cargoAdditions;

    /// <summary>
    /// UI-Dropdown-object of a TrainStation for choosing the cargoAdditionNumber
    /// </summary>
    public Dropdown station;
    
    /// <summary>
    /// UI-Dropdown-object of an If-Switch for choosing the related TrainStation-Cargo
    /// </summary>
    public Dropdown switchValue;
    
    /// <summary>
    /// UI-Dropdown-object of an If-Switch for choosing a ComparationValue
    /// </summary>
    public Dropdown switchCompare;
    
    /// <summary>
    /// UI-Dropdown-object of a While-Switch for choosing the related TrainStation-Cargo
    /// </summary>
    public Dropdown whileSwitchValue;
    
    /// <summary>
    /// UI-Dropdown-object of a While-Switch for choosing a ComparationValue
    /// </summary>
    public Dropdown whileSwitchCompare;
    
    /// <summary>
    /// UI-Dropdown-object of a Unchosen-Switch for choosing the SwitchMode
    /// </summary>
    public Dropdown generalSwitch;
    
    /// <summary>
    /// UI-Dropdown-object of an InTunnel for choosing the related OutTunnel (tunnelNumber)
    /// </summary>
    public Dropdown inTunnelOpenOuts;
    
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
    /// List of registered StationScripts 
    /// </summary>
    private List<StationScript> stationBodies;
    
    /// <summary>
    /// List of registered StationScripts 
    /// </summary>
    private List<SwitchScript> switchBodies;
    
    /// <summary>
    /// List of registered StationScripts 
    /// </summary>
    private List<InTunnelScript> inTunnelBodies;
    
    /// <summary>
    /// Player Object of active player 
    /// </summary>
    public Player player;
    
    /// <summary>
    /// Counter to generate new OutTunnelNumbers if needed
    /// </summary>
    public int outTunnelCounter;
        
    /// <summary>
    /// List of previous TunnelNumbers which are deleted and open to use
    /// </summary>
    public List<int> deletedTunnelNumbers;

    /// <summary>
    /// List of current given TunnelNumbers
    /// </summary>
    public List<int> givenTunnelNumbers;

    
    /// <summary>
    /// Call Server to remove OutTunnelNumber synchronized
    /// </summary>
    /// <param name="tunnelNumber">the tunnenlNumber to remove</param>
    /// @author Bastian Badde
    public void RemoveOutTunnel(int tunnelNumber)
    { 
        player.OutTunnelChanged(tunnelNumber);
    }

    /// <summary>
    /// Remove OutTunnelNumber from givenTunnelNumbers
    /// </summary>
    /// <param name="tunnelNumber">The tunnenlNumber to remove</param>
    /// @author Bastian Badde
    public void SetRemovedOutTunnel(int tunnelNumber)
    {
        if (givenTunnelNumbers.Remove(tunnelNumber)) deletedTunnelNumbers.Add(tunnelNumber);
    }

    /// <summary>
    /// Creates a new switchNumber an registers a new Switch
    /// </summary>
    /// <param name="switchO">Object of the SwitchScript</param>
    /// <returns>The stationNumber of the new registered TrainStation</returns>
    /// @author Bastian Badde
    public int RegisterNewSwitch(SwitchScript switchO)
    {
        switchBodies.Add(switchO);
        return switchCounter++;
    }
    
    /// <summary>
    /// Creates a new inTunnelNumber and registers a new InTunnel
    /// </summary>
    /// <param name="inTunnel">Object of the InTunnelScript</param>
    /// <returns>The stationNumber of the new registered TrainStation</returns>
    /// @author Bastian Badde
    public int RegisterNewInTunnel(InTunnelScript inTunnel)
    {
        inTunnelBodies.Add(inTunnel);
        return inTunnelCounter++;
    }
    
    /// <summary>
    /// Creates a new stationNumber and registers a new TrainStation
    /// </summary>
    /// <param name="station">Object of the StationScript</param>
    /// <returns>The stationNumber of the new registered TrainStation</returns>
    /// @author Bastian Badde
    public int RegisterNewStation(StationScript station)
    {
        cargoAdditions.Add(1);
        stationBodies.Add(station);
        return stationCounter++;
    }

    /// <summary>
    /// Sets the caption of the OutTunnel-PopUp-Panel
    /// </summary>
    /// <param name="outTunnelNumber">Number of the out tunnel</param>
    /// @author Bastian Badde
    public void UpdateOutTunnel(int outTunnelNumber)
    {
        outTunnelCaption.text = "Tunnel-Ausgang: T" + outTunnelNumber;
    }
    
    /// <summary>
    /// Sets the settings used for appropriate displaying the PopUp of the current selected InTunnel
    /// openTunnelStrings: List of all tunnels
    /// currentValue:
    /// tempValue:
    /// </summary>
    /// <param name="inTunnel">The InTunnelScript-object of the currently selected InTunnel</param>
    /// @author Bastian Badde
    public void UpdateInTunnel(InTunnelScript inTunnel)
    {
        this.currentInTunnel = inTunnel;
        List<string> openTunnelStrings = new List<string>();
        int currentValue = 0;
        int tempValue = 0;
        foreach (int i in givenTunnelNumbers)
        {
            openTunnelStrings.Add("T" + i);
            if (i == inTunnel.relatedOutTunnelNumber) currentValue = tempValue;
            tempValue++;
        }
        inTunnelOpenOuts.options.Clear();
        inTunnelOpenOuts.AddOptions(openTunnelStrings);
        inTunnelOpenOuts.value = currentValue;
    }
    
    /// <summary>
    /// Raises the cargoValue of the relevant TrainStation by the relevant cargoAddition and updates the MissionField
    /// in the inventory
    /// </summary>
    /// <param name="stationNumber">The stationNumber of the relevant TrainStation</param> 
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
    /// <param name="switchScript">The SwitchScript-object of the currently selected Switch</param>
    /// @author Bastian Badde
    public void UpdateSwitch(SwitchScript switchScript)
    {
        this.currentSwitch = switchScript;
        whileSwitchCompare.value = switchCompare.value = switchScript.comparationValues[1];
        whileSwitchValue.value = switchValue.value = switchScript.comparationValues[0];
        inputForSwitch.text = inputIfSwitch.text = inputWhileSwitch.text = switchScript.comparationValues[2].ToString();
    }

    /// <summary>
    /// Sets the settings used for appropriate displaying the PopUp of the current selected TrainStation
    /// </summary>
    /// <param name="stationnumber">The stationNumber of the currently selected TrainStation</param>
    /// <param name="station">The StationScript-object of the currently selected TrainStation</param>
    /// @author Bastian Badde
    public void UpdateStation(int stationNumber, StationScript station)
    {
        this.currentStation = stationNumber;
        this.currentStationBody = station;
        this.stationCaption.text = "Bahnhof C" + (stationNumber + 1);
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
        station.value = cargoAdditions[currentStation] - 1;
    }
    
    /// <summary>
    /// Enters the settings of the currently selected TrainStation-PopUp.
    /// Called by the AcceptButton of the TrainStation-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void AcceptButtonClicked()
    {
        player.CargoChanged(currentStation, station.value + 1);
        ClosePanel();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stationNr"></param>
    /// <param name="value"></param>
    public void SetStationCargo(int stationNr, int value)
    {
        cargoAdditions[stationNr] = value;
        stationBodies.Find(s => s.stationNumber.Equals(stationNr)).cargoAdditionNumber = value;
    }
   
    /// <summary>
    /// Enters the settings of the currently selected IfSwitch-PopUp.
    /// Called by the AcceptButton of the IfSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void IfSwitchAcceptButtonClicked()
    {
        player.SwitchValuesChanged(
            currentSwitch.switchNumber, switchValue.value, 
            switchCompare.value, Int32.Parse(inputIfSwitch.text));
        ClosePanel();
    }

    
    /// <summary>
    /// Retrieves synchronized data from player to set the switch-values
    /// </summary>
    /// <param name="switchNumber">The switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">The stationNumber whose cargo to compare with</param>
    /// <param name="compare">The decoded compare value</param>
    /// <param name="value">The value to compare with</param>
    /// @author Bastian Badde
    public void SetSwitchValues(int switchNumber, int cargo, int compare, int value)
    {
        switchBodies.Find(s => s.switchNumber.Equals(switchNumber)).comparationValues = new[]
        {
            cargo,compare,value
        };
    }
    
    /// <summary>
    /// Enters the settings of the currently selected WhileSwitch-PopUp.
    /// Called by the AcceptButton of the WhileSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void WhileSwitchAcceptButtonClicked()
    {
        player.SwitchValuesChanged(
            currentSwitch.switchNumber, whileSwitchValue.value, 
            whileSwitchCompare.value, Int32.Parse(inputWhileSwitch.text));
        ClosePanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected ForSwitch-PopUp.
    /// Called by the AcceptButton of the ForSwitch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void ForSwitchAcceptButtonClicked()
    {
        player.SwitchValuesChanged(currentSwitch.switchNumber, 0, 0, Int32.Parse(inputForSwitch.text));
        ClosePanel();
    }
    
    /// <summary>
    /// Enters the settings of the currently selected general Switch-PopUp.
    /// Called by the AcceptButton of the general Switch-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void GeneralSwitchAcceptButtonClicked()
    {
        player.SwitchModeChanged(currentSwitch.switchNumber, generalSwitch.value);
    }
    
    /// <summary>
    /// Retrieves synchronized data from player to set the switch-mode
    /// </summary>
    /// <param name="switchNumber">The switchNumber of the relevant SwicthScript</param>
    /// <param name="mode">The decoded mode-value to set with</param>
    /// @author Bastian Badde
    public void SetSwitchMode(int switchNumber, int mode)
    {
        switchBodies.Find(s => s.switchNumber.Equals(switchNumber)).ChangeSwitchMode(mode);
        if (!(currentSwitch is null) && currentSwitch.switchNumber == switchNumber)
        {
            ClosePanel();
            currentSwitch.OpenPanel();
        }
    }
    
    /// <summary>
    /// Enters the settings of the currently selected InTunnel-PopUp.
    /// Called by the AcceptButton of the InTunnel-PopUp.
    /// </summary>
    /// @author Bastian Badde
    public void InTunnelAcceptedButtonClicked()
    {
        player.InTunnelChanged(currentInTunnel.inTunnelNumber, givenTunnelNumbers[inTunnelOpenOuts.value]);
        ClosePanel();
    }

    /// <summary>
    /// Retrieves synchronized data from player to set the in-Tunnel-values
    /// </summary>
    /// <param name="inTunnelNumber">The inTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">The outTunnelNumber to set the related outTunnelNumber</param>
    /// @author Bastian Badde
    public void SetInTunnelValues(int inTunnelNumber, int outTunnelNumber)
    {
        inTunnelBodies.Find(s => s.inTunnelNumber.Equals(inTunnelNumber)).relatedOutTunnelNumber = outTunnelNumber;
    }
    
    /// <summary>
    /// Closes the current open PopUp-panel.
    /// panels: Collection of the different PopUp-Panels
    /// </summary>
    /// @author Bastian Badde
    public void ClosePanel()
    {
        MissionProver.panelIsOpen = false;
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
            DeleteButton.GetComponent<Image>().sprite = DeleteImageWhite;
        }
        else
        {
            DeleteButton.GetComponent<Image>().sprite = DeleteImageBlack;
        }
    }
    
    /// <summary>
    /// Sets the Text which is displayed after the train enters the RailEnd-object.
    /// </summary>
    /// <param name="text">Text to be displayed</param>
    /// @author Bastian Badde
    public void SetFinalText(string text)
    {
        finalText.text = text;
    }

    /// <summary>
    /// Builds the text to be displayed in the MissionField of the inventory and displays it.
    /// i: Index for the foreach loop
    /// </summary>
    /// @author Bastian Badde
    private void SetMissionField()
    {
        missionText.text = "Mission:\n\n";
        int i = 1;
        foreach (int c in mission.cargos)
        {
            missionText.text += "Bahnhof " + i + ": " + mission.cargoCounters[i-1] + "/" + c + "\n";
            missionTextP2.text += "Bahnhof " + i + ": " + mission.cargoCounters[i-1] + "/" + c + "\n";
            i++;
        }
    }

    /// <summary>
    /// Sets the Mission-object and then displays it.
    /// </summary>
    /// <param name="mission">The relevant Mission-object</param>
    /// @author Bastian Badde
    public void SetMission(Mission mission)
    {
        this.mission = mission;
        SetMissionField();
    }

    /// <summary>
    /// Displays an error-message in case something went wrong.
    /// </summary>
    /// <param name="caption">The caption of the error message</param>
    /// <param name="message">The error-message to display</param>
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
    /// panels: Collection of the different PopUp-Panels
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
                        MissionProver.panelIsOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Initialises the components with default values
    /// </summary>
    /// @author Bastian Badde
    void Start()
    {
        panelIsOpen = false;
        deleteOn = false;
        cargoAdditions = new List<int>();
        stationBodies = new List<StationScript>();
        inTunnelBodies = new List<InTunnelScript>();
        switchBodies = new List<SwitchScript>();
        deletedTunnelNumbers = new List<int>();
        givenTunnelNumbers = new List<int>();
        outTunnelCounter = 0;
    }

    /// <summary>
    /// Does the same as Start(), but can be executed manual for testing reasons.
    /// </summary>
    /// @author Bastian Badde
    public void StartManual()
    {
       Start();
    }
}
