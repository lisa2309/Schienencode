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
            prover.RegisterNewStation();
            prover.RegisterNewStation();
            int thirdStationNumber= prover.RegisterNewStation();
            
            Assert.AreEqual(2,thirdStationNumber);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestOutTunnelRegistration()
        {
            GameObject gameObject = new GameObject("TestOTRegistration");
            OutTunnelScript ot = gameObject.AddComponent<OutTunnelScript>();
            ot.AddOutTunnel();
            ot.AddOutTunnel();
            Assert.AreEqual(1,OutTunnelScript.GivenTunnelNumbers[1]);
            
            ot.AddOutTunnel();
            OutTunnelScript.RemoveOutTunnel(1);
            Assert.AreEqual(2,OutTunnelScript.GivenTunnelNumbers[1]);
            
            yield return null;
        }
        
        
    }
}