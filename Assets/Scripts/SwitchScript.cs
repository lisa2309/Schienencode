using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SwitchScript : MonoBehaviour
    {
        /// <summary>
    /// 
    /// </summary>
    private int _switchNumber;

    /// <summary>
    /// 
    /// </summary>
    private MissionProver _prover;
    
    /// <summary>
    /// 
    /// </summary>
    private GameObject panels;

    private SwitchMode mode;

    /// <summary>
    /// Array of the relevant dropdown-values from the switchPopUps
    /// ComparationValues[0] relates to the stationnumber of the relevant StationScript (to get the cargo of the station).
    /// ComparationValues[1] is a number for the comparation-sign: '1' means '>'; '2' means '<'; '3' means '=='.
    /// ComparationValues[2] is the value to compare with (only value which is also set in the 'For-Switch').
    /// </summary>
    public int[] ComparationValues;
    
    private enum SwitchMode
    {
        Unchosen,
        If,
        While
    }

    /// <summary>
    /// 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        Debug.Log("Switch is clicked ");
        //_prover.currentStation = this._stationNumber;
        //if (!isLocalPlayer) return;
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
        {
            _prover.UpdateSwitch(this);
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        switch (mode)
        {
            case SwitchMode.Unchosen:
                OpenSpecificPanel("panel04");
                break;
            case SwitchMode.If:
                OpenSpecificPanel("panel02");
                break;
            case SwitchMode.While:
                OpenSpecificPanel("panel03");
                break;
        }
    }

    public void ChangeSwitchMode(bool isIf)
    {
        if (isIf) this.mode = SwitchMode.If;
        else this.mode = SwitchMode.While;
    }

    
    private void OpenSpecificPanel(string panelString)
    {
        panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {
                if (panel.name != panelString)
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
    /// 
    /// @author
    /// </summary>
    void Start()
    {
        ComparationValues = new int[] {1,1,1};
        mode = SwitchMode.Unchosen;
        _prover = FindObjectOfType<MissionProver>();
        //this._stationNumber = _prover.RegisterNewStation();
        //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
        //popUpPanel = GameObject.Find("PopUpPanel");
    }
    }
}