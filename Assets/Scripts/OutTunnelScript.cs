using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a OutTunnel-prefab to be able to connect an OutTunnel with an InTunnel
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde
public class OutTunnelScript : MonoBehaviour
{
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    public MissionProver prover;
    
    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// ID of the OutTunnel
    /// </summary>
    /// @author Bastian Badde
    public int OutTunnelNumber { private set; get; }

    /// <summary>
    /// Bool which is true if InitOutTunnel() is already invoked (default value is false).
    /// </summary>
    /// @author Bastian Badde
    public bool IsInited { private set; get; }

    /// <summary>
    /// Opens the relevant PopUp-Panel, when the OutTunnel is clicked by mouse
    /// </summary>
    /// @author Bastian Badde
    void OnMouseDown()
    {
        Debug.Log("is clicked ");
        if (!MissionProver.deleteOn && !MissionProver.panelIsOpen)
        {
            prover.UpdateOutTunnel(this.OutTunnelNumber);
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
                if (panel.name != "panel06")
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
            prover.UpdateStationSettings();
        }
    }

    /// <summary>
    /// Add OutTunnelNumber to givenTunnelNumbers
    /// </summary>
    /// @author Bastian Badde
    public void InitOutTunnel()
    {
        if (prover is null)prover = FindObjectOfType<MissionProver>();
        if (prover.deletedTunnelNumbers.Count > 0)
        {
            this.OutTunnelNumber = prover.deletedTunnelNumbers.First();
            prover.deletedTunnelNumbers.Remove(prover.deletedTunnelNumbers.First());
            prover.givenTunnelNumbers.Add(this.OutTunnelNumber);
        }
        else
        {
            this.OutTunnelNumber = prover.outTunnelCounter++;
            prover.givenTunnelNumbers.Add(this.OutTunnelNumber);
        }
        prover.givenTunnelNumbers.Sort();
        IsInited = true;
    }
        
}
    
  