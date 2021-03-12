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
    /// <summary>
    /// this is variable to save name of current created prefab
    /// </summary>
    public string prefab;

    /// <summary>
    /// image of current created prefab this image will make a preview show bevore create the prefab
    /// </summary>
    public GameObject imageButton;


    /// <summary>
    /// the rotation will in variable stored there by CreatePrefab class also used 
    /// so the variable name will be the prefab name and the prefab is in  CreatePrefab class declared 
    ///  this function get the name of this Prefab and stored in the  global variable(accessible for all scripts) the Rotation=0  but indexed with ame of this Prefab 
    /// that means that you get this value if you search for this name in PlayerPrefs.getFloat()
    /// parent:
    /// </summary>
    /// @author Ahmed L'harrak
    void Awake()
    {   
        GameObject parent = this.transform.parent.parent.Find("createpanel").gameObject;
        prefab = parent.transform.Find("create").gameObject.GetComponent<CreatePrefab>().currentPrefab.name;
        PlayerPrefs.SetFloat(prefab, 0f);
        
    }

    /// <summary>
    /// if you click on button, this function is called, wich search for the stored Rotation value of the current prefab and stored it in rotate variable
    /// which increments this variable rotate by 90 degrees by evrey call and stored again in the same globale variable
    /// and It is checked whether the variable rotate has the value 360 reached if yes then will to 0 reseted
    /// also the image in zhe Panel Windows will  rotated to easily create a preview for the Player
    /// rotate: 
    /// </summary>
    /// @author Ahmed L'harrak
    public void RotatePrefab_right()
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
    /// rotation left of the prefab
    /// rotate:
    /// <summary>
    /// @author Ahmed L'harrak
    public void RotatePrefab_left()
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
