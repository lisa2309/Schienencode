using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class MissionProverTest
    {
        [UnityTest]
        public IEnumerator TestStationRegistration()
        {
            GameObject gameObject = new GameObject("TestTrainRegistration");
            MissionProver prover = gameObject.AddComponent<MissionProver>();
            prover.StartManual();
            prover.RegisterNewStation(gameObject.AddComponent<StationScript>());
            prover.RegisterNewStation(gameObject.AddComponent<StationScript>());
            int thirdStationNumber= prover.RegisterNewStation(gameObject.AddComponent<StationScript>());
            
            Assert.AreEqual(2,thirdStationNumber);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestOutTunnelRegistration()
        {
            GameObject gameObject = new GameObject("TestOTRegistration");
            MissionProver prover = gameObject.AddComponent<MissionProver>();
            OutTunnelScript ot = gameObject.AddComponent<OutTunnelScript>();
            ot.InitOutTunnel();
            ot.InitOutTunnel();
            Assert.AreEqual(1,prover.givenTunnelNumbers[1]);
            
            ot.InitOutTunnel();
            prover.RemoveOutTunnel(1);
            Assert.AreEqual(2,prover.givenTunnelNumbers[1]);
            
            yield return null;
        }
        
        
    }
}