using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This is a window which has all property(rotation) of the current gameobject
/// </summary>
/// @author Ahmed L'harrak
public class Panel : MonoBehaviour
{
    /// <summary>
    /// This is a panel window who contain all properties(rotation button, create button) of the clicked prefabs 
    /// </summary>
    public GameObject selectionPanel;

    /// <summary>
    /// This function make the windows panel visible if it's not and close the others. 
    /// If the panel is allredy visible, then the panel will be close after the function is called
    /// </summary>
    /// @author Ahmed L'harrak
    public void OpenPanel()
    {
        if (selectionPanel != null)
        {
            foreach (Transform panel in selectionPanel.transform.parent.GetComponentInChildren<Transform>())
            {
                if (panel.name != selectionPanel.name)
                {
                    panel.gameObject.SetActive(false);
                }
            }
            if (selectionPanel.activeSelf)
            {
                selectionPanel.SetActive(false);
            }
            else
            {
                selectionPanel.SetActive(true);
            }
        }
    }

}
