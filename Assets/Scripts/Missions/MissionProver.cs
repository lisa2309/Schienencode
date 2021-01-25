using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class MissionProver : MonoBehaviour
{
    public Mission mission;
    public Text missiontext;
    public Text finalText;
    public int stationCounter = 0;

    public int RegisterNewStation()
    {
        return stationCounter++;
    }
    

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

    public void SetFinalText(string text)
    {
        finalText.text = text;
    }

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

    public void SetMission(Mission mission)
    {
        this.mission = mission;
        SetMissionField();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // mission = new Mission(new []{3});
        // Debug.Log("Cargo= " + mission.cargos[0]);
        //missiontext = GameObject.Find("MissionsText").GetComponent<Text>();;
        
        // SetMissionField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
