using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;
using Mirror;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a rail and ensures that the rail is deleted when the mouse is clicked on it
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
public class DeleteRail : NetworkBehaviour
{
    /// <summary>
    /// Object from Player
    /// </summary>
    public Player player = null;

    /// <summary>
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        if (MissionProver.deleteOn)
        {
            destroyrail();
            Destroy(GameObject.Find("RailPopUpText(Clone)"));
        }
    }

    /// <summary>
    /// destroy for everyone on the server
    /// @author Ahmed L'harrak
    /// </summary>
    [Command]
    void destroyrail()
    {
        if (gameObject.name.Equals("TunnelOut"))
        {
            OutTunnelScript.RemoveOutTunnel(gameObject.GetComponent<OutTunnelScript>().OutTunnelNumber);
        }
        NetworkServer.Destroy(gameObject);
    }

}
