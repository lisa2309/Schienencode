using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;


/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// A preview of objects is generated, as well as the object itself. 
/// The current coordinate depends on the mouse position
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
public class ObjectPlacer : MonoBehaviour
{
    /// <summary>
    /// name of prefab for Straight
    /// </summary>
    private const string strGeradeschiene = "Straight270Final";

    /// <summary>
    /// name of prefab for curve left
    /// </summary>
    private const string strCurveleft = "CurveL0Final";

    /// <summary>
    /// name of prefab for curve rigth
    /// </summary>
    private const string strCurverigth = "CurveR0Final";

    /// <summary>
    /// name of prefab for tunnel in
    /// </summary>
    private const string strTunelin = "TunnelIn";

    /// <summary>
    /// name of prefab for tunnel out
    /// </summary>
    private const string strTunelout = "TunnelOut";

    /// <summary>
    /// name of prefab for switch left 0
    /// </summary>
    private const string strSwitchl0 = "SwitchL0Final";

    /// <summary>
    /// name of prefab for switch left 1
    /// </summary>
    private const string strSwitchl1 = "SwitchL1Final";

    /// <summary>
    /// name of prefab for switch rigth 0
    /// </summary>
    private const string strSwitchr0 ="SwitchR0Final";

    /// <summary>
    /// name of prefab for switch rigth 1
    /// </summary>
    private const string strSwitchr1 ="SwitchR1Final";

    /// <summary>
    /// 
    /// </summary>
    public GameObject prefabtoinstant;

    /// <summary>
    /// 
    /// </summary>
    public bool isPreviewOn;

    /// <summary>
    /// 
    /// </summary>
    public float rotate = 0;

    /// <summary>
    /// 
    /// </summary>
    public Player player = null;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 oldMousePosition;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 newMousePosition;

    /// <summary>
    /// 
    /// </summary>
    private Grid grid;

    /// <summary>
    /// 
    /// </summary>
    private GameObject objectPreview;

    /// <summary>
    /// 
    /// </summary>
    private bool canDrag;

    /// <summary>
    /// 
    /// </summary>
    private Ray ray;
    
    /// <summary>
    /// Index from the actual Scene
    /// </summary>
    private int actualSceneIndex;
    

