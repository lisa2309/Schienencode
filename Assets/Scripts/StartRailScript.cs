using System;
using Database;
using UnityEngine;


public class StartRailScript : MonoBehaviour
{
    
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private DatabaseConnector _dbcn;
    
    void Start()
    {
        _dbcn = FindObjectOfType<DatabaseConnector>();
        _dbcn.RetrieveFromDatabaseForMission();
    }
}
