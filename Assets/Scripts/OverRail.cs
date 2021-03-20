using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// A hovertext of the rail, which the mouse is currently on, is created
/// </summary>
/// Modified by Ronja Haas & Anna-Lisa Müller
/// @source Quelle: https://www.youtube.com/watch?v=D9DrW7_tMa8
public class OverRail : MonoBehaviour
{
	/// <summary>
	/// Textobject for the Popup
	/// </summary>
    public Transform popuptext;

	/// <summary>
	/// String to tell if the Text is ON or Off
	/// </summary>
    public static string TextStatus = "off";

    /// <summary>
    /// Name of prefab for all Curve Rails
    /// </summary>
	private const string RailCurve = "Curve";

	/// <summary>
	/// Name of prefab the Straight Rail
	/// </summary>
	private const string RailStraight = "Strai";

	/// <summary>
	/// Name of prefab for all Switch Rails
	/// </summary>
	private const string RailSwitch = "Switc";

	/// <summary>
	/// Name of prefab for the Startrail
	/// </summary>
	private const string RailStart = "RailS";

	/// <summary>
	/// Name of prefab for the Endrail
	/// </summary>
	private const string RailEnd = "RailE";

	/// <summary>
	/// Name of prefab all Tunnel Rails
	/// </summary>
	private const string RailTunnel = "Tunne";

	/// <summary>
	/// Name of prefab the Tunnel Entry
	/// </summary>
	private const string RailTunnelIn = "TunnelIn";

	/// <summary>
	/// Name of prefab the trainstation
	/// </summary>
	private const string TrainstationRequest = "Train";

	/// <summary>
    /// R stands for a rail, which can be programmed with if, for, while
    /// </summary>
	private const string SwitchProgrammable = "R";
    
	/// <summary>
	/// Create the popuptext depends on wich Rail the mouse is on and instantiate it
	/// objectName: Name of the gameobject where the mouse is over
	/// objectLetters: ObjectName split up in the separate letters
	/// finalName: The first five letters of objectName
	/// </summary>
	/// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseEnter()
    {
        if (TextStatus == "off")
        {
			string objectName = gameObject.name;
			char[] objectLetters = objectName.ToCharArray();
			string finalName = convertCharArrayToString(5, objectLetters);
			switch(finalName) 
			{
				case RailCurve:
					popuptext.GetComponent<TextMesh>().text = "Kurve";
					break;
				case RailStraight:
					popuptext.GetComponent<TextMesh>().text = "Gerade";
					break;
				case RailSwitch:
					if (gameObject.name.Contains(SwitchProgrammable))
					{
						popuptext.GetComponent<TextMesh>().text = "Weiche " + gameObject.GetComponent<SwitchScript>().mode;
					}
					else
					{
						popuptext.GetComponent<TextMesh>().text = "Weiche";
					}
					break;
				case RailStart:
					popuptext.GetComponent<TextMesh>().text = "Start";
					break;
				case RailEnd:
					popuptext.GetComponent<TextMesh>().text = "Ziel";
					break;
				case RailTunnel:
					if(objectName == RailTunnelIn)
					{
						popuptext.GetComponent<TextMesh>().text = "Eingang Tunnel";
					} else {
						popuptext.GetComponent<TextMesh>().text = "Ausgang Tunnel";
					}
					break;
				case TrainstationRequest:
					int stationNumber = gameObject.GetComponent<StationScript>().stationNumber + 1;
					popuptext.GetComponent<TextMesh>().text = "Bahnhof: C" + stationNumber;
					break;
				default:
					popuptext.GetComponent<TextMesh>().text = gameObject.name;
					break;
			}
            TextStatus = "on";
            Instantiate(popuptext, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), popuptext.rotation);
        }
    }

	/// <summary>
	/// Set textsatus OFF when it's On
	/// </summary>
	/// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseExit()
    {
        if (TextStatus == "on")
        {
            TextStatus = "off";
        }
    }

	/// <summary>
	/// Form's a new String from the char array with only the amount of number of char's from size
	/// word: char array with the size of size
	/// </summary>
	/// <param name="size">Size of how many letters is need for the return string</param>
	/// <param name="name">Char array wich is used to form a new string</param>
	/// <returns>String wich is based on the char array name</returns>
	/// @author Ronja Haas & Anna-Lisa Müller 
	public string convertCharArrayToString(int size, char[] name) {
		char[] word = new char[size];
		for (int i = 0; i < size; i++)
		{
			word[i] = name[i];
		}
		return new string(word);
	}
}
