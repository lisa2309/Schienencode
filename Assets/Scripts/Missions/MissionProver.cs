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
    public int stationCounter = 0;

    private int tempValueStation;

    private int tempValueSwitch;

    private int tempCompareValueSwitch;
    
    public int currentStation;
    
    private List<int> carogoCounters;

    public Dropdown ddStation;
    
    public Dropdown ddSwitchValue;
    
    public Dropdown ddSwitchCompare;

    public InputField inputSwitchCount;

    private bool ifCondition;
    
    
    /// <summary>
    /// 
    /// @author 
    /// </summary>
    /// <returns></returns>
    public int RegisterNewStation()
    {
        carogoCounters.Add(1);
        return stationCounter++;
        //carogoCounters = new int[stationCounter];
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
        mission.cargoCounters[stationNumber]+= carogoCounters[stationNumber];
        SetMissionField();
    }
    
    
    public void AcceptButtonClicked()
    {
        Debug.Log("Tempvalue Found: " + tempValueStation);
        //Debug.Log("CCL: " + carogoCounters.Count + " CurrSt: " + currentStation);
        carogoCounters[currentStation] = tempValueStation;
        tempValueStation = 1;
    }
    
    public void SwitchAcceptButtonClicked()
    {
        try
        {
            switch (tempCompareValueSwitch)
            {
                case 1:
                    //obj = station;
                    break;
                default:
                    //obj = null;
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Debug.Log("Tempvalue Found: " + tempValueStation);
        //Debug.Log("CCL: " + carogoCounters.Count + " CurrSt: " + currentStation);
        carogoCounters[currentStation] = tempValueStation;
        tempValueStation = 1;
    }

    public void ClosePanel()
    {
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
    
    public void HandleDropdownValue(int val)
    {
        Debug.Log("Dropvalue Found: " + (ddStation.value+1));
        tempValueStation = ddStation.value + 1;
    }
    
    public void HandleDropdownValueSwitch(int val)
    {
        Debug.Log("Dropvalue Found on switch: " + (ddSwitchValue.value+1));
        tempValueSwitch = ddStation.value + 1;
    }
    
    public void HandleDropdownCompareSwitch(int val)
    {
        Debug.Log("Comparevalue Found on switch: " + (ddSwitchCompare.value+1));
        tempCompareValueSwitch = ddStation.value + 1;
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
        ifCondition = false;
        carogoCounters = new List<int>();
        tempValueStation = tempValueSwitch = tempCompareValueSwitch = 1;
        // mission = new Mission(new []{3});
        // Debug.Log("Cargo= " + mission.cargos[0]);
        //missiontext = GameObject.Find("MissionsText").GetComponent<Text>();;

        // SetMissionField();
    }

}
