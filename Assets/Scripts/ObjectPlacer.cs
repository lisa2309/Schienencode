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
/// @author Ronja Haas & Anna-Lisa Müller 
public class ObjectPlacer : MonoBehaviour
{
    public GameObject gameObject;
    public bool isPreviewOn;

    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;
    private Grid grid;
    private GameObject objectPreview;
    public float rot=0;
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
        candrag=true;
        RaycastHit hitInfo;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        

        if (Physics.Raycast(ray, out hitInfo))
        {
             
            var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);

            if (isObjectPreview)
            {
                objectPreview = Instantiate(gameObject, finalPosition, Quaternion.Euler(0,rot, 0));
                
               
                GameObject objectPreviwChild;
                objectPreview.GetComponent<Collider>().enabled = false;
            if(gameObject.name=="TunnelIn" || gameObject.name=="TunnelOut" || gameObject.name=="TunnelInmitte"){
                        foreach(Transform c in objectPreview.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Transform>())
                                        {
                                            if (c.name != "Route")
                                            {
                                                c.GetComponent<Renderer>().material.color = new Color(255,20,147,0.5f);
                                            }
                                        }
                 }
            else    {
                        foreach(Transform c in objectPreview.transform.GetChild(0).GetComponentInChildren<Transform>())
                        {
                            if (c.name != "Route")
                            {
                                c.GetComponent<Renderer>().material.color = new Color(255,20,147,0.5f);
                            }
                        }
                    }
            }
            else if (!isObjectPreview)
            {
                GameObject gamob;
                ///Debug.Log("links   "+finalPosition);

        if(gameObject.name == "Straight270Final"){
                
                Vector3 xsize= new Vector3((objectPreview.GetComponent<BoxCollider>().size.x)/2,0,0);
                               
                Vector3 startcapsul = objectPreview.GetComponent<BoxCollider>().center + finalPosition + xsize ;
                Vector3 endcapsul= objectPreview.GetComponent<BoxCollider>().center + finalPosition - xsize ;
                
                
                startcapsul = Quaternion.Euler(0,rot, 0) * (startcapsul - finalPosition) + finalPosition ;
                endcapsul = Quaternion.Euler(0,rot, 0) *  (endcapsul - finalPosition) + finalPosition;

            Collider[] colls = Physics.OverlapCapsule(startcapsul ,endcapsul, 1f);
            ///Debug.DrawLine(startcapsul,endcapsul,Color.red,20f);
                    candrag=true;


                 Vector3 point0;
                 Vector3 point3;


                    point0 = objectPreview.transform.GetChild(0).Find("Route").Find("Point0").position;
                    point3 = objectPreview.transform.GetChild(0).Find("Route").Find("Point3").position;
                    
                    Collider[] hitColliders;
        
                            
                hitColliders = Physics.OverlapSphere(point0, 2f);
                           foreach (var hitCollider in hitColliders)
                            {
                                Debug.Log("name hit coll "+hitCollider.name );

                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"){
                                Vector3 point00 = hitCollider.transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                                else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){

                                Vector3 point00 = hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                                            
                hitColliders = Physics.OverlapSphere(point3, 2f);
                           foreach (var hitCollider in hitColliders)
                            {
                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                             else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                //Debug.Log("distance punkt3 "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                    foreach (Collider cool in colls ){
                    gamob = cool.gameObject;
                        
                      if(gamob.name !="Terrain"  && gamob.name !="Inside" && gamob.name !="Outside"){
                            Debug.Log("gamobjeeect name "+gamob.name);
                           candrag=false;
                         }

                    }
                   
           /// Debug.Log("punkt "+finalPosition+" hit punkt "+hitInfo.point+"gameobject "+hitInfo2.collider.gameObject.name);
        

                }
            else if(gameObject.name == "CurveL0Final" || gameObject.name =="CurveR0Final" ){
                Vector3 higth= new Vector3(0,0,(objectPreview.GetComponent<CapsuleCollider>().height)/2);
                Vector3 startcapsul = objectPreview.GetComponent<CapsuleCollider>().center + finalPosition +higth ;
                Vector3 endcapsul= objectPreview.GetComponent<CapsuleCollider>().center + finalPosition - higth;
                
              
                startcapsul = Quaternion.Euler(0,rot, 0) * (startcapsul - finalPosition) + finalPosition ;
                endcapsul = Quaternion.Euler(0,rot, 0) *  (endcapsul - finalPosition) + finalPosition;
               

            Collider[] colls = Physics.OverlapCapsule(startcapsul ,endcapsul,objectPreview.GetComponent<CapsuleCollider>().radius);
           
                    candrag=true;
                    Vector3 point0;
                    Vector3 point3;


                    point0 = objectPreview.transform.GetChild(0).Find("Route").Find("Point0").position;
                    point3 = objectPreview.transform.GetChild(0).Find("Route").Find("Point3").position;
                    Collider[] hitColliders;
        
                            
                hitColliders = Physics.OverlapSphere(point0, 3f);
                           foreach (var hitCollider in hitColliders)
                            {
                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"){
                                Vector3 point00 =hitCollider.transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                                else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){

                                Vector3 point00 = hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                                            
                hitColliders = Physics.OverlapSphere(point3, 3f);
                           foreach (var hitCollider in hitColliders)
                            {
                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                             else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                //Debug.Log("distance punkt3 "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                    
                    foreach (Collider cool in colls ){
                    gamob = cool.gameObject;
                        
                      if(gamob.name !="Terrain"  && gamob.name !="Inside" && gamob.name !="Outside"){
                            Debug.Log("gamobjeeect name "+gamob.name);
                           candrag=false;
                         }
                    }  
           /// Debug.Log("punkt "+finalPosition+" hit punkt "+hitInfo.point+"gameobject "+hitInfo2.collider.gameObject.name);
                }



            else if(gameObject.name == "TunnelIn" || gameObject.name == "TunnelOut" || gameObject.name=="TunnelInmitte"){
                
                Vector3 xsize= new Vector3(0,0,(objectPreview.GetComponent<BoxCollider>().size.z)/2);
     
                Vector3 startcapsul = objectPreview.GetComponent<BoxCollider>().center + finalPosition + xsize ;
                Vector3 endcapsul= objectPreview.GetComponent<BoxCollider>().center + finalPosition - xsize ;
                
                
                startcapsul = Quaternion.Euler(0,rot, 0) * (startcapsul - finalPosition) + finalPosition ;
                endcapsul = Quaternion.Euler(0,rot, 0) *  (endcapsul - finalPosition) + finalPosition;

            Collider[] colls = Physics.OverlapCapsule(startcapsul ,endcapsul, 1f);
            ///Debug.DrawLine(startcapsul,endcapsul,Color.red,20f);
                    candrag=true;


                 Vector3 point0;
                 Vector3 point3;


                    point0 = objectPreview.transform.GetChild(0).GetChild(0).Find("Route").Find("Point0").position;
                    point3 = objectPreview.transform.GetChild(0).GetChild(0).Find("Route").Find("Point3").position;
                    
                    Collider[] hitColliders;
        
                            
                hitColliders = Physics.OverlapSphere(point0, 2f);
                           foreach (var hitCollider in hitColliders)
                            {
                                Debug.Log("name hit coll "+hitCollider.name );

                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"){
                                Vector3 point00 = hitCollider.transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance punkt0 "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                                else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){

                                Vector3 point00 = hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point0").position;
                                float dist = Vector3.Distance(point00, point0);
                                Debug.Log("distance "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                                            
                hitColliders = Physics.OverlapSphere(point3, 2f);
                           foreach (var hitCollider in hitColliders)
                            {
                                if(hitCollider.name  == "Straight270Final(Clone)" || hitCollider.name  == "CurveL0Final(Clone)" || hitCollider.name  == "CurveR0Final(Clone)"|| hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                //Debug.Log("distance punkt3 "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                              else if( hitCollider.name  == "TunnelOut(Clone)"|| hitCollider.name  == "TunnelIn(Clone)"|| hitCollider.name  == "Tunnelmitte(Clone)"){
                                Transform point33 =hitCollider.transform.GetChild(0).transform.GetChild(0).Find("Route").Find("Point3");
                                float dist = Vector3.Distance(point33.position, point3);
                                //Debug.Log("distance punkt3 "+dist);
                               if(dist<0.5f){
                                    candrag=false;
                               }
                                }
                            }
                                            
                        


                    
                    foreach (Collider cool in colls ){
                    gamob = cool.gameObject;
                        
                      if(gamob.name !="Terrain" && gamob.name !="Inside" && gamob.name !="Outside" ){
                            //Debug.Log("gamobjeeect name "+gamob.name);
                           candrag=false;
                         }

                    }
                   
           /// Debug.Log("punkt "+finalPosition+" hit punkt "+hitInfo.point+"gameobject "+hitInfo2.collider.gameObject.name);
          


                }





               
                if(candrag){
                        Instantiate(gameObject, finalPosition,Quaternion.Euler(0,rot, 0));
                }
   
              
               
                //gameObject.transform.position = new Vector3(gamob.transform.position.x-4.05f,gamob.transform.position.y,gamob.transform.position.z);
                
            }    
        }
    }

}