    /// <summary>
    /// The object of type "Grid" is searched and stored in a local variable for later use. 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    /// <summary>
    /// When the right mouse button is pressed, the "PlaceObjectNearPoint" method is called.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlaceObjectNearPoint(false);
        }
        ObjectPreview();
    }

    /// <summary>
    /// Creates an preview of an object and destroys it. 
    /// This happens only when the position of the mouse pointer has changed. 
    /// Because only then a new preview object is needed.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
    private void ObjectPreview()
    {
        oldMousePosition = newMousePosition;
        newMousePosition = Input.mousePosition;
        if (oldMousePosition != newMousePosition)
        {
            if (objectPreview != null)
            {
                Destroy(objectPreview);
            }
            if (isPreviewOn)
            {
                PlaceObjectNearPoint(true);
            }
        }
    }

    /// <summary>
    /// The point you clicked on in the game world is transformed into a coordinate and also adapted to the grid. 
    /// Depending on the value of the passing parameter, either an object is created or a preview of the object is generated. 
    /// if the created object generat a collision with other objectes  then will this object  destroyed
    /// if the dictance of poit0 of the both objectes (created and adjacent object) not more than 0.5 units also (composed) 
    /// but in in wrong direction then will this object  destroyed 
    /// Variables:
    /// hitInfo:
    /// finalPosition:
    /// gamob:
    /// arrpoint0:
    /// arrpoint00:
    /// arrpoint3:
    /// arrpoint33
    /// hitColliders:
    /// colls:
    /// dist:
    /// centerpoint:
    /// </summary>
    /// <param name="isObjectPreview">Is the object a preview of an object or not</param>
    /// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
    public void PlaceObjectNearPoint(bool isObjectPreview)
    {
        canDrag = true;
        RaycastHit hitInfo;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
            if (isObjectPreview)
            {
                objectPreview = Instantiate(prefabtoinstant, finalPosition, Quaternion.Euler(0, rotate, 0));
                objectPreview.name = prefabtoinstant.name;
                objectPreview.GetComponent<Collider>().enabled = false;
                if (prefabtoinstant.name == strTunelin || prefabtoinstant.name == strTunelout)
                {
                    foreach (Transform c in objectPreview.transform.GetChild(0).GetComponentInChildren<Transform>())
                    {
                        if (c.name != "Route")
                        {
                            
                            c.GetComponent<Renderer>().material.color = new Color(255, 20, 147, 0.5f);
                        }
                    }
                }
                else if (prefabtoinstant.name == strSwitchr0 || prefabtoinstant.name == strSwitchr1 || prefabtoinstant.name == strSwitchl0 || prefabtoinstant.name == strSwitchl1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        foreach (Transform c in objectPreview.transform.GetChild(i).GetComponentInChildren<Transform>())
                        {
                            if (c.name != "Route")
                            {
                                c.GetComponent<Renderer>().material.color = new Color(255, 20, 147, 0.5f);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Transform c in objectPreview.transform.GetChild(0).GetComponentInChildren<Transform>())
                    {
                        if (c.name != "Route")
                        {
                            c.GetComponent<Renderer>().material.color = new Color(255, 20, 147, 0.5f);
                        }
                    }
                }
            }
            else if (!isObjectPreview)
            {
                GameObject gamob;
                Vector3[] arrpoint0, arrpoint00, arrpoint3, arrpoint33;
                Collider[] hitColliders, colls;
                float dist;

                Vector3 centerpoint = objectPreview.GetComponent<BoxCollider>().center + finalPosition;
                centerpoint = Quaternion.Euler(0, rotate, 0) * (centerpoint - finalPosition) + finalPosition;
                colls = Physics.OverlapBox(centerpoint, objectPreview.GetComponent<BoxCollider>().size / 2);
                canDrag = true;
                
                arrpoint0 = GetPosition(objectPreview, "Point0");
                arrpoint3 = GetPosition(objectPreview, "Point3");

                foreach (Vector3 point0 in arrpoint0)
                {
                    hitColliders = Physics.OverlapSphere(point0, 2f);
                    foreach (var hitCollider in hitColliders)
                    {
                        arrpoint00 = GetPosition(hitCollider.gameObject, "Point0");
                        foreach (Vector3 point00 in arrpoint00){
                            dist = Vector3.Distance(point00, point0);
                            if (dist < 0.5f && point00 != Vector3.positiveInfinity)
                            {
                                canDrag = false;
                                break;
                            }
                        }
                        if(canDrag==false)
                        {
                            break;
                        }
                    }
                    if(canDrag==false)
                    {
                        break;
                    }
                }
                foreach (Vector3 point3 in arrpoint3)
                {
                    hitColliders = Physics.OverlapSphere(point3, 2f);
                    foreach (var hitCollider in hitColliders)
                    {
                        arrpoint33 = GetPosition(hitCollider.gameObject, "Point3");
                        foreach (Vector3 point33 in arrpoint33)
                        {
                            dist = Vector3.Distance(point33, point3);
                            if (dist < 0.5f && point33 != Vector3.positiveInfinity)
                            {
                                canDrag = false;
                                break;
                            }
                        }
                        if(canDrag==false)
                        {
                            break;
                        }
                    }
                    if(canDrag==false)
                    {
                        break;
                    }
                }
                foreach (Collider cool in colls)
                {
                    gamob = cool.gameObject;
                    if (gamob.name != "Terrain" && gamob.name != "Inside" && gamob.name != "Outside")
                    {
                        canDrag = false;
                    }
                }
                if (canDrag)
                {
                    if(player != null){
                        player.anrufen(prefabtoinstant.name, finalPosition, rotate, true);   
                    }
                }
            }
        }
    }

    /// <summary>
    /// this function becomm a collider gameobject and point name
    ///it will search for the point ther is 3 options if the object tunel then the point in 3 level deep
    ///if it is straight then deep level 2 else the object is somthing else the mybe have not rout gameobject that have points
    /// result:
    /// </summary>
    /// <param name="hitCollider"> this game object (adjacent objecte) does not far more than 2 units from the place of instantiate the prefab </param>
    /// <param name="point">the name of the point of the hitcolider (this point should at ende or begin this gameobject )</param>
    /// <returns>this function will return vector3 of the searched point </returns>
    /// @author Ahmed L'harrak
    ///
    Vector3[] GetPosition(GameObject hitCollider, String point)
    {
     Vector3[] result= new Vector3[2];

        

        if (hitCollider.name == strTunelout || hitCollider.name == strTunelin)
        {
            
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = Vector3.positiveInfinity;
        }
        else if (hitCollider.name == strGeradeschiene || hitCollider.name == strCurveleft|| hitCollider.name == strCurverigth)
        {
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = Vector3.positiveInfinity;
        }
        else if (hitCollider.name == strSwitchl0 || hitCollider.name == strSwitchl1 || hitCollider.name == strSwitchr0 || hitCollider.name == strSwitchr1)
        {
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = hitCollider.transform.GetChild(1).Find("Route").Find(point).position;
            //Debug.Log("hitcollieder name : "+result[0]+"  result 1 :"+result[1]);
        }
        else
        {
            result[0] = Vector3.positiveInfinity;
            result[1] = Vector3.positiveInfinity;
        }
    
        return result;
    }

}