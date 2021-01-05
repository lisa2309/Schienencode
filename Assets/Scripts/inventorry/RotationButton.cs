using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// 
/// </summary>
/// @author Ahmed L'harrak
public class RotationButton : MonoBehaviour
{
    public string prefab;
    public GameObject imageButton;

    /// <summary>
    /// 
    /// </summary>
    /// @author Ahmed L'harrak
    void Awake()
    {
        GameObject parent = this.transform.parent.gameObject;
        prefab = parent.transform.Find("create").gameObject.GetComponent<CreatePrefab>().currentPrefab.name;
        PlayerPrefs.SetFloat(prefab, 0f);
    }

    /// <summary>
    /// 
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
        imageButton.transform.rotation = Quaternion.Euler(0, 0, -rotate - 90);
    }
}
