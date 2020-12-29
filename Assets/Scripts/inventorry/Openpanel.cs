using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openpanel : MonoBehaviour
{

    public GameObject auswahlpanel;

    public void panelopen()
    {

        if (auswahlpanel != null)
        {

            foreach (Transform panel in auswahlpanel.transform.parent.GetComponentInChildren<Transform>())
            {
                if (panel.name != auswahlpanel.name)
                {
                    panel.gameObject.SetActive(false);
                }

            }


            if (auswahlpanel.activeSelf)
            {

                auswahlpanel.SetActive(false);
            }
            else
            {
                auswahlpanel.SetActive(true);
            }

        }
    }

}
