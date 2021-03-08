using System;
using System.Collections;
 using UnityEngine.Networking;
using UnityEngine;
using Mirror;
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            

            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, player);
            


        }

                public override void OnServerDisconnect(NetworkConnection conn)
        {
            Debug.Log("destroy server");

            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }

        public override void OnClientConnect(NetworkConnection conn){
           
            ClientScene.AddPlayer(conn);

        }
    }

