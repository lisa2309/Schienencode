using System;
using System.Collections;
 using UnityEngine.Networking;
using UnityEngine;
using Mirror;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Custom NetworkManager that simply assigns the correct positions when
/// spawning players. The built in RoundRobin spawn method wouldn't work after
/// someone reconnects (both players would be on the same side).
/// </summary>
/// @author Ahmed L'harrak
[AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        /// <summary>
        /// When a new player connected to the game the server create a player prefab for the new player 
        /// </summary>
        /// <param name="connection">The Connection between server and client</param>
        public override void OnServerAddPlayer(NetworkConnection connection)
        {
            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(connection, player);
        }

        /// <summary>
        /// This method will be called when the server close the connection 
        /// </summary>
        /// <param name="connection">The Connection between server and client</param>
         /// @author Ahmed L'harrak
        public override void OnServerDisconnect(NetworkConnection connection)
        {
            base.OnServerDisconnect(connection);
        }

        /// <summary>
        /// When a new client connect to the game then this function calls the call (ClientScene.AddPlayer(connection);) that mean it call OnServerAddPlayer.
        /// Then the server create the player prefab for this client in the game 
        /// </summary>
        /// <param name="connection">The Connection between server and client</param>
        /// @author Ahmed L'harrak
        public override void OnClientConnect(NetworkConnection connection){ 
            ClientScene.AddPlayer(connection);
        }
    }

