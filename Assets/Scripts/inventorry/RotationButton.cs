using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// the rotation will be controlled here by plus minus 90 degrees
/// </summary>
/// @author Ahmed L'harrak
public class RotationButton : MonoBehaviour
{
    public string prefab;
    public GameObject imageButton;

    /// <summary>
    /// the rotation will in variable stored there by CreatePrefab class also used 
    /// so the variable name will be the prefab name and the prefab is in  CreatePrefab class declared 
    ///  this function get the name of this Prefab and stored in the  global variable(accessible for all scripts) the Rotation=0  but indexed with ame of this Prefab 
    /// that means that you get this value if you search for this name in PlayerPrefs.getFloat()
    /// </summary>
    /// @author Ahmed L'harrak
    void Awake()
    {
        GameObject parent = this.transform.parent.gameObject;
        prefab = parent.transform.Find("create").gameObject.GetComponent<CreatePrefab>().currentPrefab.name;
        PlayerPrefs.SetFloat(prefab, 0f);
    }

    /// <summary>
    /// if you click on button, this function is called, wich search for the stored Rotation value of the current prefab and stored it in rotate variable
    /// which increments this variable rotate by 90 degrees by evrey call and stored again in the same globale variable
    /// and It is checked whether the variable rotate has the value 360 reached if yes then will to 0 reseted
    /// also the image in zhe Panel Windows will  rotated to easily create a preview for the Player
    /// </summary>
    /// @author Ahmed L'harrak
    public void RotatePrefab()
    {
        float rotate = PlayerPrefs.GetFloat(prefab);
        if (rotate == 360)
        {
            rotate = 0f;
        }
        PlayerPrefs.SetFloat(prefab, rotate + 90);
        imageButton.transform.rotation = Quaternion.Euler(0, 0, -(rotate + 90));
    }
}
