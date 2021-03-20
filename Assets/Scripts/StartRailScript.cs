using System;
using Database;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Script to load the missionstring from the database
/// </summary>
/// @author Bastian Badde
public class StartRailScript : MonoBehaviour
{
    
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private DatabaseConnector databaseConnector;
    
    /// <summary>
    ///  Start method to load the missionstring from the database when the startrail prefab is instatiated
    /// </summary>
    /// @author Bastain Badde
    void Start()
    {
        databaseConnector = FindObjectOfType<DatabaseConnector>();
        databaseConnector.RetrieveFromDatabaseForMission();
    }
}
