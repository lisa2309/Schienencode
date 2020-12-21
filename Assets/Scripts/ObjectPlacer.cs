using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private Grid grid;
    public GameObject gameObject;
    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;
    public BoxCollider boxColliderTerrain;
    private GameObject objectPreview ;
    public Boolean isPreviewOn;

    /// <summary>
    /// The object of type "Grid" is searched and stored in a local variable for later use. 
    /// </summary>
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseOver()
    {
        oldMousePosition = newMousePosition;
        newMousePosition = Input.mousePosition;
        if (boxColliderTerrain && (oldMousePosition != newMousePosition))
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
    /// 
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlaceObjectNearPoint(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isObjectPreview"></param>
    private void PlaceObjectNearPoint(Boolean isObjectPreview)
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
                        c.GetComponent<Renderer>().material.color = new Color(255,20,147);
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