using System.Collections;
using System.Threading;
using Database;
using DefaultNamespace;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine;

namespace Tests
{
    public class DatabaseTest
    {

        [UnityTest]
        public IEnumerator TestDBCommunication()
        {
            GameObject gameObject = new GameObject("TestDB");
            DatabaseConnector dbc = gameObject.AddComponent<DatabaseConnector>();
            dbc.boardName = "testName";
            dbc.PostToDatabase(new Board("Board:", "Mission:"));
            Thread.Sleep(50);
            dbc.RetrieveFromDatabase();
            Thread.Sleep(200);
            
            LogAssert.Expect(LogType.Log, "BoardData retrieved");
            yield return null;
        }
    }
}