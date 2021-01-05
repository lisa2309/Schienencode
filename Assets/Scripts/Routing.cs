using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author Florian Vogel & Bjarne Bensel 
public class Routing : MonoBehaviour
{
    GameObject[] rails;
    GameObject buffer;
    List<GameObject> route = new List<GameObject>();

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
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
