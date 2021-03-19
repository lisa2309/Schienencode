using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Destroy's the popuptext to prevent it isn't spamt all the time 
/// @source: https://www.youtube.com/watch?v=D9DrW7_tMa8
/// Modified by: Ronja Haas & Anna-Lisa Müller 
/// </summary>
public class HoverText : MonoBehaviour
{
    /// <summary>
    /// When the textstatus is set on off it destroy's the popuptext
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void Update()
    {
        if (OverRail.textstatus == "off")
        {
            Destroy(gameObject);
        }
    }
}
