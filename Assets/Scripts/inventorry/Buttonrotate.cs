using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonrotate : MonoBehaviour
{
    // Start is called before the first frame update
    private float rot;
    public string prefab;
    public GameObject imgbutton;



    void Awake()
    {
        GameObject parent = this.transform.parent.gameObject;
        prefab = parent.transform.Find("create").gameObject.GetComponent<Createprefab>().myPrefab.name;
        PlayerPrefs.SetFloat(prefab, 0f);

    }

    public void rotate()
    {

        rot = PlayerPrefs.GetFloat(prefab);
        if (rot == 360)
        {
            rot = 0f;
        }
        PlayerPrefs.SetFloat(prefab, rot + 90);

        imgbutton.transform.rotation = Quaternion.Euler(0, 0, -rot - 90);


    }
}
