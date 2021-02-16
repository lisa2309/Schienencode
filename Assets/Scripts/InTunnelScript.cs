using System.Collections.Generic;
using UnityEngine;

    public class InTunnelScript : MonoBehaviour
    {
        
        /// <summary>
        /// 
        /// </summary>
        private MissionProver _prover;

        public int RelatedOutTunnelNumber = -1;

        /// <summary>
        /// 
        /// </summary>
        private GameObject panels;
        
        void OnMouseDown()
        {
            if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
            {
                _prover.UpdateInTunnel(this);
                OpenPanel();
            }
        }


        
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
        /// 
        /// @author
        /// </summary>
        void Start()
        {
            _prover = FindObjectOfType<MissionProver>();
        }
    }
