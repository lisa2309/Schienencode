using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// @author
/// </summary>
public class MissionProver : MonoBehaviour
{
    public static bool deleteOn;

    public static bool panelisOpen;

    public static bool buildOnDB;
    
    /// <summary>
    /// 
    /// </summary>
    public Mission mission;

    /// <summary>
    /// 
    /// </summary>
    public Text missiontext;

    /// <summary>
    /// 
    /// </summary>
    public Text finalText;

    /// <summary>
    /// 
    /// </summary>
    private int stationCounter = 0;
    
    private int tunnelCounter = 0;

    private int currentStation;
    
    private List<int> cargoAdditions;

    public Dropdown ddStation;
    
    public Dropdown ddSwitchValue;
    
    public Dropdown ddSwitchCompare;
    
    public Dropdown ddWhileSwitchValue;
    
    public Dropdown ddWhileSwitchCompare;
    
    public Dropdown ddGeneralSwitch;
    
    public Dropdown ddInTunnelOpenOuts;
    
    //public Dropdown ddOutTunnelToDelete;

    public InputField inputIfSwitch;
    
    public InputField inputWhileSwitch;

    public InputField inputForSwitch;

    public Text stationCaption;
    
    public Text outTunnelCaption;

    private SwitchScript currentSwitch;

    private StationScript currentStationBody;

    private InTunnelScript currentInTunnel;

    public Sprite DeleteImageWhite;

    public Sprite DeleteImageBlack;

    public Button DeleteButton;
    
    
    /// <summary>
    /// 
    /// @author 
    /// </summary>
    /// <returns></returns>
    public int RegisterNewStation()
    {
        cargoAdditions.Add(1);
        return stationCounter++;
        //carogoCounters = new int[stationCounter];
    }

    /// <summary>
    /// 
    /// @author 
    /// </summary>
    /// <returns></returns>
    public void UpdateOutTunnel(int outTunnelNumber)
    {
        outTunnelCaption.text = "Tunnel-Ausgang: T" + outTunnelNumber;
    }
    
    /// <summary>
    /// 
    /// @author 
    /// </summary>
    /// <returns></returns>
    public void UpdateInTunnel(InTunnelScript inTunnel)
    {
        this.currentInTunnel = inTunnel;
        List<string> openTunnelStrings = new List<string>();
        int currentValue = 0;
        int tempValue = 0;
        foreach (int i in OutTunnelScript.OpenTunnelNumbers)
        {
            openTunnelStrings.Add("T" + i);
            if (i == inTunnel.RelatedOutTunnelNumber) currentValue = tempValue;
            tempValue++;
        }
        ddInTunnelOpenOuts.options.Clear();
        ddInTunnelOpenOuts.AddOptions(openTunnelStrings);
        ddInTunnelOpenOuts.value = currentValue;
    }
    
    /// <summary>
    /// 
    /// @author
    /// </summary>
    /// <param name="stationNumber"></param>
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
    
    public void UpdateSwitch(SwitchScript switchScript)
    {
        this.currentSwitch = switchScript;
        ddWhileSwitchCompare.value = ddSwitchCompare.value = switchScript.ComparationValues[1];
        ddWhileSwitchValue.value = ddSwitchValue.value = switchScript.ComparationValues[0];
        inputForSwitch.text = inputIfSwitch.text = inputWhileSwitch.text = switchScript.ComparationValues[2].ToString();
    }

    public void UpdateStation(int stationnumber, StationScript station)
    {
        this.currentStation = stationnumber;
        this.currentStationBody = station;
        this.stationCaption.text = "Bahnhof C" + (stationnumber + 1);
    }

    public void ResetSwitchClicked()
    {
        currentSwitch.ResetSwitch();
        ClosePanel();
        currentSwitch.OpenPanel();
    }
    
    public void UpdateStationSettings()
    {
        ddStation.value = cargoAdditions[currentStation] - 1;
    }
    
    public void AcceptButtonClicked()
    {
        //Debug.Log("CCL: " + carogoCounters.Count + " CurrSt: " + currentStation);
        currentStationBody.cargoAdditionNumber = cargoAdditions[currentStation] = ddStation.value + 1;
        ClosePanel();
    }
   
    public void IfSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues = new []
            {ddSwitchValue.value, ddSwitchCompare.value, Int32.Parse(inputIfSwitch.text)};
        // Debug.Log("current cargo: " + mission.cargoCounters[ddSwitchValue.value]);
        // Debug.Log("current compareVal: " + (ddSwitchCompare.value));
        // Debug.Log("current inputVal: " + Int32.Parse(inputIfSwitch.text));
        ClosePanel();
    }
    
    public void WhileSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues = new []
            {ddWhileSwitchValue.value, ddWhileSwitchCompare.value, Int32.Parse(inputWhileSwitch.text)};
        // Debug.Log("current cargo: " + mission.cargoCounters[ddSwitchValue.value]);
        // Debug.Log("current compareVal: " + (ddSwitchCompare.value));
        // Debug.Log("current inputVal: " + Int32.Parse(inputIfSwitch.text));
        ClosePanel();
    }
    
    public void ForSwitchAcceptButtonClicked()
    {
        currentSwitch.ComparationValues[2] = Int32.Parse(inputForSwitch.text);
        ClosePanel();
    }
    
    public void GeneralSwitchAcceptButtonClicked()
    {
        currentSwitch.ChangeSwitchMode(ddGeneralSwitch.value);
        ClosePanel();
        currentSwitch.OpenPanel();
    }
    
    public void InTunnelAcceptedButtonClicked()
    {
        currentInTunnel.RelatedOutTunnelNumber = OutTunnelScript.OpenTunnelNumbers[ddInTunnelOpenOuts.value];
        ClosePanel();
    }
    

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
    /// 
    /// @author
    /// </summary>
    /// <param name="text"></param>
    public void SetFinalText(string text)
    {
        finalText.text = text;
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>
    private void SetMissionField()
    {
        missiontext.text = "Mission:\n\n";
        int i = 1;
        foreach (int c in mission.cargos)
        {
            missiontext.text += "Cargo " + i + ": " + mission.cargoCounters[i-1] + "/" + c + "\n";
            i++;
        }
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>
    /// <param name="mission"></param>
    public void SetMission(Mission mission)
    {
        this.mission = mission;
        SetMissionField();
    }
    
    /// <summary>
    /// 
    /// @author
    /// </summary>
    void Start()
    {
        panelisOpen = false;
        deleteOn = false;
        cargoAdditions = new List<int>();
        // mission = new Mission(new []{3});
        // Debug.Log("Cargo= " + mission.cargos[0]);
        //missiontext = GameObject.Find("MissionsText").GetComponent<Text>();;

        // SetMissionField();
    }

}
