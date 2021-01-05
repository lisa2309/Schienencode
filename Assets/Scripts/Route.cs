﻿
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/*
Quelle: https://www.youtube.com/watch?v=11ofnLOE8pw
*/
/// <summary>
/// 
/// </summary>
public class Route : MonoBehaviour
{
	[SerializeField]
	private Transform[] controlPoints;
	private Vector3 gizmosPosition;

	/// <summary>
    /// 
    /// </summary>
    /// @author Florian Vogel & Bjarne Bensel
	private void OnDrawGizmos()
	{
		for (float t = 0; t <= 1; t += 0.05f)
		{
			gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
				3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
				3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
				Mathf.Pow(t, 3) * controlPoints[3].position;

			Gizmos.DrawSphere(gizmosPosition, 0.25f);
		}

		Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[3].position.z),
			new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[3].position.z));

		Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[3].position.z),
			new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));
	}
}