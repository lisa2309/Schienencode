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
/// Each player has his own player script
/// </summary>
/// @author Ahmed L'harrak & Bastian Badde 
public class Player : NetworkBehaviour
{
    
    /// <summary>
    /// bool which is true if map is loaded from database
    /// </summary>
    private bool databaseIsLoaded;
    
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
    /// Object of the databaseconnector
    /// </summary>
    private DatabaseConnector databaseConnector;

    /// <summary>
    /// Object of this class
    /// </summary>
    public static Player player;

    /// <summary>
    /// Object of CameraMovement
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
    /// Object of the missionprover
    /// </summary>
    private MissionProver missionProver;

    /// <summary>
    /// Initialisation of variables dbcon and objectplacer
    /// and then assign current player object to player object in dbcon and objectplacer scripts.
	/// Additionally retrieves all information from existing JSON-Files.
    /// </summary>
    /// @author Ahmed L'harrak
	/// @source SWTP-Framework (JSON retrieval)
    void Start() {
    prefabToInstant = straightRail;
    player = this;
    if(this.isLocalPlayer)
    {
        databaseConnector = FindObjectOfType<DatabaseConnector>();
        databaseConnector.player = this;

        missionProver = FindObjectOfType<MissionProver>();
        missionProver.player = this;
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
        databaseIsLoaded = false;
    }
    camera = FindObjectOfType<CameraMovement>();
    camera.MaxFieldCameraView();
    }
    
    /// <summary>
    /// This method will load from database after 3 seconds delay until the client is ready. 
    /// </summary>
    /// @author Ahmed L'harrak
    IEnumerator LoadFromDbDelayed()
    {
        Debug.Log("Load from DB!!");
        yield return new WaitForSeconds(2);
        databaseConnector.RetrieveFromDatabase();
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
    /// Update Method which is permanently executed and loads from DB once the number of connections is more than one.
    /// </summary>
    /// <returns></returns>
    /// @author Ahmed L'harrak
    void Update(){

        if (this.isServer)
        {
            //Debug.Log("db_is_loaded"+ db_is_loaded);
            if( NetworkServer.connections.Count > 1 && databaseIsLoaded==false){
                databaseIsLoaded=true;
                Debug.Log("loading ....");
                StartCoroutine(LoadFromDbDelayed());
            }
        }
    }
    

    /// <summary>
    /// Change the scene for example when a level finished. Then there will be a new scene laoded
    /// </summary>
    /// <param name="map">String from the map</param>
    /// @author Ahmed L'harrak
    public void NewScene(String map)
    {
        if (this.isServer)
        {
            FindObjectOfType<MyNetworkManager>().ServerChangeScene(map); 
        }
    }
    
    /// <summary>
    /// Looking for which prefabe will be instiantiate. Search by name if their exist then prefabtoinstant will be the correspend gameobject of this name
    /// Then it will be this prefabe created for all players in this game
    /// cloneObject: The instatiat game object
    /// </summary>
    /// <param name="prefabName"> The name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> The position (cordinaten ) where the prefabe shoulde created </param>
    /// <param name="rotate"> Which rotation should this prefab created</param>
    /// <param name="buildByObjectPlacer">Bool to check if a rail is set by the ObjectPlacer</param>
    /// @author Ahmed L'harrak
    [Command]
    void InstatiatePrefab(string prefabName, Vector3 finalPosition, float rotate, bool buildByObjectPlacer)
    {
        switch (prefabName){
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
        GameObject cloneObject = Instantiate(prefabToInstant, finalPosition, Quaternion.Euler(0, rotate, 0));
        cloneObject.name = prefabToInstant.name;
        if (!buildByObjectPlacer)
        {
            Destroy(cloneObject.GetComponent<DeleteRail>());
            if (cloneObject.name.Equals("TunnelIn")) cloneObject.GetComponent<InTunnelScript>().buildOnDatabase=true;
        }
        NetworkServer.Spawn(cloneObject,this.connectionToClient);

        if (cloneObject.name.Equals("TunnelOut"))
        {
            InitOutTunnelOnClient(cloneObject);
        }
        if (cloneObject.name.Equals("SwitchR1Final") || cloneObject.name.Equals("SwitchR0Final"))
        {
            RegisterSwitchOnClient(cloneObject);
        }
        if (cloneObject.name.Equals("TunnelIn")) 
        { 
            RegisterInTunnelOnClient(cloneObject);
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
    /// <param name="prefabName"> The name of prefabe to be intantiate for alle Player in this Game </param>
    /// <param name="finalPosition"> The position (cordinaten) where the prefabe should created </param>
    /// <param name="rotate"> Which rotation should this prefab creatde</param>
    /// @author Ahmed L'harrak
    public void Call(string prefabName, Vector3 finalPosition, float rotate, bool buildOnOP)
    {
        if (!isLocalPlayer) return;   
        InstatiatePrefab(prefabName, finalPosition, rotate, buildOnOP);
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
    /// If this gameobject exist it will be deleted from this Game for all Players
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
    /// @source SWTP-Framework
	/// Modified by: Christopher-Marcel Klein
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
    /// @source SWTP-Framework
    /// Modified by: Christopher-Marcel Klein
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
    /// @source SWTP-Framework
	/// Modified by: Christopher-Marcel Klein
    [TargetRpc]
    public void TargetReceiveHostName(NetworkConnection connection, string hostName)
    {
        GameServer.Instance.HandleGameResults(2,hostName);
    }
    
    /// <summary>
    /// Method to synchronize the station cargos
    /// </summary>
    /// <param name="currentStation">StationNumber of the relevant StationScript</param>
    /// <param name="value">Cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void CargoChanged(int currentStation, int value){
        if (this.isServer)
        {
            ClientCargoChanged(currentStation,value);
        }
        else {
            if (!isLocalPlayer) return; 
            ServerCargoChanged(currentStation,value);
        }
    }

    /// <summary>
    /// Command to synchronize the station cargos from server-side
    /// </summary>
    /// <param name="currentStation">StationNumber of the relevant StationScript</param>
    /// <param name="value">Cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerCargoChanged(int currentStation, int value){
        CargoChanged(currentStation,value);
    }

    /// <summary>
    /// Command to synchronize the station cargos from client-side
    /// </summary>
    /// <param name="currentStation">StationNumber of the relevant StationScript</param>
    /// <param name="value">Cargo-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientCargoChanged(int currentStation, int value){
        FindObjectOfType<MissionProver>().SetStationCargo(currentStation, value);
    }

    /// <summary>
    /// Method to synchronize the switch-values
    /// </summary>
    /// <param name="switchNumber">The switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">The stationNumber whose cargo to compare with</param>
    /// <param name="compare">The decoded compare value</param>
    /// <param name="value">The value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    public void SwitchValuesChanged(int switchNumber, int cargo,int compare, int value){
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
    /// <param name="switchNumber">The switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">The stationNumber whose cargo to compare with</param>
    /// <param name="compare">The decoded compare value</param>
    /// <param name="value">The value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerSwitchValuesChanged(int switchNumber, int cargo,int compare, int value){
        SwitchValuesChanged(switchNumber, cargo, compare,value);
    }

    /// <summary>
    /// Command to synchronize the switch-values from client-side
    /// </summary>
    /// <param name="switchNumber">The switchNumber of the relevant SwicthScript</param>
    /// <param name="cargo">The stationNumber whose cargo to compare with</param>
    /// <param name="compare">The decoded compare value</param>
    /// <param name="value">The value to compare with</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientSwitchValuesChanged(int switchNumber, int cargo,int compare, int value){
        FindObjectOfType<MissionProver>().SetSwitchValues(switchNumber, cargo, compare,value);
    }
    
