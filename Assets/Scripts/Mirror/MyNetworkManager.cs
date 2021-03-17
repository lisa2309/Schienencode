using System;
using System.Collections;
 using UnityEngine.Networking;
using UnityEngine;
using Mirror;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Custom NetworkManager that simply assigns the correct racket positions when
/// spawning players. The built in RoundRobin spawn method wouldn't work after
/// someone reconnects (both players would be on the same side).
/// </summary>
 /// @author Ahmed L'harrak
[AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        /// <summary>
        /// when a new player connected to the game will the server creat a player prefab  to this new player 
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        /// <summary>
        /// this method will called when the server close the connection 
        /// </summary>
        /// <param name="conn"></param>
         /// @author Ahmed L'harrak
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
        }

        /// <summary>
        /// when a new client connected to the game then will this function called the call also  ClientScene.AddPlayer(conn); that s mean it call OnServerAddPlayer
        /// than the server creat the player prefab for this client in the game 
        /// </summary>
        /// <param name="conn"></param>
        /// @author Ahmed L'harrak
        public override void OnClientConnect(NetworkConnection conn){ 
            ClientScene.AddPlayer(conn);
        }
    }

