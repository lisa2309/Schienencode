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
    /// bool which is true if InitOutTunnel() is already invoked (default value is false).
    /// </summary>
    public bool IsInited { private set; get; }
        
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private MissionProver prover;

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
    /// bool which is true, when it is build on the db (default value is false).
    /// </summary>
    public bool buildOnDB;
        
    /// <summary>
    /// Opens the relevant PopUp-Panel, when the InTunnel is clicked by mouse
    /// </summary>
    /// @author Bastian Badde
    void OnMouseDown()
    {
        Debug.Log("InTunnel could open");
        Debug.Log("OpenPanel: " + MissionProver.panelisOpen);
        Debug.Log("OnDB: " + buildOnDB);
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen && !buildOnDB)
        {
            Debug.Log("InTunnel should open");
            prover.UpdateInTunnel(this);
            OpenPanel();
        }
    }
        
    /// <summary>
    /// Opens the relevant popUp-Panel
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
                        MissionProver.panelisOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
            prover.UpdateStationSettings();
        }
    }

    /// <summary>
    /// Initialises the components with default-Values and register switch to the missionprover
    /// </summary>
    /// @author Bastian Badde
    public void Register()
    {
        relatedOutTunnelNumber = 0;
        prover = FindObjectOfType<MissionProver>();
        this.inTunnelNumber = prover.RegisterNewInTunnel(this);
        IsInited = true;
    }

}
