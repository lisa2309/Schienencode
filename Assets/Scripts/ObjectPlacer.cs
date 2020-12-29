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
/// @author Ronja Haas & Anna-Lisa Müller  & Ahmed L'harrak
public class ObjectPlacer : MonoBehaviour
{
    public GameObject gameObject;
    public bool isPreviewOn;

    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;
    private Grid grid;
    private GameObject objectPreview;
    public float rot = 0;
    private bool candrag;
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
    /// but this happens only when the position of the mouse pointer has changed. 
    /// Because only then a new preview object is needed.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller  & Ahmed L'harrak
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
    /// </summary>
    /// <param name="isObjectPreview">Is the object a preview of an object or not</param>
    /// @author Ronja Haas & Anna-Lisa Müller & Ahmed L'harrak
    public void PlaceObjectNearPoint(bool isObjectPreview)
    {
        candrag = true;
        RaycastHit hitInfo;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hitInfo))
        {

            var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);

            if (isObjectPreview)
            {
                objectPreview = Instantiate(gameObject, finalPosition, Quaternion.Euler(0, rot, 0));
                objectPreview.name = gameObject.name;


                GameObject objectPreviwChild;
                objectPreview.GetComponent<Collider>().enabled = false;
                if (gameObject.name == "TunnelIn" || gameObject.name == "TunnelOut" || gameObject.name == "TunnelInmitte")
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
                ///Debug.Log("links   "+finalPosition);

                ///// Creat boxcollieder gleich wie bei der Prefab///////////
                /////////////////////////////////////////////////////////////
                Vector3 centerpoint = objectPreview.GetComponent<BoxCollider>().center + finalPosition;

                /////////////////////// rotation der prefabs -> rotation kordinaten//////////////
                centerpoint = Quaternion.Euler(0, rot, 0) * (centerpoint - finalPosition) + finalPosition;

                /////////////////////// überprufen ob es collision 
                colls = Physics.OverlapBox(centerpoint, objectPreview.GetComponent<BoxCollider>().size / 2);
                //Debug.DrawLine(centerpoint,new Vector3(centerpoint.x + objectPreview.GetComponent<BoxCollider>().size.x,centerpoint.y,centerpoint.z +objectPreview.GetComponent<BoxCollider>().size.z),Color.red,20f);
                candrag = true;
                point0 = getposition(objectPreview, "Point0");
                point3 = getposition(objectPreview, "Point3");

                hitColliders = Physics.OverlapSphere(point0, 2f);
                foreach (var hitCollider in hitColliders)
                {

                    //Debug.Log("name hit coll " + hitCollider.name);
                    point00 = getposition(hitCollider.gameObject, "Point0");
                    dist = Vector3.Distance(point00, point0);
                    //Debug.Log("distance point 0" + dist);
                    if (dist < 0.5f && point00 != Vector3.positiveInfinity)
                    {
                        candrag = false;
                    }


                }

                hitColliders = Physics.OverlapSphere(point3, 2f);
                foreach (var hitCollider in hitColliders)
                {
                    point33 = getposition(hitCollider.gameObject, "Point3");
                    dist = Vector3.Distance(point33, point3);
                    //Debug.Log("distance point 3" + dist);
                    if (dist < 0.5f && point33 != Vector3.positiveInfinity)
                    {
                        candrag = false;
                    }
                }
                foreach (Collider cool in colls)
                {
                    gamob = cool.gameObject;

                    if (gamob.name != "Terrain" && gamob.name != "Inside" && gamob.name != "Outside")
                    {
                        ///Debug.Log("gamobjeeect name "+gamob.name);
                        candrag = false;
                    }

                }

                /// Debug.Log("punkt "+finalPosition+" hit punkt "+hitInfo.point+"gameobject "+hitInfo2.collider.gameObject.name);

                if (candrag)
                {

                    GameObject cloneobj = Instantiate(gameObject, finalPosition, Quaternion.Euler(0, rot, 0));
                    cloneobj.name = gameObject.name;

                }

                //gameObject.transform.position = new Vector3(gamob.transform.position.x-4.05f,gamob.transform.position.y,gamob.transform.position.z);

            }
        }
    }


    Vector3 getposition(GameObject hitcoll, String point)
    {

        //Debug.Log("Getposition point " + hitcoll.name);
        if (hitcoll.name == "TunnelOut" || hitcoll.name == "TunnelIn")
        {

            return hitcoll.transform.GetChild(0).transform.GetChild(0).Find("Route").Find(point).position;

        }
        else if (hitcoll.name == "Straight270Final" || hitcoll.name == "CurveL0Final" || hitcoll.name == "CurveR0Final")
        {

            return hitcoll.transform.GetChild(0).Find("Route").Find(point).position;

        }
        else return Vector3.positiveInfinity;


    }

}