using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class help : MonoBehaviour
{
    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;
    /// <summary>
    /// UI-Dropdown-object of a Unchosen-Switch for choosing the SwitchMode
    /// </summary>
    public Dropdown ddHelpValue;

    int auswahl = 0;

    public void showHelp()
    {
        
        Debug.Log("Help opened");
        
        OpenSpecificPanel("panelTutorial01");
    }

    public void openChoosenHelp()
    {
        
        //Debug.Log(ddHelpValue.ToString());
        if (true)
        {
            OpenSpecificPanel("panelTutorial02");
        }else if (ddHelpValue.value == 1)
        {
            OpenSpecificPanel("panelTutorial03");
        }
        Debug.Log(auswahl);
    }

    
    public void HandleInputData(int val)
    {
        auswahl = val;
    }

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


}
