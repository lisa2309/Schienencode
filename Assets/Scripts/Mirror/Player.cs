using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;
using Database;
using SimpleJSON;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author Ahmed L'harrak
public class Player : NetworkBehaviour
{
    /// <summary>
    /// Straight prefab gameobject
    /// </summary>
    public GameObject straightRail;

    /// <summary>
    /// Curve rigth prefab gameobject
    /// </summary>
    public GameObject curveR0;

    /// <summary>
    /// Curve left prefab gameobject
    /// </summary>
    public GameObject curveL0;

    /// <summary>
    /// Tunnel in  prefab gameobject
    /// </summary>
    public GameObject tunnelIn;

    /// <summary>
    /// Tunnel out prefab gameobject
    /// </summary>
    public GameObject tunnelOut;

    /// <summary>
    /// Trainstation prefab gameobject
    /// </summary>
    public GameObject station;

    /// <summary>
    /// Switch left 0 prefab gameobject
    /// </summary>
    public GameObject switchL0;

    /// <summary>
    /// Switch left 1 prefab gameobject
    /// </summary>
    public GameObject switchL1;

    /// <summary>
    /// Switch rigth 0 prefab gameobject
    /// </summary>
    public GameObject switchR0;

    /// <summary>
    /// Switch rigth 1 prefab gameobject
    /// </summary>
    public GameObject switchR1;

    /// <summary>
    /// Rail start prefab gameobject
    /// </summary>
    public GameObject railStart;

    /// <summary>
    /// Rail end prefab gameobject
    /// </summary>
    public GameObject railEnd;
    
    /// <summary>
    /// Gameobjict variable to save instantiate prefabe
    /// </summary>
    private GameObject prefabToInstant;

    /// <summary>
    /// Script objectplacer
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

    /// <summary>
    /// 
    /// </summary>
    private MissionProver missionprv;

    /// <summary>
    /// Initialisation of variables dbcon and objectplacer
    /// and then assign current player object to player object in dbcon and objectplacer scripts
    /// </summary>
    /// @author Ahmed L'harrak
    void Start() {
    prefabToInstant = straightRail;
    player = this;
    if(this.isLocalPlayer)
    {
        
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
        RegisterAll();
    }
    camera = FindObjectOfType<CameraMovement>();
    camera.MaxFieldCameraView();
    }

    /// <summary>
    /// This method will called when a player click on own start train button 
    /// </summary>
    /// @author Ahmed L'harrak
    void CmdPressButton()
    {
        if (this.isServer)
        {
           StartTrain();
        }
    }

    /// <summary>
    /// Start the train only from server side 
    /// </summary>
    /// @author Ahmed L'harrak
    public void StartTrain()
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
    /// Change the scene for example when a level finished than it will new scen laoded
    /// </summary>
    /// <param name="map"></param>
    /// @author Ahmed L'harrak
    public void NewScene(String map)
    {
        FindObjectOfType<MyNetworkManager>().ServerChangeScene(map); 
    }
    
