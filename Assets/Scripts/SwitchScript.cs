using System;
using UnityEngine;


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

    public SwitchMode mode;

    /// <summary>
    /// Array of the relevant dropdown-values from the switchPopUps
    /// ComparationValues[0] relates to the stationnumber of the relevant StationScript (to get the cargo of the station).
    /// ComparationValues[1] is a number for the comparation-sign: '0' means '>'; '1' means '<'; '2' means '=='.
    /// ComparationValues[2] is the value to compare with (only value which is also set in the 'For-Switch').
    /// </summary>
    public int[] ComparationValues;
    
    public enum SwitchMode
    {
        Unchosen,
        If,
        For,
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

    public void ResetSwitch()
    {
        this.mode = SwitchMode.Unchosen;
        this.ComparationValues = new int[] {0,0,1};
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

    public void ChangeSwitchMode(int switchVal)
    {
        switch (switchVal)
        {
            case 0:
                this.mode = SwitchMode.If;
                break;
            case 1:
                this.mode = SwitchMode.While;
                break;
            case 2:
                this.mode = SwitchMode.For;
                break;
            default:
                this.mode = SwitchMode.Unchosen;
                break;
                
        }
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
        ComparationValues = new int[] {0,0,1};
        mode = SwitchMode.Unchosen;
        _prover = FindObjectOfType<MissionProver>();
        //this._stationNumber = _prover.RegisterNewStation();
        //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
        //popUpPanel = GameObject.Find("PopUpPanel");
    }
    }
