using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Createprefab : MonoBehaviour
{
    public GameObject myPrefab;
    private GameObject instantobjk;
    public GameObject parent;
    private Transform target;
    private float rot;
    private ObjectPlacer ojplz;
    public  void Createschien()
    {
          
  

    rot = PlayerPrefs.GetFloat(myPrefab.name);
  //Debug.Log("creat prefabs  "+rot);
 Vector3 position;
 
        Ray ray = new Ray(Camera.main.transform.position, Vector3.forward);
        position = ray.GetPoint(Camera.main.farClipPlane / 2);
        //Debug.Log("target is " + position + " pixels from the left");
        ojplz = FindObjectOfType<ObjectPlacer>();
        ojplz.gameObject=myPrefab;
        ojplz.rot=rot;
        ojplz.isPreviewOn=true;
        
        //instantobjk = Instantiate(myPrefab, new Vector3(position.x,0.03f,position.y),Quaternion.Euler(0,rot, 0),parent.transform);
        
    
    }


    
}
