using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
// 
/// <summary>
/// the current gameobject will marked as nexte instansiated gameobject
/// </summary>
/// @author Ahmed L'harrak
public class CreatePrefab : MonoBehaviour
{
    /// <summary>
    /// that is the prefab that will craeted
    /// </summary>
    public GameObject currentPrefab;

    /// <summary>
    /// in this varible is the rotation of created prefabs saved
    /// </summary>
    private float rotate;

    /// <summary>
    /// script objectplacer there is for placment this prefabe responsable is
    /// </summary>
    private ObjectPlacer objectPlacer;

    /// <summary>
    /// call the public gameobject  variable of Objectplacer class and change it to current gameobject 
    /// so that whenn the player an Object  created then will be the current object of this panel windows
    /// Variables:
    /// position:
    /// ray:
    /// </summary>
    /// @author Ahmed L'harrak
    public void CreateRail()
    {
        rotate = PlayerPrefs.GetFloat(currentPrefab.name);
        Vector3 position;

        Ray ray = new Ray(Camera.main.transform.position, Vector3.forward);
        position = ray.GetPoint(Camera.main.farClipPlane / 2);
        objectPlacer = FindObjectOfType<ObjectPlacer>();
        objectPlacer.prefabtoinstant = currentPrefab;
        objectPlacer.rotate = rotate;
        Debug.Log(rotate);
        objectPlacer.isPreviewOn = true;
        this.transform.parent.parent.gameObject.SetActive(false);
    }

}
