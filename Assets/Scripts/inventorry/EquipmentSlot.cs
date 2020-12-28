using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{

	protected DropArea DropArea;

	protected virtual void Awake()
	{
		
		DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
		DropArea.pos=this.GetComponent<RectTransform>().position;
		DropArea.OnDropHandler += OnItemDropped;

	}

	private void OnItemDropped(DraggableComponent draggable)
	{
		RectTransform rect;
		rect=this.GetComponent<RectTransform>();
		if(draggable.dragrichtung==1){
		
		draggable.instantobjk.GetComponent<RectTransform>().position = new Vector3(rect.position.x,rect.position.y,rect.position.z+1.1f);
		DropArea.richt=true;
		draggable.links=true;
		}
		else{
		draggable.instantobjk.GetComponent<RectTransform>().position = new Vector3(rect.position.x,rect.position.y,rect.position.z-1.1f);
		DropArea.links=true;
		draggable.richt=true;
		}
		draggable.instantobjk.transform.SetParent(draggable.parent.transform);
		Destroy(draggable.instantobjk.GetComponent<DraggableComponent>());


	}
}
