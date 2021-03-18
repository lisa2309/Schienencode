using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Database;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Loads the next scene
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Database object of the class DatabaseConnector
    /// </summary>
    private DatabaseConnector database;
    
    /// <summary>
    /// Number of maximum scene that exist
    /// </summary>
    private int maxScene;
    
    /// <summary>
    /// Number of actuel Scene
    /// </summary>
    private int actualSceneIndex;

    /// <summary>
    /// Object of the player
    /// </summary>
    private Player player;

    /// <summary>
    /// Loads the next scene after the current scene
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    public void NextScene()
    {
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (actualSceneIndex < (maxScene-1))
        {
            player = Player.player;
            player.NewScene(SceneUtility.GetScenePathByBuildIndex(actualSceneIndex + 1));
        }     
    }

    /// <summary>
    /// Search for the object of the type DatabaseConnector.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    void Start()
    { 
        database = FindObjectOfType<DatabaseConnector>();
        maxScene = 6;
    }
}
