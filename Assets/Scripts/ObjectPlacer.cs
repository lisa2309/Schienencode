using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;
using Debug = UnityEngine.Debug;


/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// A preview of objects is generated, as well as the object itself. 
/// The current coordinate depends on the mouse position
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
public class ObjectPlacer : MonoBehaviour
{
    public GameObject prefabtoinstant;
    public bool isPreviewOn;
    public float rotate = 0;
    public Player player=null;

    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;
    private Grid grid;
    private GameObject objectPreview;
    private bool canDrag;
    private Ray ray;

    /// <summary>
    /// The object of type "Grid" is searched and stored in a local variable for later use. 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
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
    ///  if the created object generat a collision with other objectes  then will this object  destroyed
    /// if the dictance of poit0 of the both objectes (created and adjacent object) not more than 0.5 units also (composed) 
    /// but in in wrong direction then will this object  destroyed 
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
                if (prefabtoinstant.name == "TunnelIn" || prefabtoinstant.name == "TunnelOut" || prefabtoinstant.name == "TunnelInmitte")
                {
                    foreach (Transform c in objectPreview.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Transform>())
                    {
                        if (c.name != "Route")
                        {
                            c.GetComponent<Renderer>().material.color = new Color(255, 20, 147, 0.5f);
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
                Vector3 point0, point00, point3, point33;
                Collider[] hitColliders, colls;
                float dist;

                Vector3 centerpoint = objectPreview.GetComponent<BoxCollider>().center + finalPosition;
                centerpoint = Quaternion.Euler(0, rotate, 0) * (centerpoint - finalPosition) + finalPosition;
                colls = Physics.OverlapBox(centerpoint, objectPreview.GetComponent<BoxCollider>().size / 2);
                canDrag = true;
                point0 = GetPosition(objectPreview, "Point0");
                point3 = GetPosition(objectPreview, "Point3");

                hitColliders = Physics.OverlapSphere(point0, 2f);
                foreach (var hitCollider in hitColliders)
                {
                    point00 = GetPosition(hitCollider.gameObject, "Point0");
                    dist = Vector3.Distance(point00, point0);
                    if (dist < 0.5f && point00 != Vector3.positiveInfinity)
                    {
                        canDrag = false;
                    }
                }
                hitColliders = Physics.OverlapSphere(point3, 2f);
                foreach (var hitCollider in hitColliders)
                {
                    point33 = GetPosition(hitCollider.gameObject, "Point3");
                    dist = Vector3.Distance(point33, point3);
                    if (dist < 0.5f && point33 != Vector3.positiveInfinity)
                    {
                        canDrag = false;
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
                 player.anrufen(prefabtoinstant.name, finalPosition, rotate);
                    
                    
                }
                
                   // GameObject cloneObj = Instantiate(prefabtoinstant, finalPosition, Quaternion.Euler(0, rotate, 0));
                   // cloneObj.name = prefabtoinstant.name;
                }
            }
        }
    }

    /// <summary>
    /// this function becomm a collider gameobject and point name
    ///it will search for the point ther is 3 options if the object tunel then the point in 3 level deep
    ///if it is straight then deep level 2 else the object is somthing else the mybe have not rout gameobject that have points
    /// </summary>
    /// <param name="hitCollider"> this game object (adjacent objecte) does not far more than 2 units from the place of instantiate the prefab </param>
    /// <param name="point">the name of the point of the hitcolider (this point should at ende or begin this gameobject )</param>
    /// <returns>this function will return vector3 of the searched point </returns>
    /// @author Ahmed L'harrak
    ///
    Vector3 GetPosition(GameObject hitCollider, String point)
    {
        if (hitCollider.name == "TunnelOut" || hitCollider.name == "TunnelIn")
        {
            return hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find(point).position;
        }
        else if (hitCollider.name == "Straight270Final" || hitCollider.name == "CurveL0Final" || hitCollider.name == "CurveR0Final")
        {
            return hitCollider.transform.GetChild(0).Find("Route").Find(point).position;
        }
        else
        {
            return Vector3.positiveInfinity;
        }
    }

}