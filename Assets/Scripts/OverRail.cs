using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// https://www.youtube.com/watch?v=D9DrW7_tMa8
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller
public class OverRail : MonoBehaviour
{
    public Transform popuptext;
    public static string textstatus = "off";
    
    void OnMouseEnter()
    {
        if (textstatus == "off")
        {
            popuptext.GetComponent<TextMesh>().text = gameObject.name;
            textstatus = "on";
            Instantiate(popuptext, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), popuptext.rotation);
        }
    }

    void OnMouseExit()
    {
        if (textstatus == "on")
        {
            textstatus = "off";
        }
    }
}
