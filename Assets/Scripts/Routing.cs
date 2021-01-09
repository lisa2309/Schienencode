using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Not working, just a Plan for routing
/// </summary>
/// @author Florian Vogel & Bjarne Bensel 
public class Routing : MonoBehaviour
{
    GameObject[] rails;
    GameObject buffer;
    List<GameObject> route = new List<GameObject>();

    /// <summary>
    /// This Should be triggert when Player is finished. Generates the Route und starts the Train
    /// </summary>
    /// <param name="start"></param>
    /// @author Florian Vogel & Bjarne Bensel 
    void GenerateRoute(GameObject start)
    {
        buffer = start;
        route.Add(buffer);
        rails = GameObject.FindGameObjectsWithTag("Schiene");
        foreach(GameObject rail in rails)
        {
            if (rail.transform.position.x == (buffer.transform.position.x + GetDirectionX(buffer)))
            {
                if(rail.transform.position.x == (buffer.transform.position.x + GetDirectionX(buffer)))
                {

                }
            }
        }
    }

    /// <summary>
    /// Calculates the offset on X axis
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>shift on X axis </returns>
    /// @author Florian Vogel & Bjarne Bensel 
    int GetDirectionX(GameObject gameObject)
    {
        if(gameObject.transform.rotation.y == 0)
        {
            return 4;
        }
        else if(gameObject.transform.rotation.y == 180)
        {
            return -4;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Calculates the offset on Z axis
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>shift on Z axis</returns>
    /// @author Florian Vogel & Bjarne Bensel 
    int GetDirectionZ(GameObject gameObject)
    {
        if (gameObject.transform.rotation.y == 270)
        {
            return 4;
        }
        else if (gameObject.transform.rotation.y == 90)
        {
            return -4;
        }
        else
        {
            return 0;
        }
    }
}
