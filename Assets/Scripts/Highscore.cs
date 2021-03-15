using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class check's if the mission is correct complete and show's a text depend on if the mission correct or not.
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller
public class Highscore : MonoBehaviour
{
    /// <summary>
    /// missionsProver object of the class MissionProver.
    /// </summary>
    private MissionProver missionProver;
    /// <summary>
    /// sceneLoader object of the class SceneLoader.
    /// </summary>
    private SceneLoader sceneLoader;

    /// <summary>
    /// GameObject from all panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// Searchs for the objects of the types MissionProver and SceneLoader.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    void Awake()
    {
        missionProver = FindObjectOfType<MissionProver>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    /// <summary>
    /// When the collider is triggert, add a new text to the Missionfield depends on if the Mission is complete or not.
    /// After that the next scene get loaded.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    /// @author Ronja Haas & Anna-Lisa Müller
    private void OnTriggerEnter(Collider other)
    {
        if (missionProver.mission.IsComplete()) 
        {
			missionProver.missiontext.text += "             " + "\n";
			missionProver.missiontext.text += "------------------------------" + "\n";
			missionProver.missiontext.text += "             " + "\n";
            missionProver.missiontext.text += "Gewonnen!";
            OpenPanel("winPanel");
        }
        else if (!missionProver.mission.IsComplete())
        {
			missionProver.missiontext.text += "             " + "\n";
			missionProver.missiontext.text += "------------------------------" + "\n";
			missionProver.missiontext.text += "             " + "\n";
            missionProver.missiontext.text += "Verloren!";
            OpenPanel("losePanel");
        }
    }
    
	/// <summary>
    /// Opens the relevant popUp-Panel depending on if the mission is complete or not
    /// </summary>
    /// <param name="panelName">the name of the panel what to open</param>
    /// @author Ronja Haas & Anna-Lisa Müller
    public void OpenPanel(string panelName)
    {
        panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {
                if (panel.name != panelName)
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

}
