﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// this is a windows the have all property(rotation) of the current gameobject of this windows
/// </summary>
/// @author Ahmed L'harrak
public class Panel : MonoBehaviour
{
    /// <summary>
    /// this is panel windows who content  all proprietes(rotation create button) of the clicked prefabs 
    /// </summary>
    public GameObject selectionPanel;

    /// <summary>
    /// this function makes the windows panel visible if it's not  and close the others 
    /// if the panel is allredy visible then will be close after call of this function
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
