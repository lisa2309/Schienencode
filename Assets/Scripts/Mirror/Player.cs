using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Player : NetworkBehaviour
{

public GameObject gerade_schiene;
public GameObject kurve_ro;
public GameObject kurve_lo;

public GameObject tunel_in;
public GameObject tunel_out;

private GameObject prefabtoinstant;


private ObjectPlacer objectPlacer;
private DeleteRail deletrail;


void Start() {
prefabtoinstant = gerade_schiene;

if(this.isLocalPlayer){

   objectPlacer = FindObjectOfType<ObjectPlacer>();
   objectPlacer.player = this;
   deletrail = FindObjectOfType<DeleteRail>();
   deletrail.player=this;
}

}



[Command]
 void insprefab(string prefabto, Vector3 finalPosition, float rotate)
    {
        
        switch (prefabto){
            case "Straight270Final":
            prefabtoinstant = gerade_schiene;

            break;
            case "CurveL0Final":
            prefabtoinstant = kurve_lo;
            break;
            case "CurveR0Final":
            prefabtoinstant = kurve_ro;
            break;
            case "TunnelIn":
            prefabtoinstant = tunel_in;
            break;
            case "TunnelOut":
            prefabtoinstant = tunel_out;
            break;
            default:
            prefabtoinstant = gerade_schiene;
            break;
        }
            
        GameObject cloneObj = Instantiate(prefabtoinstant, finalPosition, Quaternion.Euler(0, rotate, 0));
        cloneObj.name = prefabtoinstant.name;
        NetworkServer.Spawn(cloneObj,this.connectionToClient);
       
        
    }

    // Update is called once per frame
  public  void anrufen(string prefabname, Vector3 finalPosition, float rotate)
    {
          if (!isLocalPlayer) return;
              
               insprefab(prefabname, finalPosition, rotate);
            

    }


    // Called by the Player
    [Client]
    public void TellServerToDestroyObject(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    // Executed only on the server
    [Command]
    private void CmdDestroyObject(GameObject obj)
    {
        // It is very unlikely but due to the network delay
        // possisble that the other player also tries to
        // destroy exactly the same object beofre the server
        // can tell him that this object was already destroyed.
        // So in that case just do nothing.
        if(!obj) return;

        NetworkServer.Destroy(obj);
    }
}


