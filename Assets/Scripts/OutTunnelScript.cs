using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
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
        
        //private static List<int> usedTunnelNumbers = new List<int>();
        
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
            //if (!isLocalPlayer) return;
            if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
            {
                _prover.UpdateOutTunnel(this.OutTunnelNumber);
                OpenPanel();
            }
        }

        /// <summary>
        /// Remove OutTunnelNumber from GivenTunnelNumbers
        /// </summary>
        /// <param name="tunnelNumber">the tunnenNumber to remove</param>
        /// @author Bastian Badde
        public static void RemoveOutTunnel(int tunnelNumber)
        { 
            //if (GivenTunnelNumbers.Remove(tunnelNumber) || usedTunnelNumbers.Remove(tunnelNumber)) DeletedTunnelNumbers.Add(tunnelNumber);
            if (GivenTunnelNumbers.Remove(tunnelNumber)) DeletedTunnelNumbers.Add(tunnelNumber);
        }
        
        // public static void RemoveOutTunnel(List<int> tunnelNumbers)
        // {
        //     //usedTunnelNumbers.RemoveAll(x => tunnelNumbers.Contains(x));
        //     foreach (int i in tunnelNumbers)
        //     {
        //         if (usedTunnelNumbers.Contains(i))
        //         {
        //             usedTunnelNumbers.Remove(i);
        //             GivenTunnelNumbers.Add(i);
        //         }
        //     }
        //     _prover.
        // }

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
        /// Initialises the components and sets the OutTunnelNumber on a free value
        /// </summary>
        /// @author Bastian Badde
        void Start()
        {
            if (!(_prover is null)) _prover = FindObjectOfType<MissionProver>();
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
            //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
            //popUpPanel = GameObject.Find("PopUpPanel");
        }
    }
}