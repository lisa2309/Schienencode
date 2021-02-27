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
	private const string RAILCURVE = "Curve";
	private const string RAILSTRAIGHT = "Strai";
	private const string RAILSWITCH = "Switc";
	private const string RAILSTART = "RailS";
	private const string RAILEND = "RailE";
	private const string RAILTUNNEL = "Tunne";
	private const string RAILTUNNELIN = "TunnelIn";
    
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
					popuptext.GetComponent<TextMesh>().text = "Weiche";
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
						popuptext.GetComponent<TextMesh>().text = "Ausganeg Tunnel";
					}
					break;

				default:
					popuptext.GetComponent<TextMesh>().text = gameObject.name;
					break;
			}
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

	string convertCharArrayToString(int size, char[] name) {
		char[] word = new char[size];
		for (int i = 0; i < size; i++)
		{
			word[i] = name[i];
		}
		return new string(word);
	}
}
