﻿using UnityEngine;
using Mirror;
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        public Transform leftRacketSpawn;
        public Transform rightRacketSpawn;
        GameObject ball;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position
           
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // destroy ball
           
        }
    }

