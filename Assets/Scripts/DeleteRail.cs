using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// This class is attached to a rail and ensures that the rail is deleted when the mouse is clicked on it
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller 
public class DeleteRail : MonoBehaviour
{
    public static BoxCollider railBoxCollider;

    /// <summary>
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        Destroy(this.gameObject); 
    }

}
