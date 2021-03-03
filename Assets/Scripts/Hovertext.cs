using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovertext : MonoBehaviour
{
    /* created by: SWT-P_WS_2021_Schienencode */
    /*
    Quelle: https://www.youtube.com/watch?v=D9DrW7_tMa8
    */
    /// <summary>
    /// Destry's the popuptext to prevent it isn't spamt all the time 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    
    
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
