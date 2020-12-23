using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject myPrefab;
	public GameObject instantobjk;
	public event Action<PointerEventData> OnBeginDragHandler;
	public event Action<PointerEventData> OnDragHandler;
	public event Action<PointerEventData, bool> OnEndDragHandler;
	public bool FollowCursor { get; set; } = true;
	public Vector3 StartPosition;
	public bool CanDrag { get; set; } = true;
	public bool richt =false;
	public bool links = false;
	public int dragrichtung=0;

	private RectTransform rectTransform;
	private RectTransform rectTransform1;
	private Transform transform1;
	public Camera plancamera;
	private Ray ray;
	private Vector3 rayPoint;
	public GameObject parent;

	private float distance;

	private void Awake()
	{
		rectTransform1 = GetComponent<RectTransform>();
		transform1 = GetComponent<Transform>();
		

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			Destroy (instantobjk);
			return;
		}



			ray = plancamera.ScreenPointToRay(Input.mousePosition);
            rayPoint = ray.GetPoint(distance);
		 

		instantobjk =  Instantiate(myPrefab, new Vector3 (7.152126f,-0.017f,7.152126f), Quaternion.Euler(-90,0, 0));
		distance = Vector3.Distance(transform.position, plancamera.transform.position);
		instantobjk.AddComponent<EquipmentSlot>(); 

		rectTransform = instantobjk.GetComponent<RectTransform>();
        



		//rectTransform.anchoredPosition=new Vector2(rectTransform1.transform.position.x+relativePosition.x,rectTransform1.transform.position.y+relativePosition.y);

		//Debug.Log("begin    "+instantobjk.transform.position);

		OnBeginDragHandler?.Invoke(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		
		if (!CanDrag)
		{
			Destroy (instantobjk);
			return;
		}

		OnDragHandler?.Invoke(eventData);

		if (FollowCursor)
		{

			ray = plancamera.ScreenPointToRay(Input.mousePosition);
            rayPoint = ray.GetPoint(distance);
			instantobjk.transform.position = new Vector3 (rayPoint.x,-0.017f,rayPoint.z);

			//Debug.Log("ondrag transform "+instantobjk.transform.position);
			///rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
			//Debug.Log("anchred "+rectTransform.anchoredPosition );

			//Debug.Log("ondrag    "+instantobjk.transform.position);
			//Debug.Log("ondrag xray    "+rayPoint.x);
			
			
		}
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{


		//Debug.Log("Text: " +CanDrag);
		if (!CanDrag)
		{
			Destroy (instantobjk);
			return;
		}

		var results = new List<RaycastResult>();
		Debug.Log("The cursor clicked the selectable UI element. " + eventData);
		EventSystem.current.RaycastAll(eventData, results);
		
		DropArea dropArea = null;

		foreach (var result in results)
		{
			
			dropArea = result.gameObject.GetComponent<DropArea>();
			Debug.Log("teeeest   "+result);
			//Debug.Log("transform "+instantobjk.transform.position);

			if (dropArea != null)
			{
				Debug.Log("teeeest   "+result);

				break;
			}
		}

		if (dropArea != null)
		{
			///Debug.Log("teeeeeeeest");
			if (dropArea.Accepts(this) )
			{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
			
			if(dropArea.pos.x<rayPoint.x &&  dropArea.richt==false){
				this.dragrichtung=1;
				//Debug.Log("riiicht");
				dropArea.Drop(this);
				
				OnEndDragHandler?.Invoke(eventData, true);
				return;
				}
			else if(dropArea.pos.x>rayPoint.x &&  dropArea.links==false){
				this.dragrichtung=2;
				//Debug.Log("links");
				dropArea.Drop(this);
				OnEndDragHandler?.Invoke(eventData, true);
				return;
				}

			}
		}
		Destroy (instantobjk);
		OnEndDragHandler?.Invoke(eventData, false);
	
	}

	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		///Debug.Log("tessssssst");
		StartPosition = GetComponent<RectTransform>().anchoredPosition;
	}
}

