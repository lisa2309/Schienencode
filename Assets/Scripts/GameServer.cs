using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Mirror;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class represents a game server.
/// The server starts automatically and loads the player prefab into the online scene.
/// </summary>
/// Quelle: SWTP Framework
/// Modified by: Ronja Haas & Anna-Lisa Müller
public class GameServer : NetworkManager
{
    /// <summary>
    /// Location of the file that holds playerInfo of
    /// the player that started the game.
    /// NOTE: This is not important for development.
    /// </summary>
    private const string fileName = "Info.txt";

    /// <summary>
    /// Defines how long the game server will try to connect the client before quitting the application.
    /// </summary>
    [SerializeField] private float disconnectWaitTime;

    /// <summary>
    /// Stores the time until disconnect.
    /// </summary>
    private float disconnectTimer;

    /// <summary>
    /// Stores information about the player.
    /// </summary>
    private JSONNode playerInfos;

    /// <summary>
    /// Stores information about the game.
    /// </summary>
    private JSONNode gameInfos;

    /// <summary>
    /// Stores if the local player is the host.
    /// </summary>
    private bool isHost;

    /// <summary>
    /// Signals if the game can be quit.
    /// </summary>
    private bool readyToQuit;

    /// <summary>
    /// The number of rounds played is stored in here
    /// </summary>
    private int gameRounds;

    /// <summary>
    /// 
    /// </summary>
    public JSONNode PlayerInfos => playerInfos;

    /// <summary>
    /// 
    /// </summary>
    public JSONNode GameInfos => gameInfos;

    /// <summary>
    /// 
    /// </summary>
    public static GameServer Instance => (GameServer) singleton;

    /// <summary>
    /// Start is called before the first frame update.
    /// The server loads the player info of the local player, if:
    /// - the player is the host -> start server as host
    /// - the player is the client -> join server as client
    /// NOTE: This is not important for development.
    /// </summary>
    void Start()
    {
        base.Start();

        readyToQuit = false;
        isHost = false;

        LoadPlayerInfoMockup();     // <- FOR DEVELOPMENT

        //LoadPlayerInfo();             // <- FOR RELEASE

        disconnectTimer = disconnectWaitTime;

        if (isHost)
        {
            StartHost();
        }
        Debug.Log("TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEST");
    }

    /// <summary>
    /// Update is called once per frame.
    /// Checks if the local player is a client and not connected.
    /// If so the client tries to connect and the disconnect timer is updated.
    /// </summary>
    private void Update()
    {
        // Try connecting if not host
        if (!isHost && !NetworkClient.isConnected)
        {
            disconnectTimer -= Time.deltaTime;

            if (disconnectTimer <= 0f)
            {
                Application.Quit();
            }

            StartClient();
        }

        if (isHost && NetworkServer.connections.Count < int.Parse(gameInfos["playerAmount"].Value))
        {
            disconnectTimer -= Time.deltaTime;

            if (disconnectTimer <= 0f)
            {
                Application.Quit();
            }
        }

        // Try quitting
        if (readyToQuit)
        {
            if (isHost)
            {
                // Host waits for all players to quit first
                if (NetworkServer.connections.Count <= 1)
                {
                    Application.Quit();
                }
            }
            else
            {
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// Loads player info from file.
    /// NOTE: This is not important for development.
    /// </summary>
    private void LoadPlayerInfo()
    {
        // Read file
        string filePath = "";
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.Windows:
                filePath = Application.dataPath + @"\..\..\..\..\Framework\Windows\" + fileName;
                break;
            case OperatingSystemFamily.Linux:
                filePath = Application.dataPath + @"\..\..\..\..\Framework\Linux\" + fileName;
                break;
            case OperatingSystemFamily.MacOSX:
                filePath = Application.dataPath + @"/../../../../../Framework/MACOSX/" + fileName;
                break;
            default:
                throw new ArgumentException("Illegal OS !");
        }

        Debug.LogError("FilePath: " + filePath);
        StreamReader file = new StreamReader(filePath);
        JSONNode jsonFile = JSON.Parse(file.ReadLine());

        // Load data
        playerInfos = jsonFile["playerInfo"];
        gameInfos = jsonFile["gameInfo"];
        isHost = jsonFile["playerInfo"]["isHost"].AsBool;


        // Close file
        file.Close();
        File.Delete(fileName);
    }

    /// <summary>
    /// Sets mockup player info.
    /// This can be used during development to simulate starting the game from the framework.
    /// NOTE: This is important for development.
    /// </summary>
    private void LoadPlayerInfoMockup()
    {
        isHost = true;

        playerInfos = new JSONObject();
        gameInfos = new JSONObject();

        SetRounds(6);

        playerInfos.Add("name", "Mustermann");
        gameInfos.Add("playerAmount",2);
        gameInfos.Add("rounds", GetRounds());
        gameInfos.Add("board", "Pirates");
    }

    /// <summary>
    /// Sets the number of laps
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    private void SetRounds(int rounds)
    {
        this.gameRounds = rounds;
    }

    /// <summary>
    /// Returns the set number of laps
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    public int GetRounds()
    {
        return this.gameRounds;
    }

    /// <summary>
    /// Handles the results of the game and sets the readyToQuit flag.
    /// Needs the placement of the local player and the name of the winning player
    /// and writes them into a JSON that is used to give rewards in the framework.
    /// IMPORTANT: THIS HAS TO BE CALLED AT THE END OF THE GAME!
    /// </summary>
    /// <param name="localPlayerWinningPlacement">Placement of the local player</param>
    /// <param name="nameOfWinner">Name of the winner</param>
    public void HandleGameResults(int localPlayerWinningPlacement, string nameOfWinner)
    {
        // Create file
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        
        var sr = File.CreateText(fileName);

        // Write file
        JSONObject fileJson = new JSONObject();

        fileJson.Add("placement", localPlayerWinningPlacement);
        fileJson.Add("nameOfWinner", nameOfWinner);

        sr.Write(fileJson.ToString());
        sr.Close();

        readyToQuit = true;
    }
}