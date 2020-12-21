using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */

public class DeleteRail : MonoBehaviour
{
    public static BoxCollider railBoxCollider;

    /// <summary>
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    void OnMouseDown()
    {
        Destroy(this.gameObject); 
    }

}
