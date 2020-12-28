using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	public bool richt =false;
	public bool links = false;
	public Vector3 pos;

		public List<DropCondition> DropConditions = new List<DropCondition>();
	public event Action<DraggableComponent> OnDropHandler;
	public bool Accepts(DraggableComponent draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
	}
	public void Drop(DraggableComponent draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
}
