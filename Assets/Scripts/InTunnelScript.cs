using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a InTunnel-prefab to be able to connect an OutTunnel with an InTunnel
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde
public class InTunnelScript : MonoBehaviour
{
    
    /// <summary>
    /// ID of the InTunnel
    /// </summary>
    public int inTunnelNumber;
    
    /// <summary>
    /// Bool which is true if InitOutTunnel() is already invoked (default value is false).
    /// </summary>
    public bool IsInited { private set; get; }
        
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private MissionProver missionProver;

    /// <summary>
    /// The OutTunnelNumber of the related OutTunnel.
    /// -1 by default.
    /// </summary>
    public int relatedOutTunnelNumber;

    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// Bool which is true, when it is build on the db (default value is false).
    /// </summary>
    public bool buildOnDatabase;
        
    /// <summary>
    /// Opens the relevant PopUp-Panel, when the InTunnel is clicked by mouse
    /// </summary>
    /// @author Bastian Badde
    void OnMouseDown()
    {
        Debug.Log("InTunnel could open");
        Debug.Log("OpenPanel: " + MissionProver.panelIsOpen);
        Debug.Log("OnDB: " + buildOnDatabase);
        if (!MissionProver.deleteOn && !MissionProver.panelIsOpen && !buildOnDatabase)
        {
            Debug.Log("InTunnel should open");
            missionProver.UpdateInTunnel(this);
            OpenPanel();
        }
    }
        
    /// <summary>
    /// Opens the relevant popUp-Panel
    /// panels: Collection of the different PopUp-Panels
    /// </summary>
    /// @author Ahmed L'harrak & Bastian Badde
    public void OpenPanel()
    {
        panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {
                if (panel.name != "panel07")
                {
                    panel.gameObject.SetActive(false);
                }
                else
                {
                    if (!panel.gameObject.activeSelf)
                    {
                        Debug.Log("Open InTunnelPanel");
                        MissionProver.panelIsOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
            missionProver.UpdateStationSettings();
        }
    }

    /// <summary>
    /// Initialises the components with default-Values and register switch to the missionprover
    /// </summary>
    /// @author Bastian Badde
    public void Register()
    {
        relatedOutTunnelNumber = 0;
        missionProver = FindObjectOfType<MissionProver>();
        this.inTunnelNumber = missionProver.RegisterNewInTunnel(this);
        IsInited = true;
    }
}
