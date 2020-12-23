using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openpanel : MonoBehaviour
{
    
    public GameObject auswahlpanel;
   
    public void panelopen(){

        if (auswahlpanel!= null){


            if(auswahlpanel.activeSelf){

 auswahlpanel.SetActive(false);
            }
            else{
                 auswahlpanel.SetActive(true);
            }
               
        }
    }

}
