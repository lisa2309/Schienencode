﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Database;
using SimpleJSON;
using UnityEngine.UI;


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

public static Player player;
private CameraMovement camera;

// WIP -----------------------------------------------------------------------------
    /// <summary>
    /// Canvas for player gui, displays all player data
    /// </summary>
    [SerializeField] private Canvas playerCanvas;

    /// <summary>
    /// Text to display the name of the player
    /// </summary>
    [SerializeField] private Text playerNameText;

    /// <summary>
    /// Text to display the players info from the game server
    /// </summary>
    [SerializeField] private Text playerInfoText;

    /// <summary>
    /// Text to display the games info from the game server
    /// </summary>
    [SerializeField] private Text gameInfoText;

    /// <summary>
    /// Player name
    /// </summary>
    private String playerName;
// WIP ----------------------------------------------------------------------------  

/// <summary>
/// 
/// @author Ahmed L'harrak
/// initialisation of variables dbcon and objectplacer
/// and then asignment current player object to player object in dbcon and  objectplacer scripts
/// </summary>
void Start() {
    prefabtoinstant = gerade_schiene;
    player = this;
    if(this.isLocalPlayer){
        
        dbCon = FindObjectOfType<DatabaseConnector>();
        dbCon.player = this;
        Debug.Log("is Client " + this.netId);

        objectPlacer = FindObjectOfType<ObjectPlacer>();
        objectPlacer.player = this;
       // deletrail = FindObjectOfType<DeleteRail>();
       // deletrail.player=this;
       // camera = FindObjectOfType<CameraMovement>(); 
        //camera.MaxFieldCameraView();
        playerCanvas.enabled = false;
    }
    // WIP ----------------------------------------------------------------------------
    else
    {
        // Iteration through JSON-Nodes
        foreach (KeyValuePair<string, JSONNode> kvp in GameServer.Instance.PlayerInfos)
        {
            playerInfoText.text += kvp.Key + " : " + kvp.Value + "\n";
        }

        foreach (KeyValuePair<string, JSONNode> kvp in GameServer.Instance.GameInfos)
        {
            gameInfoText.text += kvp.Key + " : " + kvp.Value + "\n";
        }

        // Accessing Single Value
        playerNameText.text = GameServer.Instance.PlayerInfos["name"].Value;
    }
// WIP ----------------------------------------------------------------------------   

    if (this.isServer)
    {
    GameObject.Find("ButtonStartTrain").GetComponent<Button>().onClick.RemoveAllListeners();
    GameObject.Find("ButtonStartTrain").GetComponent<Button>().onClick.AddListener(CmdPressButton);
        Debug.Log("SERVER ");
       dbCon.RetrieveFromDatabase();
    }

    camera = FindObjectOfType<CameraMovement>();
    camera.MaxFieldCameraView();
}

// WIP ----------------------------------------------------------------------------   
/// <summary>
/// Waits for game result to call FinishGame
/// </summary>
/// TODO: requirements for end of game (bool from another class?)
private void Update()
{
    //if (isLocalPlayer && gameFinished) FinishGame();
}
// WIP ----------------------------------------------------------------------------  






 void CmdPressButton()
     {
            if (this.isServer)
    {

        Debug.Log("SERVER -------");
     
    }
        starttrain();

    }

public void starttrain(){
 
     if (isServer)
     {
        Debug.Log("Start button gedrückt");
        FindObjectOfType<Routing>().GenerateRoute();

    }
    else{
        Debug.Log("ist kein Superplayer");
    }
 
}

public void newscen(String map)
{
FindObjectOfType<MyNetworkManager>().ServerChangeScene(map); 

}

public uint getId()
{
    return this.netId;
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
        if (MissionProver.buildOnDB)
        {
            Destroy(cloneObj.GetComponent<DeleteRail>());
            if (cloneObj.name.Equals("TunnelIn")) cloneObj.GetComponent<InTunnelScript>().buildOnDB=true;
        }
        if (cloneObj.name.Equals("TunnelOut")) cloneObj.GetComponent<OutTunnelScript>().AddOutTunnel();
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
    
    // WIP ----------------------------------------------------------------------------
    /// <summary>
    /// Function that handles game results.
    /// Currently lets Host automatically win.
    /// </summary>
    /// TODO: Implement connection to actual game results
    private void FinishGame()
    {
        if (GameServer.Instance.PlayerInfos["isHost"].AsBool)
        {
            GameServer.Instance.HandleGameResults(1, GameServer.Instance.PlayerInfos["name"].Value);
        }
        else
        {
            CmdGetHostName(netId);
        }
    }

    /// <summary>
    /// Command for getting the hosts name, needed to handle game results
    /// </summary>
    /// <param name="connId">NetId of the player that called this command</param>
    [Command]
    public void CmdGetHostName(uint connId)
    {
        NetworkConnection receiverConnection = NetworkIdentity.spawned[connId].connectionToClient;
        TargetReceiveHostName(receiverConnection, GameServer.Instance.PlayerInfos["name"].Value);
    }
    
    /// <summary>
    /// TargetRPC that sends the hosts name back to the local player and calls HandleGameResults
    /// </summary>
    /// <param name="connection">Connection for the TargetRPC</param>
    /// <param name="hostName">Name of the host</param>
    /// TODO: Implement connection to actual game results
    [TargetRpc]
    public void TargetReceiveHostName(NetworkConnection connection, string hostName)
    {
        GameServer.Instance.HandleGameResults(2,hostName);
    }

    // WIP ----------------------------------------------------------------------------
}


