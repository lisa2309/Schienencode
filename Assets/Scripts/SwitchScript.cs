using System;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a switch-prefab which have influence to the route of the train, so the player can define
/// which path the train should chose
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde
public class SwitchScript : MonoBehaviour
{
    /// <summary>
    /// ID of the switch
    /// </summary>
    private int _switchNumber;

    /// <summary>
    /// Missionprover object of the scene for organisation
    /// </summary>
    private MissionProver _prover;
    
    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// Enum-Element to differentiate the kind of switch 
    /// </summary>
    public SwitchMode mode;

    /// <summary>
    /// Array of the relevant dropdown-values from the switchPopUps
    /// ComparationValues[0] relates to the stationnumber of the relevant StationScript (to get the cargo of the station).
    /// ComparationValues[1] is a number for the comparation-sign: '0' means '>'; '1' means '<'; '2' means '=='.
    /// ComparationValues[2] is the value to compare with (only value which is also set in the 'For-Switch').
    /// </summary>
    public int[] ComparationValues;
    
    /// <summary>
    /// Opens the relevant PopUp-Panel, when the switch is clicked by mouse
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    void OnMouseDown()
    {
        Debug.Log("Switch is clicked ");
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
        {
            Debug.Log("Switch should open Panel");
            _prover.UpdateSwitch(this);
            OpenPanel();
        }
    }

    /// <summary>
    /// resets the settings of the switch
    /// </summary>
    /// @author Bastian Badde
    public void ResetSwitch()
    {
        this.mode = SwitchMode.Unchosen;
        this.ComparationValues = new int[] {0,0,1};
    }

    /// <summary>
    /// Opens the relevant popUp-Panel depending on the current SwitchMode
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
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
            case SwitchMode.For:
                OpenSpecificPanel("panel05");
                break;
        }
    }

    /// <summary>
    /// Changes the current SwitchMode
    /// </summary>
    /// <param name="switchVal">the dropdown Value of the appropriate SwitchMode</param>
    /// @author Bastian Badde
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

    /// <summary>
    /// Opens the relevant popUp-Panel depending on the current SwitchMode
    /// </summary>
    /// <param name="panelString">the name of the panel what to open</param>
    /// @author Ahmed L'harrak & Bastian Badde
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
    /// Initialises the components with default-Values
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    void Start()
    {
        ComparationValues = new int[] {0,0,1};
        mode = SwitchMode.Unchosen;
        _prover = FindObjectOfType<MissionProver>();
    }
}
