using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Class to open the help panels and switch between the detailed pages for manual and controlls
/// </summary>
/// @author Florian Vogel & Bjarne Bensel & Ahmed L'harrak & Bastian Badde
public class Help : MonoBehaviour
{
    /// <summary>
    /// Collection of the different PopUp-Panels
    /// </summary>
    private GameObject panels;

    /// <summary>
    /// Opens the initial help panel
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void ShowHelp()
    {        
        Debug.Log("Help opened");        
        OpenSpecificPanel("panelTutorial01");
    }

    /// <summary>
    /// Opens the controll help page
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void OpenControl()
    {
        Debug.Log("Control opened");
        OpenSpecificPanel("panelTutorial03");
    }

    /// <summary>
    /// Opens the manual help page
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel 
    public void OpenManual()
    {
        Debug.Log("Manual opened");
        OpenSpecificPanel("panelTutorial02");
    }

    /// <summary>
    /// Opens the relevant popUp-Panel depending on the panelString
    /// </summary>
    /// <param name="panelString">The name of the panel what to open</param>
    /// @author Ahmed L'harrak & Bastian Badde
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
                        MissionProver.panelIsOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
