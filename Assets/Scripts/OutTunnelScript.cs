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
        /// static counter to generate new OutTunnelNumbers if needed
        /// </summary>
        private static int _outTunnelCounter = 0;
        
        /// <summary>
        /// List of previous TunnelNumbers which are deleted and open to use
        /// </summary>
        private static readonly List<int> DeletedTunnelNumbers = new List<int>();

        /// <summary>
        /// List of current given TunnelNumbers
        /// </summary>
        public static readonly List<int> GivenTunnelNumbers = new List<int>();
        
        /// <summary>
        /// ID of the OutTunnel
        /// </summary>
        public int OutTunnelNumber { private set; get; }

        /// <summary>
        /// MissionProver object of the scene for organisation
        /// </summary>
        private static MissionProver _prover;
    
        /// <summary>
        /// Collection of the different PopUp-Panels
        /// </summary>
        private GameObject panels;

        /// <summary>
        /// Opens the relevant PopUp-Panel, when the OutTunnel is clicked by mouse
        /// </summary>
        /// @author Bastian Badde
        void OnMouseDown()
        {
            Debug.Log("is clicked ");
            if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
            {
                _prover.UpdateOutTunnel(this.OutTunnelNumber);
                OpenPanel();
            }
        }

        /// <summary>
        /// Remove OutTunnelNumber from GivenTunnelNumbers
        /// </summary>
        /// <param name="tunnelNumber">the tunnenlNumber to remove</param>
        /// @author Bastian Badde
        public static void RemoveOutTunnel(int tunnelNumber)
        { 
            if (GivenTunnelNumbers.Remove(tunnelNumber)) DeletedTunnelNumbers.Add(tunnelNumber);
        }
        
        /// <summary>
        /// Add OutTunnelNumber to GivenTunnelNumbers
        /// </summary>
        /// @author Bastian Badde
        public void AddOutTunnel()
        { 
            if (DeletedTunnelNumbers.Count > 0)
            {
                this.OutTunnelNumber = DeletedTunnelNumbers.First();
                DeletedTunnelNumbers.Remove(DeletedTunnelNumbers.First());
                GivenTunnelNumbers.Add(this.OutTunnelNumber);
            }
            else
            {
                this.OutTunnelNumber = _outTunnelCounter++;
                GivenTunnelNumbers.Add(this.OutTunnelNumber);
            }
            GivenTunnelNumbers.Sort();
            Debug.Log("OTN: " + this. OutTunnelNumber);
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
        /// Initialises the MissionProver-object
        /// </summary>
        /// @author Bastian Badde
        void Start()
        {
            _prover = FindObjectOfType<MissionProver>();
        }
        
        
    }
    
  