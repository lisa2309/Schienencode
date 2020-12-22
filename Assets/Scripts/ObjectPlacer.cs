using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// A preview of objects is generated, as well as the object itself. 
/// The current coordinate depends on the mouse position
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller 
public class ObjectPlacer : MonoBehaviour
{
    public GameObject gameObject;
    public bool isPreviewOn;

    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;
    private Grid grid;
    private GameObject objectPreview;

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
    /// @author Ronja Haas & Anna-Lisa Müller 
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
    /// @author Ronja Haas & Anna-Lisa Müller 
    public void PlaceObjectNearPoint(bool isObjectPreview)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
            if (isObjectPreview)
            {
                objectPreview = Instantiate(gameObject, finalPosition, Quaternion.identity);
                GameObject objectPreviwChild;
                objectPreview.GetComponent<BoxCollider>().enabled = false;
                foreach(Transform c in objectPreview.transform.GetChild(0).GetComponentInChildren<Transform>())
                {
                    if (c.name != "Route")
                    {
                        c.GetComponent<Renderer>().material.color = new Color(255,20,147,0.5f);
                    }
                }
            }
            else if (!isObjectPreview)
            {
                Instantiate(gameObject, finalPosition, Quaternion.identity);
            }     
        }
    }

}