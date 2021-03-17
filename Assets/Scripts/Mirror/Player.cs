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
    private const string strGeradeschiene = "Straight270Final";

    /// <summary>
    /// name of prefab for curve left
    /// </summary>
    private const string strCurveleft = "CurveL0Final";

    /// <summary>
    /// name of prefab for curve rigth
    /// </summary>
    private const string strCurverigth = "CurveR0Final";

    /// <summary>
    /// name of prefab for tunnel in
    /// </summary>
    private const string strTunelin = "TunnelIn";

    /// <summary>
    /// name of prefab for tunnel out
    /// </summary>
    private const string strTunelout = "TunnelOut";

    /// <summary>
    /// name of prefab for switch left 0
    /// </summary>
    private const string strSwitchl0 = "SwitchL0Final";

    /// <summary>
    /// name of prefab for switch left 1
    /// </summary>
    private const string strSwitchl1 = "SwitchL1Final";

    /// <summary>
    /// name of prefab for switch rigth 0
    /// </summary>
    private const string strSwitchr0 ="SwitchR0Final";

    /// <summary>
    /// name of prefab for switch rigth 1
    /// </summary>
    private const string strSwitchr1 ="SwitchR1Final";

    /// <summary>
    /// name of prefab for rail start
    /// </summary>
    private const string strRailstart = "RailStart";

    /// <summary>
    /// name of prefab for rail end
    /// </summary>
    private const string strRailend = "RailEnd";

    /// <summary>
    /// name of prefab for trainstation
    /// </summary>
    private const string strTrainstation = "TrainStation";

    /// <summary>
    ///   Straight prefab gameobject
    /// </summary>
    public GameObject geradeSchiene;

    /// <summary>
    ///   curve rigth prefab gameobject
    /// </summary>
    public GameObject kurveRo;

    /// <summary>
    ///   curve left prefab gameobject
    /// </summary>
    public GameObject kurveLo;

    /// <summary>
    ///   tunel in  prefab gameobject
    /// </summary>
    public GameObject tunelIn;

    /// <summary>
    ///   tunnel out prefab gameobject
    /// </summary>
    public GameObject tunelOut;

    /// <summary>
    /// trainstation prefab gameobject
    /// </summary>
    public GameObject station;

    /// <summary>
    /// switch left 0 prefab gameobject
    /// </summary>
    public GameObject switchL0;

    /// <summary>
    /// switch left 1 prefab gameobject
    /// </summary>
    public GameObject switchL1;

    /// <summary>
    ///   switch rigth 0 prefab gameobject
    /// </summary>
    public GameObject switchR0;

    /// <summary>
    ///   switch rigth 1 prefab gameobject
    /// </summary>
    public GameObject switchR1;

    /// <summary>
    /// rail start  prefab gameobject
    /// </summary>
    public GameObject railStart;

    /// <summary>
    /// rail end prefab gameobject
    /// </summary>
    public GameObject railEnd;
    
    /// <summary>
    /// gameobjict variable to save instantiate prefabe
    /// </summary>
    private GameObject prefabtoinstant;

    /// <summary>
    /// script objectplacer
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
    /// </summary>
    public static Player player;

    /// <summary>
    /// 
    /// </summary>
    private CameraMovement camera;

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

    
private MissionProver       missionprv;

    /// <summary>
    /// initialisation of variables dbcon and objectplacer
    /// and then asignment current player object to player object in dbcon and  objectplacer scripts
    /// </summary>
    /// @author Ahmed L'harrak
    void Start() {
    prefabtoinstant = geradeSchiene;
    player = this;
    if(this.isLocalPlayer){
        
        dbCon = FindObjectOfType<DatabaseConnector>();
        dbCon.player = this;

        missionprv = FindObjectOfType<MissionProver>();
        missionprv.player = this;
        Debug.Log("is Client " + this.netId);
        
        objectPlacer = FindObjectOfType<ObjectPlacer>();
        objectPlacer.player = this;
        playerCanvas.enabled = false;
    }
    else
    {
        foreach (KeyValuePair<string, JSONNode> kvp in GameServer.Instance.PlayerInfos)
        {
            playerInfoText.text += kvp.Key + " : " + kvp.Value + "\n";
        }

        foreach (KeyValuePair<string, JSONNode> kvp in GameServer.Instance.GameInfos)
        {
            gameInfoText.text += kvp.Key + " : " + kvp.Value + "\n";
        }
        playerNameText.text = GameServer.Instance.PlayerInfos["name"].Value;
    }

    if (this.isServer)
    {
        GameObject.Find("ButtonStartTrain").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartTrain").GetComponent<Button>().onClick.AddListener(CmdPressButton);

        Debug.Log("SERVER ");
        dbCon.RetrieveFromDatabase();
    }
    else
    {
        foreach (var tunnel in FindObjectsOfType<OutTunnelScript>())
        {
            tunnel.InitOutTunnel();
        }
        foreach (var tunnel in FindObjectsOfType<InTunnelScript>())
        {
            tunnel.Register();
            tunnel.buildOnDB = true;
        }
        foreach (var switchScript in FindObjectsOfType<SwitchScript>())
        {
            switchScript.Register();
        }
    }

    camera = FindObjectOfType<CameraMovement>();
    camera.MaxFieldCameraView();
    }

    /// <summary>
    /// this method will called when a player click on own   start train button 
    /// </summary>
    /// @author Ahmed L'harrak
    void CmdPressButton()
    {
        if (this.isServer)
        {
           starttrain();
        }
        
    }

    /// <summary>
    /// start the train only from server side 
    /// </summary>
    /// @author Ahmed L'harrak
    public void starttrain()
    {
        if (isServer)
        {
            Debug.Log("Start button gedrückt");
            FindObjectOfType<Routing>().GenerateRoute();
        }
        else
        {
            Debug.Log("ist kein Superplayer");
        }
    }

    /// <summary>
    /// change the scene for example when a level finished than it will new scen laoded
    /// </summary>
    /// <param name="map"></param>
    /// @author Ahmed L'harrak
    public void newscen(String map)
    {
        FindObjectOfType<MyNetworkManager>().ServerChangeScene(map); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// @author Ronja Haas & Anna-Lisa Müller
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
    /// @author Ahmed L'harrak
    /// cloneObj: the instatiat game object
    [Command]
    void insprefab(string prefabto, Vector3 finalPosition, float rotate)
    {
        switch (prefabto){
            case strGeradeschiene:
                prefabtoinstant = geradeSchiene;
                break;
            case strCurveleft: 
                prefabtoinstant = kurveLo;
                break;
            case strCurverigth:
                prefabtoinstant = kurveRo;
                break;
            case strTunelin:
                prefabtoinstant = tunelIn;
                break;
            case strTunelout:
                prefabtoinstant = tunelOut;
                break;
            case strSwitchl0:
                prefabtoinstant = switchL0;
                break;
            case strSwitchl1:
                prefabtoinstant = switchL1;
                break;
            case strSwitchr0:
                prefabtoinstant = switchR0;
                break;
            case strSwitchr1:
                prefabtoinstant = switchR1;
                break;
            case strRailstart:
                prefabtoinstant = railStart;
                break;
            case strRailend:
                prefabtoinstant = railEnd;
                break;
            case strTrainstation:
                prefabtoinstant = station;
                break;
            default:
                prefabtoinstant = geradeSchiene;
                break;
        }
        GameObject cloneObj = Instantiate(prefabtoinstant, finalPosition, Quaternion.Euler(0, rotate, 0));
        cloneObj.name = prefabtoinstant.name;
        if (MissionProver.buildOnDB)
        {
            Destroy(cloneObj.GetComponent<DeleteRail>());
            if (cloneObj.name.Equals("TunnelIn")) cloneObj.GetComponent<InTunnelScript>().buildOnDB=true;
        }
        NetworkServer.Spawn(cloneObj,this.connectionToClient);  
        
        if (cloneObj.name.Equals("TunnelOut"))
        {
            initOutTunnelOnClient(cloneObj);
        }
        if (cloneObj.name.Equals("SwitchR1Final") || cloneObj.name.Equals("SwitchR0Final"))
        {
            RegisterSwitchOnClient(cloneObj);
        }
        if (cloneObj.name.Equals("TunnelIn"))
        {
            RegisterInTunnelOnClient(cloneObj);
        }
    }

    /// <summary>
    /// Initiate values of created Switch
    /// </summary>
    /// <param name="obj"> Gameobject of the Switch </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void RegisterSwitchOnClient(GameObject obj)
    {
        obj.GetComponent<SwitchScript>().Register();
    }
    
    /// <summary>
    /// Initiate values of created Switch
    /// </summary>
    /// <param name="obj"> Gameobject of the Switch </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void RegisterInTunnelOnClient(GameObject obj)
    {
        obj.GetComponent<InTunnelScript>().Register();
    }

    /// <summary>
    /// Initiate values of created OutTunnel
    /// </summary>
    /// <param name="obj"> Gameobject of the OutTunnel </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void initOutTunnelOnClient(GameObject obj)
    {
        Debug.Log("Init out on client+++++++++");
        obj.GetComponent<OutTunnelScript>().InitOutTunnel();
    }

    /// <summary>
    /// Update is called once per frame
    /// @author Ahmed L'harrak
    /// this function is a public function to be called from other scripts like opbjectplacer it will then create the chosen prefabe in the chosen cordinaten and rotation value
    /// </summary>
    /// <param name="prefabname"> the name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> the position (cordinaten ) where the prefabe shoulde created </param>
    /// <param name="rotate"> with witch rotation should this prefab creatde</param>
    /// @author Ahmed L'harrak
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
    /// @author Ahmed L'harrak
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
    /// @author Ahmed L'harrak
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
    
    /// <summary>
    /// Function that handles game results.
    /// Currently lets Host automatically win.
    /// </summary>
    /// TODO: Implement connection to actual game results
    /// @author
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
    /// @author 
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
    /// @author 
    [TargetRpc]
    public void TargetReceiveHostName(NetworkConnection connection, string hostName)
    {
        GameServer.Instance.HandleGameResults(2,hostName);
    }

    
    
    // +++++++ Synchronisation +++++++
    
    //Station
    
    /// <summary>
    /// Method to synchronize the station cargos
    /// </summary>
    /// <param name="currentStation">stationNumber of the relevant StationScript</param>
    /// <param name="value">cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void CargoChanged(int currentStation,int  value){
        if (this.isServer)
        {
            ClientCargoChanged(currentStation,value);
        }
        else {///client
            if (!isLocalPlayer) return; 
            ServerCargoChanged(currentStation,value);
        }
    }

    /// <summary>
    /// Command to synchronize the station cargos from server-side
    /// </summary>
    /// <param name="currentStation">stationNumber of the relevant StationScript</param>
    /// <param name="value">cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerCargoChanged(int currentStation,int  value){
        CargoChanged(currentStation,value);
    }

    /// <summary>
    /// Command to synchronize the station cargos from client-side
    /// </summary>
    /// <param name="currentStation">stationNumber of the relevant StationScript</param>
    /// <param name="value">cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientCargoChanged(int currentStation,int  value){
        FindObjectOfType<MissionProver>().SetStationCargo(currentStation, value);
    }
    
    //Switch-values
    
    /// <summary>
    /// Method to synchronize the switch-values
    /// </summary>
    /// <param name="switchNumber">the switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">the stationNumber whose cargo to compare with</param>
    /// <param name="compare">the decoded compare value</param>
    /// <param name="value">the value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void SwitchValuesChanged(int switchNumber, int cargo,int compare, int  value){
        if (this.isServer)
        {
            ClientSwitchValuesChanged(switchNumber, cargo, compare,value);
        }
        else {
            if (!isLocalPlayer) return; 
            ServerSwitchValuesChanged(switchNumber, cargo, compare,value);
        }
    }

    /// <summary>
    /// Command to synchronize the switch-values from server-side
    /// </summary>
    /// <param name="switchNumber">the switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">the stationNumber whose cargo to compare with</param>
    /// <param name="compare">the decoded compare value</param>
    /// <param name="value">the value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerSwitchValuesChanged(int switchNumber, int cargo,int compare, int  value){
        SwitchValuesChanged(switchNumber, cargo, compare,value);
    }

    /// <summary>
    /// Command to synchronize the switch-values from client-side
    /// </summary>
    /// <param name="switchNumber">the switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">the stationNumber whose cargo to compare with</param>
    /// <param name="compare">the decoded compare value</param>
    /// <param name="value">the value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientSwitchValuesChanged(int switchNumber, int cargo,int compare, int  value){
        FindObjectOfType<MissionProver>().SetSwitchValues(switchNumber, cargo, compare,value);
    }
    
    //Switch-mode
    
    /// <summary>
    /// Method to synchronize the switch-modes
    /// </summary>
    /// <param name="switchNumber">switchNumber of the relevant SwitchScript</param>
    /// <param name="mode">mode-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void SwitchModeChanged(int switchNumber, int mode){
        if (this.isServer)
        {
            ClientSwitchModeChanged(switchNumber,mode);
        }
        else {
            if (!isLocalPlayer) return; 
            ServerSwitchModeChanged(switchNumber, mode);
        }
    }

    /// <summary>
    /// Command to synchronize the switch-modes from server-side
    /// </summary>
    /// <param name="switchNumber">switchNumber of the relevant SwitchScript</param>
    /// <param name="mode">mode-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerSwitchModeChanged(int switchNumber, int mode){
        SwitchModeChanged(switchNumber, mode);
    }

    /// <summary>
    /// Command to synchronize the switch-modes from client-side
    /// </summary>
    /// <param name="switchNumber">switchNumber of the relevant SwitchScript</param>
    /// <param name="mode">mode-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientSwitchModeChanged(int switchNumber, int mode){
        FindObjectOfType<MissionProver>().SetSwitchMode(switchNumber, mode);
    }

    //In-Tunnel
    
    /// <summary>
    /// Method to synchronize the In-Tunnels
    /// </summary>
    /// <param name="inTunnelNumber">inTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">outTunnelNumber to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void InTunnelChanged(int inTunnelNumber, int outTunnelNumber){
        if (this.isServer)
        {
            ClientInTunnelChanged(inTunnelNumber,outTunnelNumber);
        }
        else {
            if (!isLocalPlayer) return; 
            ServerInTunnelChanged(inTunnelNumber, outTunnelNumber);
        }
    }

    /// <summary>
    /// Command to synchronize the In-Tunnels from server-side
    /// </summary>
    /// <param name="inTunnelNumber">inTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">outTunnelNumber to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerInTunnelChanged(int inTunnelNumber, int outTunnelNumber){
        InTunnelChanged(inTunnelNumber, outTunnelNumber);
    }

    /// <summary>
    /// Command to synchronize the In-Tunnels from client-side
    /// </summary>
    /// <param name="inTunnelNumber">inTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">outTunnelNumber to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientInTunnelChanged(int inTunnelNumber, int outTunnelNumber){
        FindObjectOfType<MissionProver>().SetInTunnelValues(inTunnelNumber, outTunnelNumber);
    }
    
    //Remove Out-Tunnel
    
    /// <summary>
    /// Method to synchronize Removing Out-Tunnels
    /// </summary>
    /// <param name="outTunnelNumber">outTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void OutTunnelChanged(int outTunnelNumber){
        if (this.isServer)
        {
            ClientOutTunnelChanged(outTunnelNumber);
        }
        else {
            if (!isLocalPlayer) return; 
            ServerOutTunnelChanged(outTunnelNumber);
        }
    }

    /// <summary>
    /// Command to synchronize the Removing from Out-Tunnels from server-side
    /// </summary>
    /// <param name="outTunnelNumber">outTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerOutTunnelChanged(int outTunnelNumber){
        OutTunnelChanged(outTunnelNumber);
    }

    /// <summary>
    /// Command to synchronize the Removing from Out-Tunnels from client-side
    /// </summary>
    /// <param name="outTunnelNumber">outTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientOutTunnelChanged(int outTunnelNumber){
        FindObjectOfType<MissionProver>().SetRemovedOutTunnel(outTunnelNumber);
    }

}


