using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class OverRailTest
    {
        [UnityTest]
        public IEnumerator TestCurveL0Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "CurveL0Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters); 
            
            //Assert
            Assert.AreEqual("Curve", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCurveR0Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "CurveR0Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Curve", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestRailEnd()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "RailEnd";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("RailE", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestRailStart()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "RailStart";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("RailS", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestStraight270Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "Straight270Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Strai", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestSwitchL0Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "SwitchL0Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Switc", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestSwitchL1Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "SwitchL1Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Switc", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestSwitchR0Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "SwitchR0Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Switc", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestSwitchR1Final()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "SwitchR1Final";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Switc", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestTunnelIn()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "TunnelIn";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Tunne", result);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestTunnelOut()
        {
            
            // ASSIGN
            GameObject gameObject = new GameObject("TestOverRail");
            OverRail or = gameObject.AddComponent<OverRail>();
            
            //Act
            string objectName = "TunnelOut";
            char[] objectLetters = objectName.ToCharArray();
            string result = or.ConvertCharArrayToString(5, objectLetters);
            
            //Assert
            Assert.AreEqual("Tunne", result);
            yield return null;
        }
    }
}
