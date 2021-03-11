using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/*
Quelle: https://www.youtube.com/watch?v=D9DrW7_tMa8
*/
/// <summary>
/// A hovertext of the rail, which the mouse is currently on, is created.
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller
public class OverRail : MonoBehaviour
{
	/// <summary>
	/// Textobject for the Popup
	/// </summary>
    public Transform popuptext;

	/// <summary>
	/// String to tell if the Text is ON or Off
	/// </summary>
    public static string textstatus = "off";

    /// <summary>
    /// name of prefab for all Curve Rails
    /// </summary>
	private const string RAILCURVE = "Curve";

	/// <summary>
	/// name of prefab the Straight Rail
	/// </summary>
	private const string RAILSTRAIGHT = "Strai";

	/// <summary>
	/// name of prefab for all Switch Rails
	/// </summary>
	private const string RAILSWITCH = "Switc";

	/// <summary>
	/// name of prefab for the Startrail
	/// </summary>
	private const string RAILSTART = "RailS";

	/// <summary>
	/// name of prefab for the Endrail
	/// </summary>
	private const string RAILEND = "RailE";

	/// <summary>
	/// name of prefab all Tunnel Rails
	/// </summary>
	private const string RAILTUNNEL = "Tunne";

	/// <summary>
	/// name of prefab the Tunnel Entry
	/// </summary>
	private const string RAILTUNNELIN = "TunnelIn";

	/// <summary>
	/// name of prefab the trainstation
	/// </summary>
	private const string TRAINSTATIONREQUEST = "Train";

	/// <summary>
    /// r stands for a rail, which can be programmed with if, for, while
    /// </summary>
	private const string SWITCHPROGRAMMABLE= "R";
    
	/// <summary>
	/// Create the popuptext depends on wich Rail the mouse is on and instantiate it.
	/// </summary>
	/// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseEnter()
    {
        if (textstatus == "off")
        {
			string objectName = gameObject.name;
			char[] objectLetters = objectName.ToCharArray();
			string finalName = convertCharArrayToString(5, objectLetters);

			switch(finalName) 
			{
				case RAILCURVE:
					popuptext.GetComponent<TextMesh>().text = "Kurve";
					break;

				case RAILSTRAIGHT:
					popuptext.GetComponent<TextMesh>().text = "Gerade";
					break;

				case RAILSWITCH:
					if (gameObject.name.Contains(SWITCHPROGRAMMABLE))
					{
						popuptext.GetComponent<TextMesh>().text = "Weiche " + gameObject.GetComponent<SwitchScript>().mode;
					}
					else
					{
						popuptext.GetComponent<TextMesh>().text = "Weiche";
					}
					break;

				case RAILSTART:
					popuptext.GetComponent<TextMesh>().text = "Start";
					break;

				case RAILEND:
					popuptext.GetComponent<TextMesh>().text = "Ziel";
					break;

				case RAILTUNNEL:
					if(objectName == RAILTUNNELIN)
					{
						popuptext.GetComponent<TextMesh>().text = "Eingang Tunnel";
					} else {
						popuptext.GetComponent<TextMesh>().text = "Ausgang Tunnel";
					}
					break;

				case TRAINSTATIONREQUEST:
					int stationNumber = gameObject.GetComponent<StationScript>()._stationNumber + 1;
					popuptext.GetComponent<TextMesh>().text = "Bahnhof " + stationNumber;
					break;

				default:
					popuptext.GetComponent<TextMesh>().text = gameObject.name;
					break;
			}
            textstatus = "on";
            Instantiate(popuptext, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), popuptext.rotation);
        }
    }

	/// <summary>
	/// Set textsatus OFF when it's On
	/// </summary>
	/// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseExit()
    {
        if (textstatus == "on")
        {
            textstatus = "off";
        }
    }

	/// <summary>
	/// Form's a new String from the char array with only the amount of number of char's from size
	/// </summary>
	/// <param name="size">size of how many letters is need for the return string</param>
	/// <param name="name">char array wich is used to form a new string</param>
	/// <returns>String wich is based on the char array name</returns>
	/// @author Ronja Haas & Anna-Lisa Müller 
	string convertCharArrayToString(int size, char[] name) {
		char[] word = new char[size];
		for (int i = 0; i < size; i++)
		{
			word[i] = name[i];
		}
		return new string(word);
	}
}
