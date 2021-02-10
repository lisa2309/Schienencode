using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Database;


public class Player : NetworkBehaviour
{

public GameObject gerade_schiene;
public GameObject kurve_ro;
public GameObject kurve_lo;

public GameObject tunel_in;
public GameObject tunel_out;

public GameObject station;

    public GameObject switch_l_0;
    public GameObject switch_l_1;
    public GameObject switch_r_0;
    public GameObject switch_r_1;

    public GameObject rail_start;
    public GameObject rail_end;

    private GameObject prefabtoinstant;


private ObjectPlacer objectPlacer;
private DatabaseConnector dbCon;
private DeleteRail deletrail;


void Start() {
prefabtoinstant = gerade_schiene;

if(this.isLocalPlayer){
    dbCon = FindObjectOfType<DatabaseConnector>();
    dbCon.player = this;

   objectPlacer = FindObjectOfType<ObjectPlacer>();
   objectPlacer.player = this;
   deletrail = FindObjectOfType<DeleteRail>();
   deletrail.player=this;
}

}


/// <summary>
/// 
/// Variables:
/// cloneObj:
/// @author
/// </summary>
/// <param name="prefabto"></param>
/// <param name="finalPosition"></param>
/// <param name="rotate"></param>
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
            case "SwitchL0Final":
                prefabtoinstant = switch_l_0;
                break;
            case "SwitchL1Final":
                prefabtoinstant = switch_l_1;
                break;
            case "SwitchR0Final":
                prefabtoinstant = switch_r_0;
                break;
            case "SwitchR1Final":
                prefabtoinstant = switch_r_1;
                break;
            case "RailStart":
                prefabtoinstant = rail_start;
                break;
            case "RailEnd":
                prefabtoinstant = rail_end;
                break;
            case "TrainStation":
                prefabtoinstant = station;
                break;
            default:
            prefabtoinstant = gerade_schiene;
            break;
        }

        // if (MissionProver.buildOnDB)
        // {
        //     Debug.Log("In delete DeleteRail...");
        //     try
        //     {
        //         prefabtoinstant.GetComponent<DeleteRail>().DeactivateDeletable();
        //         //prefabtoinstant.GetComponent<DeleteRail>().enabled = false;
        //         //Destroy(prefabtoinstant.GetComponent<DeleteRail>());
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }
        // }
        GameObject cloneObj = Instantiate(prefabtoinstant, finalPosition, Quaternion.Euler(0, rotate, 0));
        cloneObj.name = prefabtoinstant.name;
        if (MissionProver.buildOnDB) Destroy(cloneObj.GetComponent<DeleteRail>());
        NetworkServer.Spawn(cloneObj,this.connectionToClient);      
    }

    /// <summary>
    /// Update is called once per frame
    /// @author
    /// </summary>
    /// <param name="prefabname"></param>
    /// <param name="finalPosition"></param>
    /// <param name="rotate"></param>
  public  void anrufen(string prefabname, Vector3 finalPosition, float rotate)
    {
        if (!isLocalPlayer) return;   
        insprefab(prefabname, finalPosition, rotate);
    }

    /// <summary>
    /// Called by the Player
    /// @author
    /// </summary>
    /// <param name="obj"></param>
    [Client]
    public void TellServerToDestroyObject(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    /// <summary>
    /// Executed only on the server
    /// @author
    /// </summary>
    /// <param name="obj"></param>
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