    /// <summary>
    /// Method to synchronize the switch-modes
    /// </summary>
    /// <param name="switchNumber">SwitchNumber of the relevant SwitchScript</param>
    /// <param name="mode">Mode-value to set</param>
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
    /// <param name="switchNumber">SwitchNumber of the relevant SwitchScript</param>
    /// <param name="mode">Mode-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerSwitchModeChanged(int switchNumber, int mode){
        SwitchModeChanged(switchNumber, mode);
    }

    /// <summary>
    /// Command to synchronize the switch-modes from client-side
    /// </summary>
    /// <param name="switchNumber">SwitchNumber of the relevant SwitchScript</param>
    /// <param name="mode">Mode-value to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientSwitchModeChanged(int switchNumber, int mode){
        FindObjectOfType<MissionProver>().SetSwitchMode(switchNumber, mode);
    }

    /// <summary>
    /// Method to synchronize the In-Tunnels
    /// </summary>
    /// <param name="inTunnelNumber">InTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">OutTunnelNumber to set</param>
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
    /// <param name="inTunnelNumber">InTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">OutTunnelNumber to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerInTunnelChanged(int inTunnelNumber, int outTunnelNumber){
        InTunnelChanged(inTunnelNumber, outTunnelNumber);
    }

    /// <summary>
    /// Command to synchronize the In-Tunnels from client-side
    /// </summary>
    /// <param name="inTunnelNumber">InTunnelNumber of the relevant InTunnelScript</param>
    /// <param name="outTunnelNumber">OutTunnelNumber to set</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientInTunnelChanged(int inTunnelNumber, int outTunnelNumber){
        FindObjectOfType<MissionProver>().SetInTunnelValues(inTunnelNumber, outTunnelNumber);
    }

    /// <summary>
    /// Method to synchronize Removing Out-Tunnels
    /// </summary>
    /// <param name="outTunnelNumber">OutTunnelNumber to remove</param>
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
    /// <param name="outTunnelNumber">OutTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [Command]
    void ServerOutTunnelChanged(int outTunnelNumber){
        OutTunnelChanged(outTunnelNumber);
    }

    /// <summary>
    /// Command to synchronize the Removing from Out-Tunnels from client-side
    /// </summary>
    /// <param name="outTunnelNumber">OutTunnelNumber to remove</param>
    /// @author Ahmed L'Harrak & Bastian Badde
    [ClientRpc]
    public void ClientOutTunnelChanged(int outTunnelNumber){
        FindObjectOfType<MissionProver>().SetRemovedOutTunnel(outTunnelNumber);
    }
    
    /// <summary>
    /// Registers all relevant Scripts of already loaded Gameobjects
    /// </summary>
    /// @author Ahmed L'Harrak & Bastian Badde
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
    /// Command to register all relevant scripts of already loaded Gameobjects on server-side
    /// </summary>
    /// @author Ahmed L'Harrak und Bastian Badde
    [Command]
    void RegisterAllOnServer(){
        RegisterAll();
    }

    /// <summary>
    /// Command to register all relevant scripts of already loaded Gameobjects on server-side and on client-side
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
            tunnel.buildOnDatabase = true;
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


