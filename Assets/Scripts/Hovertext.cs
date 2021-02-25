using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovertext : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (OverRail.textstatus == "off")
        {
            Destroy(gameObject);
        }
    }
}
