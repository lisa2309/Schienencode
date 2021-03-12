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

[AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public override void OnClientConnect(NetworkConnection conn){ 
            ClientScene.AddPlayer(conn);
        }
    }

