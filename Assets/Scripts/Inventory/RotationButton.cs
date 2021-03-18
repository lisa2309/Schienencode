using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// The rotation will be controlled here by plus minus 90 degrees
/// </summary>
/// @author Ahmed L'harrak
public class RotationButton : MonoBehaviour
{
    /// <summary>
    /// Name of the current created prefab
    /// </summary>
    public string prefab;

    /// <summary>
    /// Image of current created prefab
    /// </summary>
    public GameObject imageButton;


    /// <summary>
    /// The button for rotation is stored in a prefab.
    /// This is then used to find out the name of the prefab and store it in a global variable.
    /// This is then accessible from everywhere. In addition, the rotation of the prefab is set to zero
    /// parentPanel: GameObject of the panel by the rotation button 
    /// </summary>
    /// @author Ahmed L'harrak
    void Awake()
    {   
        GameObject parentPanel = this.transform.parent.parent.Find("createpanel").gameObject;
        prefab = parentPanel.transform.Find("create").gameObject.GetComponent<CreatePrefab>().currentPrefab.name;
        PlayerPrefs.SetFloat(prefab, 0f);
    }

    /// <summary>
    /// If the rotation right button is clicked, this method search for the rotation of the prefab and set the rotation plus 90 degrees.
    /// If the rotation is 360 or -360 degrees, it will be set to 0.
    /// rotate: Rotation of the prefab
    /// </summary>
    /// @author Ahmed L'harrak
    public void RotatePrefabRight()
    {
        float rotate = PlayerPrefs.GetFloat(prefab);
        if (rotate == 360 || rotate == -360)
        {
            rotate = 0f;
        }
        PlayerPrefs.SetFloat(prefab, rotate + 90);
        imageButton.transform.rotation = Quaternion.Euler(0, 0, -(rotate + 90));
    }
    
    /// <summary>
    /// If the rotation left button is clicked, this method search for the rotation of the prefab and set the rotation minus 90 degrees.
    /// If the rotation is 360 or -360 degrees, it will be set to 0.
    /// rotate: Rotation of the prefab
    /// <summary>
    /// @author Ahmed L'harrak
    public void RotatePrefabLeft()
    {
        float rotate = PlayerPrefs.GetFloat(prefab);
        if (rotate == 360 || rotate == -360)
        {
            rotate = 0f;
        }
        PlayerPrefs.SetFloat(prefab, rotate - 90);
        imageButton.transform.rotation = Quaternion.Euler(0, 0, -(rotate - 90));
    }
}
