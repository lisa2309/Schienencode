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
    /// database object of the class  DatabaseConnector.
    /// </summary>
    private DatabaseConnector database;
    /// <summary>
    /// Number of maximum scene that exist.
    /// </summary>
    private const int MAXSCENES = 7;

    /// <summary>
    /// Loads the next scene after the current scene.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    public void NextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        char[] sceneName = scene.name.ToCharArray();
        char number = sceneName[3];
        int sceneNumber = ((int)char.GetNumericValue(number)) + 1;
        if (sceneNumber < MAXSCENES)
        {
            SceneManager.LoadScene("Map" + sceneNumber.ToString());
            //database.RetrieveFromDatabase();
        }     
    }

    /// <summary>
    /// Search for the object of the type DatabaseConnector.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller
    void Start()
    { 
        database = FindObjectOfType<DatabaseConnector>();
    }


}
