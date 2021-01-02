using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routing : MonoBehaviour
{
    GameObject[] rails;
    GameObject buffer;
    List<GameObject> route = new List<GameObject>();

    void generateRoute(GameObject start)
    {
        //durch liste der schienen durchiterieren findbytag("schiene")
        buffer = start;
        route.Add(buffer);
        //buffer.getdirektion
        rails = GameObject.FindGameObjectsWithTag("Schiene");
        foreach(GameObject rail in rails)
        {
            if (rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer)))
            {
                if(rail.transform.position.x == (buffer.transform.position.x + getDirectionX(buffer)))
                {

                }
            }
        }
    }
    int getDirectionX(GameObject obj)
    {
        if(obj.transform.rotation.y == 0)
        {
            return 4;
        }
        else if(obj.transform.rotation.y == 180)
        {
            return -4;
        }
        else
        {
            return 0;
        }
    }

    int getDirectionZ(GameObject obj)
    {
        if (obj.transform.rotation.y == 270)
        {
            return 4;
        }
        else if (obj.transform.rotation.y == 90)
        {
            return -4;
        }
        else
        {
            return 0;
        }
    }
}
