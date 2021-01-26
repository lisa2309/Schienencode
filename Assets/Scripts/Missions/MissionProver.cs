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

    /// <summary>
    /// 
    /// @author 
    /// </summary>
    /// <returns></returns>
    public int RegisterNewStation()
    {
        return stationCounter++;
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
        mission.cargoCounters[stationNumber]++;
        SetMissionField();
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
        // mission = new Mission(new []{3});
        // Debug.Log("Cargo= " + mission.cargos[0]);
        //missiontext = GameObject.Find("MissionsText").GetComponent<Text>();;
        
        // SetMissionField();
    }

}
