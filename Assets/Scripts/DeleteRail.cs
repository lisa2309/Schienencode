using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    /// 
    /// </summary>
    public Player player=null;

    //private bool isDeletable = true;

    /// <summary>
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        //Debug.Log("is local "+isLocalPlayer);
        //if (!isLocalPlayer) return;
        //if (!isDeletable) return;
        if (MissionProver.deleteOn) destroyrail();

    }

    // public void DeactivateDeletable()
    // {
    //     Debug.Log("In Deactivation deletable...");
    //     isDeletable = false;
    // }

            // destroy for everyone on the server
        [Command]
        void destroyrail()
        {
            NetworkServer.Destroy(gameObject);
        }

}
