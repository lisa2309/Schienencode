using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
class DragTransform : MonoBehaviour
{
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;
    public bool richtfrei = true;
    public bool linksfrei = true;
 
    private Vector3 startpos;
    private Grid grid;
    private bool candrag;

    private RaycastHit hit;


        /// <summary>
    /// The object of type "Grid" is searched and stored in a local variable for later use. 
    /// </summary>
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }
    void OnMouseEnter()
    {
    
        GetComponent<Renderer>().material.color = mouseOverColor;
    }
 
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
 
    void OnMouseDown()
    {

        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }
 
    void OnMouseUp()
    {
  
GameObject gamob;
candrag=false;

if ( Physics.Raycast(transform.position,Vector3.right, out hit, 40f) || Physics.Raycast(transform.position,Vector3.forward, out hit, 30f)) {
    
gamob = hit.collider.gameObject;

    if( gamob != gameObject  ){
    if(gamob.name == gameObject.name && gamob.transform.rotation == gameObject.transform.rotation && gamob.GetComponent<DragTransform>().linksfrei == true){
    Debug.Log("links   ");
       //gameObject.transform.position = new Vector3(gamob.transform.position.x-4.05f,gamob.transform.position.y,gamob.transform.position.z);
       var finalPosition = grid.GetNearestPointOnGrid(hit.point);
       gameObject.transform.position = finalPosition;
        gamob.GetComponent<DragTransform>().linksfrei = false;
        this.richtfrei = false;
        candrag=true;



    }
    }
}

else if (Physics.Raycast(transform.position,Vector3.left, out hit, 2f) || Physics.Raycast(transform.position,-Vector3.forward, out hit, 2f)) {
    
gamob = hit.collider.gameObject;

    if(gamob.name != "Terrain" && gamob != gameObject  ){
    if(gamob.name == gameObject.name && gamob.transform.rotation == gameObject.transform.rotation  && gamob.GetComponent<DragTransform>().richtfrei == true){
        Debug.Log("rigth   ");
       gameObject.transform.position = new Vector3(gamob.transform.position.x+4.05f,gamob.transform.position.y,gamob.transform.position.z);
        gamob.GetComponent<DragTransform>().richtfrei = false;
        this.linksfrei = false;
        candrag=true;


    }
    }
}
if(!candrag) {
transform.position =startpos;
}
        dragging = false;

    }
    void OnMouseOver (){
 if(Input.GetMouseButtonDown(1)) {

    Destroy(gameObject);
   }
    }
    void Start(){

        startpos = transform.position;
        candrag=false;
    }
 
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,0.1f,rayPoint.z);


        }



        
    }
 




}
 