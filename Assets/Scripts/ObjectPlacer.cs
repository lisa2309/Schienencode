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
    /// Name of prefab for Straight
    /// </summary>
    private const string strGeradeschiene = "Straight270Final";

    /// <summary>
    /// Name of prefab for begin or first Straight
    /// </summary>
    private const string strrailstart="RailStart";

    /// <summary>
    /// Name of prefab for end or final  Straight
    /// </summary>
    private const string strrailend="RailEnd";
    /// <summary>
    /// Name of prefab for curve left
    /// </summary>
    private const string strCurveleft = "CurveL0Final";

    /// <summary>
    /// Name of prefab for curve rigth
    /// </summary>
    private const string strCurverigth = "CurveR0Final";

    /// <summary>
    /// Name of prefab for tunnel in
    /// </summary>
    private const string strTunelin = "TunnelIn";

    /// <summary>
    /// Name of prefab for tunnel out
    /// </summary>
    private const string strTunelout = "TunnelOut";

    /// <summary>
    /// Name of prefab for switch left 0
    /// </summary>
    private const string strSwitchl0 = "SwitchL0Final";

    /// <summary>
    /// Name of prefab for switch left 1
    /// </summary>
    private const string strSwitchl1 = "SwitchL1Final";

    /// <summary>
    /// Name of prefab for switch rigth 0
    /// </summary>
    private const string strSwitchr0 ="SwitchR0Final";

    /// <summary>
    /// Name of prefab for switch rigth 1
    /// </summary>
    private const string strSwitchr1 ="SwitchR1Final";

    /// <summary>
    /// Game object for instantiate prefab
    /// </summary>
    public GameObject prefabToInstant;

    /// <summary>
    /// This will indicate if the prefab will  have a preview or not
    /// </summary>
    public bool isPreviewOn;

    /// <summary>
    /// Rotation of the instantiat prefab
    /// </summary>
    public float rotate = 0;

    /// <summary>
    /// Reference of the local player (current player) --> me 
    /// </summary>
    public Player player = null;

    /// <summary>
    /// Old mouse position on sean
    /// </summary>
    private Vector3 oldMousePosition;

    /// <summary>
    /// Regestrait the new position of the mouse
    /// </summary>
    private Vector3 newMousePosition;

    /// <summary>
    /// Object of the script Grid 
    /// </summary>
    private Grid grid;

    /// <summary>
    /// This is the preview of an object
    /// </summary>
    private GameObject objectPreview;

    /// <summary>
    /// A flag to indicat if is permited to drag a game object on the game if it is false than will note create the game object on the game
    /// </summary>
    private bool canDrag;

    /// <summary>
    /// Ray helfe to get mouse position on sean
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
    /// In addition, it is checked whether the rules of placing rails are observed. 
    /// hitInfo: Collision informations 
    /// finalPosition: The adjusted position with Grid
    /// gamob: Help variable to store the current gameobject
    /// arrpoint0: Coordinate of Point0 of the instantiate gameobject 
    /// arrpoint00: Coordinate of Point0 of the hited collider gameobject 
    /// arrpoint3: Coordinate of Point3 of the instantiate gameobject 
    /// arrpoint33 : Coordinate of Point3 of the hit collider gameobject 
    /// hitColliders: The hit collider with the instantiate gameobject
    /// colls: Collision informations which contains all collisions 
    /// dist: The distance between two same point of two different game objects (instantiated game object and the collided gameobject)
    /// centerpoint: The coordinate of the created boxcollider to detect which object is around the instatiated gameobject
    /// i: Index for the for loop 
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
                objectPreview = Instantiate(prefabToInstant, finalPosition, Quaternion.Euler(0, rotate, 0));
                objectPreview.name = prefabToInstant.name;
                objectPreview.GetComponent<Collider>().enabled = false;
                if (prefabToInstant.name == strTunelin || prefabToInstant.name == strTunelout)
                {
                    foreach (Transform c in objectPreview.transform.GetChild(0).GetComponentInChildren<Transform>())
                    {
                        if (c.name != "Route")
                        {    
                            c.GetComponent<Renderer>().material.color = new Color(255, 20, 147, 0.5f);
                        }
                    }
                }
                else if (prefabToInstant.name == strSwitchr0 || prefabToInstant.name == strSwitchr1 || prefabToInstant.name == strSwitchl0 || prefabToInstant.name == strSwitchl1)
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
                        if (canDrag==false)
                        {
                            break;
                        }
                    }
                    if (canDrag==false)
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
                        if (canDrag==false)
                        {
                            break;
                        }
                    }
                    if (canDrag==false)
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
                    if (player != null){
                        player.Call(prefabToInstant.name, finalPosition, rotate, true);   
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds the position of the gameobject which is in the Drag and Drop Bar.
    /// result: This is the position which is searched
    /// </summary>
    /// <param name="hitCollider">This game object (adjacent objecte) does not far more than 2 units from the place of instantiate the prefab </param>
    /// <param name="point">The name of the point of the hitcolider (this point should be at the end or begin from this gameobject)</param>
    /// <returns>Vector3 of the searched point</returns>
    /// @author Ahmed L'harrak
    Vector3[] GetPosition(GameObject hitCollider, String point)
    {
        Vector3[] result = new Vector3[2];

        if (hitCollider.name == strTunelout || hitCollider.name == strTunelin)
        {      
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = Vector3.positiveInfinity;
        }
        else if (hitCollider.name == strGeradeschiene || hitCollider.name == strCurveleft|| hitCollider.name == strCurverigth|| hitCollider.name == strrailend|| hitCollider.name == strrailstart)
        {
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = Vector3.positiveInfinity;
        }
        else if (hitCollider.name == strSwitchl0 || hitCollider.name == strSwitchl1 || hitCollider.name == strSwitchr0 || hitCollider.name == strSwitchr1)
        {
            result[0] = hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
            result[1] = hitCollider.transform.GetChild(1).Find("Route").Find(point).position;
        }
        else
        {
            result[0] = Vector3.positiveInfinity;
            result[1] = Vector3.positiveInfinity;
        }
        return result;
    }

}