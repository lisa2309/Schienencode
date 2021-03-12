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
    private DatabaseConnector dbcn;
    
    /// <summary>
    /// 
    /// </summary>
    /// @author
    void Start()
    {
        dbcn = FindObjectOfType<DatabaseConnector>();
        dbcn.RetrieveFromDatabaseForMission();
    }
}
