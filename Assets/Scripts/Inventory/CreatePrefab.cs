using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// The current gameobject will marked as nexte instansiated gameobject
/// </summary>
/// @author Ahmed L'harrak
public class CreatePrefab : MonoBehaviour
{
    /// <summary>
    /// That is the prefab that will be created
    /// </summary>
    public GameObject currentPrefab;

    /// <summary>
    /// The rotation of the created prefabs
    /// </summary>
    private float rotate;

    /// <summary>
    /// Object of the script ObjectPlacer
    /// </summary>
    private ObjectPlacer objectPlacer;

    /// <summary>
    /// Call the public gameobject  variable of Objectplacer class and change it to the current gameobject 
    /// so that, when the player creats an object. Then this will be the current object of this panel window
    /// ray: Mouse position in the scene
    /// </summary>
    /// @author Ahmed L'harrak
    public void CreateRail()
    {
        rotate = PlayerPrefs.GetFloat(currentPrefab.name);
        Ray ray = new Ray(Camera.main.transform.position, Vector3.forward);
        objectPlacer = FindObjectOfType<ObjectPlacer>();
        objectPlacer.prefabToInstant = currentPrefab;
        objectPlacer.rotate = rotate;
        Debug.Log(rotate);
        objectPlacer.isPreviewOn = true;
        this.transform.parent.parent.gameObject.SetActive(false);
    }

}
