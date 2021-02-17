using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class OutTunnelScript : MonoBehaviour
    {
        private static int _outTunnelCounter = 0;
        
        //private static List<int> usedTunnelNumbers = new List<int>();
        
        private static readonly List<int> DeletedTunnelNumbers = new List<int>();

        public static readonly List<int> OpenTunnelNumbers = new List<int>();
        
        /// <summary>
        /// 
        /// </summary>
        public int OutTunnelNumber { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        private static MissionProver _prover;
    
        /// <summary>
        /// 
        /// </summary>
        private GameObject panels;

        
        
    /// <summary>
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        Debug.Log("is clicked ");
        //if (!isLocalPlayer) return;
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
        {
            _prover.UpdateOutTunnel(this.OutTunnelNumber);
            OpenPanel();
        }
    }

    public static void RemoveOutTunnel(int tunnelNumber)
    { 
        //if (OpenTunnelNumbers.Remove(tunnelNumber) || usedTunnelNumbers.Remove(tunnelNumber)) DeletedTunnelNumbers.Add(tunnelNumber);
        if (OpenTunnelNumbers.Remove(tunnelNumber)) DeletedTunnelNumbers.Add(tunnelNumber);
    }
    
    // public static void RemoveOutTunnel(List<int> tunnelNumbers)
    // {
    //     //usedTunnelNumbers.RemoveAll(x => tunnelNumbers.Contains(x));
    //     foreach (int i in tunnelNumbers)
    //     {
    //         if (usedTunnelNumbers.Contains(i))
    //         {
    //             usedTunnelNumbers.Remove(i);
    //             OpenTunnelNumbers.Add(i);
    //         }
    //     }
    //     _prover.
    // }

    
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
                        MissionProver.panelisOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
            _prover.UpdateStationSettings();

        }
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>
    void Start()
    {
        if (!(_prover is null)) _prover = FindObjectOfType<MissionProver>();
        if (DeletedTunnelNumbers.Count > 0)
        {
            this.OutTunnelNumber = DeletedTunnelNumbers.First();
            DeletedTunnelNumbers.Remove(DeletedTunnelNumbers.First());
            OpenTunnelNumbers.Add(this.OutTunnelNumber);
        }
        else
        {
            this.OutTunnelNumber = _outTunnelCounter++;
            OpenTunnelNumbers.Add(this.OutTunnelNumber);
        }
        //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
        //popUpPanel = GameObject.Find("PopUpPanel");
    }
    }
}