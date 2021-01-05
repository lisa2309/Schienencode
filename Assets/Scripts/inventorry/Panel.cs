using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author Ahmed L'harrak
public class Panel : MonoBehaviour
{
    public GameObject selectionPanel;

    /// <summary>
    /// 
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
