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
        /// MissionProver object of the scene for organisation
        /// </summary>
        private MissionProver _prover;

        /// <summary>
        /// The OutTunnelNumber of the related OutTunnel.
        /// -1 by default.
        /// </summary>
        public int relatedOutTunnelNumber = -1;

        /// <summary>
        /// Collection of the different PopUp-Panels
        /// </summary>
        private GameObject panels;
        
        /// <summary>
        /// Opens the relevant PopUp-Panel, when the InTunnel is clicked by mouse
        /// </summary>
        /// @author Bastian Badde
        void OnMouseDown()
        {
            if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
            {
                _prover.UpdateInTunnel(this);
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