    /// <summary>
    /// 
    /// Variables:
    /// cloneObj: The instatiat game object
    /// @author Ahmed L'harrak
    /// lokking for witch prefabe will be instiantiate  seqarch by name if ther exist then prefabtoinstant will be the correspend gameobject of this name
    /// then it will be this prefabe created for all players in this Game
    /// </summary>
    /// <param name="prefabname"> the name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> the position (cordinaten ) where the prefabe shoulde created </param>
    /// <param name="rotate"> with witch rotation should this prefab creatde</param>
    /// @author Ahmed L'harrak
    [Command]
    void insprefab(string prefabto, Vector3 finalPosition, float rotate, bool buildByOP)
    {
        switch (prefabto){
            case RailName.RailStraight:
                prefabToInstant = straightRail;
                break;
            case RailName.RailCurveLeft: 
                prefabToInstant = curveL0;
                break;
            case RailName.RailCurveRight:
                prefabToInstant = curveR0;
                break;
            case RailName.TunnelIn:
                prefabToInstant = tunnelIn;
                break;
            case RailName.TunnelOut:
                prefabToInstant = tunnelOut;
                break;
            case RailName.RailCollectRight:
                prefabToInstant = switchL0;
                break;
            case RailName.RailCollectLeft:
                prefabToInstant = switchL1;
                break;
            case RailName.RailSwitchLeft:
                prefabToInstant = switchR0;
                break;
            case RailName.RailSwitchRight:
                prefabToInstant = switchR1;
                break;
            case RailName.RailStart:
                prefabToInstant = railStart;
                break;
            case RailName.RailEnd:
                prefabToInstant = railEnd;
                break;
            case RailName.TrainStation:
                prefabToInstant = station;
                break;
            default:
                prefabToInstant = straightRail;
                break;
        }
        GameObject cloneObj = Instantiate(prefabToInstant, finalPosition, Quaternion.Euler(0, rotate, 0));
        cloneObj.name = prefabToInstant.name;
        if (!buildByOP)
        {
            Destroy(cloneObj.GetComponent<DeleteRail>());
            if (cloneObj.name.Equals("TunnelIn")) cloneObj.GetComponent<InTunnelScript>().buildOnDB=true;
        }
        NetworkServer.Spawn(cloneObj,this.connectionToClient);

        if (buildByOP)
        {
            Debug.Log("Should only be built by ObjectPlacer");
            if (cloneObj.name.Equals("TunnelOut"))
            {
                InitOutTunnelOnClient(cloneObj);
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
    }

    /// <summary>
    /// Initiate values of created Switch
    /// </summary>
    /// <param name="obj"> Gameobject of the Switch </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void RegisterSwitchOnClient(GameObject obj)
    {
        Debug.Log("++++++++++Register Switch on client++++++");
        SwitchScript s = obj.GetComponent<SwitchScript>();
        if (!s.IsInited) s.Register();
    }
    
    /// <summary>
    /// Initiate values of created Switch
    /// </summary>
    /// <param name="obj"> Gameobject of the Switch </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void RegisterInTunnelOnClient(GameObject obj)
    {
        InTunnelScript t= obj.GetComponent<InTunnelScript>();
        if (!t.IsInited) t.Register();
    }
    
    /// <summary>
    /// Initiate values of created OutTunnel
    /// </summary>
    /// <param name="obj"> Gameobject of the OutTunnel </param>
    /// @author Ahmed L'harrak & Bastian Badde
    [ClientRpc]
    public void InitOutTunnelOnClient(GameObject obj)
    {
        Debug.Log("Init out on client+++++++++");
        OutTunnelScript t= obj.GetComponent<OutTunnelScript>();
        if (!t.IsInited) t.InitOutTunnel();
    }

    /// <summary>
    /// This function is a public function to be called from other scripts like opbjectplacer it will then create the chosen prefabe in the chosen cordinaten and rotation value
    /// </summary>
    /// <param name="prefabname"> The name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> The position (cordinaten ) where the prefabe shoulde created </param>
    /// <param name="rotate"> With witch rotation should this prefab creatde</param>
    /// @author Ahmed L'harrak
    public void Call(string prefabname, Vector3 finalPosition, float rotate, bool buildOnOP)
    {
        if (!isLocalPlayer) return;   
        insprefab(prefabname, finalPosition, rotate, buildOnOP);
    }

    /// <summary>
    /// Called by the Player
    /// Call the function cmdDestroyobject()
    /// </summary>
    /// <param name="obj">The gameobject to be deleted </param>
    /// @author Ahmed L'harrak
    [Client]
    public void TellServerToDestroyObject(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    /// <summary>
    /// Executed only on the server
    /// If this gameobject exict it will be deleted from this Game for all Players
    /// </summary>
    /// <param name="obj">Gameobject to be destroyed </param>
    /// @author Ahmed L'harrak
    [Command]
    private void CmdDestroyObject(GameObject obj)
    {
        if(!obj) return;
        NetworkServer.Destroy(obj);
    }
    
    /// <summary>
    /// Function that handles game results.
    /// Currently lets Host automatically win.
    /// </summary>
    /// TODO: Implement connection to actual game results
    /// @author Christopher-Marcel Klein
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
    /// @author Christopher-Marcel Klein
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
    /// @author Christopher-Marcel Klein
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
    
    //Remove Out-Tunnel
    
    /// <summary>
    /// Method to synchronize Tunnel and Switch-registration
    /// </summary>
    /// <param name="outTunnelNumber">outTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak und Bastian Badde
    public void RegisterAll(){
        if (this.isServer)
        {
            RegisterAllOnServerOnClient();
        }
        else {
            if (!isLocalPlayer) return; 
            RegisterAllOnServer();
        }
    }

    /// <summary>
    /// Command to synchronize Tunnel and Switch-registration on server-side
    /// </summary>
    /// @author Ahmed L'Harrak und Bastian Badde
    [Command]
    void RegisterAllOnServer(){
        RegisterAll();
    }

    /// <summary>
    /// Command to synchronize Tunnel and Switch-registration on client side
    /// </summary>
    /// @author Ahmed L'Harrak und Bastian Badde
    [ClientRpc]
    public void RegisterAllOnServerOnClient(){
        //Debug.Log("++++++++++Register All on client++++++");
        foreach (var tunnel in FindObjectsOfType<OutTunnelScript>())
        {
            if (!tunnel.IsInited) tunnel.InitOutTunnel();
        }
        foreach (var tunnel in FindObjectsOfType<InTunnelScript>())
        {
            if (tunnel.IsInited) continue;
            tunnel.Register();
            tunnel.buildOnDB = true;
        }
        foreach (var switchScript in FindObjectsOfType<SwitchScript>())
        {
            if(!switchScript.IsInited) switchScript.Register();
        }
        foreach (var station in FindObjectsOfType<StationScript>())
        {
            if(!station.isInited) station.Register();
        }
    }

}


