using System;
using Database;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author 
public class StartRailScript : MonoBehaviour
{
    
    /// <summary>
    /// MissionProver object of the scene for organisation
    /// </summary>
    private DatabaseConnector _dbcn;
    
    /// <summary>
    /// 
    /// </summary>
    /// @author
    void Start()
    {
        _dbcn = FindObjectOfType<DatabaseConnector>();
        _dbcn.RetrieveFromDatabaseForMission();
    }
}
