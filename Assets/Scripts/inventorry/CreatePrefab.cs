using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author Ahmed L'harrak
public class CreatePrefab : MonoBehaviour
{
    public GameObject currentPrefab;
    private float rotate;
    private ObjectPlacer objectPlacer;

    /// <summary>
    /// 
    /// </summary>
    /// @author Ahmed L'harrak
    public void CreateRail()
    {
        rotate = PlayerPrefs.GetFloat(currentPrefab.name);
        Vector3 position;

        Ray ray = new Ray(Camera.main.transform.position, Vector3.forward);
        position = ray.GetPoint(Camera.main.farClipPlane / 2);
        objectPlacer = FindObjectOfType<ObjectPlacer>();
        objectPlacer.gameObject = currentPrefab;
        objectPlacer.rotate = rotate;
        objectPlacer.isPreviewOn = true;
        this.transform.parent.gameObject.SetActive(false);
    }

}
