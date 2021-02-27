using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Database;


public class Player : NetworkBehaviour
{
/// <summary>
/// name of prefab for Straight
/// </summary>
private const string str_geradeschiene = "Straight270Final";
/// <summary>
/// name of prefab for curve left
/// </summary>
private const string str_curveleft = "CurveL0Final";
/// <summary>
/// name of prefab for curve rigth
/// </summary>
private const string str_curverigth = "CurveR0Final";
/// <summary>
/// name of prefab for tunnel in
/// </summary>
private const string str_tunelin = "TunnelIn";
/// <summary>
/// name of prefab for tunnel out
/// </summary>
private const string str_tunelout = "TunnelOut";
/// <summary>
/// name of prefab for switch left 0
/// </summary>
private const string str_switchl0 = "SwitchL0Final";
/// <summary>
/// name of prefab for switch left 1
/// </summary>
private const string str_switchl1 = "SwitchL1Final";
/// <summary>
/// name of prefab for switch rigth 0
/// </summary>
private const string str_switchr0 ="SwitchR0Final";
/// <summary>
/// name of prefab for switch rigth 1
/// </summary>
private const string str_switchr1 ="SwitchR1Final";
/// <summary>
/// name of prefab for rail start
/// </summary>
private const string str_railstart = "RailStart";
/// <summary>
/// name of prefab for rail end
/// </summary>
private const string str_railend = "RailEnd";
/// <summary>
/// name of prefab for trainstation
/// </summary>
private const string str_trainstation = "TrainStation";

/////////////////////////// gameobjects 

/// <summary>
///   Straight prefab gameobject
/// </summary>
public GameObject gerade_schiene;
/// <summary>
///   curve rigth prefab gameobject
/// </summary>
public GameObject kurve_ro;
/// <summary>
///   curve left prefab gameobject
/// </summary>
public GameObject kurve_lo;
/// <summary>
///   tunel in  prefab gameobject
/// </summary>

public GameObject tunel_in;
/// <summary>
///   tunnel out prefab gameobject
/// </summary>
public GameObject tunel_out;
/// <summary>
///   trainstation prefab gameobject
/// </summary>

public GameObject station;
/// <summary>
///   switch left 0 prefab gameobject
/// </summary>

    public GameObject switch_l_0;
    /// <summary>
///   switch left 1 prefab gameobject
/// </summary>
    public GameObject switch_l_1;
    /// <summary>
///   switch rigth 0 prefab gameobject
/// </summary>
    public GameObject switch_r_0;
    /// <summary>
///   switch rigth 1 prefab gameobject
/// </summary>
    public GameObject switch_r_1;
    /// <summary>
///   rail start  prefab gameobject
/// </summary>

    public GameObject rail_start;
    /// <summary>
///   rail end prefab gameobject
/// </summary>
    public GameObject rail_end;



    /// <summary>
///    gameobjict variable to save instantiate prefabe
/// </summary>
    private GameObject prefabtoinstant;
/// <summary>
///   script objectplacer
/// </summary>
private ObjectPlacer objectPlacer;
/// <summary>
/// 
/// </summary>
private DatabaseConnector dbCon;
/// <summary>
///
/// </summary>
private DeleteRail deletrail;

/// <summary>
/// 
/// @author Ahmed L'harrak
/// initialisation of variables dbcon and objectplacer
/// and then asignment current player object to player object in dbcon and  objectplacer scripts
/// </summary>
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
/// @author Ahmed L'harrak
/// lokking for witch prefabe will be instiantiate  seqarch by name if ther exist then prefabtoinstant will be the correspend gameobject of this name
/// then it will be this prefabe created for all players in this Game
/// </summary>
/// <param name="prefabname"> the name of prefabe to be intantiate for alle Player in this Game </param>
/// <param name="finalPosition"> the position (cordinaten ) where the prefabe shoulde created </param>
/// <param name="rotate"> with witch rotation should this prefab creatde</param>
[Command]
 void insprefab(string prefabto, Vector3 finalPosition, float rotate)
    {
        switch (prefabto){
            case str_geradeschiene:
            prefabtoinstant = gerade_schiene;
            break;
            case str_curveleft:
            prefabtoinstant = kurve_lo;
            break;
            case str_curverigth:
            prefabtoinstant = kurve_ro;
            break;
            case str_tunelin:
            prefabtoinstant = tunel_in;
            break;
            case str_tunelout:
            prefabtoinstant = tunel_out;
            break;
            case str_switchl0:
                prefabtoinstant = switch_l_0;
                break;
            case str_switchl1:
                prefabtoinstant = switch_l_1;
                break;
            case str_switchr0:
                prefabtoinstant = switch_r_0;
                break;
            case str_switchr1:
                prefabtoinstant = switch_r_1;
                break;
            case str_railstart:
                prefabtoinstant = rail_start;
                break;
            case str_railend:
                prefabtoinstant = rail_end;
                break;
            case str_trainstation:
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
    /// @author Ahmed L'harrak
    /// this function is a public function to be called from other scripts like opbjectplacer it will then create the chosen prefabe in the chosen cordinaten and rotation value
    /// </summary>
    /// <param name="prefabname"> the name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> the position (cordinaten ) where the prefabe shoulde created </param>
    /// <param name="rotate"> with witch rotation should this prefab creatde</param>
  public  void anrufen(string prefabname, Vector3 finalPosition, float rotate)
    {
        if (!isLocalPlayer) return;   
        insprefab(prefabname, finalPosition, rotate);
    }

    /// <summary>
    /// Called by the Player
    /// @author Ahmed L'harrak
    /// call the function cmdDestroyobject()
    /// </summary>
    /// <param name="obj">the gameobject to be deleted </param>
    [Client]
    public void TellServerToDestroyObject(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    /// <summary>
    /// Executed only on the server
    /// @author Ahmed L'harrak
    /// if this gameobject exict it will be deleted from this Game for all Players
    /// </summary>
    /// <param name="obj"> gameobject to be destroyed </param>
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


