
using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/*
*/
/// <summary>
/// Script to move the Train on the Rail
/// </summary>
/// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
/// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
public class Route : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// 4 points on every rail (entrance, exit, 2 shape modifiers)
	/// </summary>
	private Transform[] controlPoints;

	/// <summary>
    /// calculated point on bezier shape
    /// </summary>
	private Vector3 gizmosPosition;

	/// <summary>
	/// Lets the Train move on the rail
	/// </summary>
	/// @author Alexander Zotov. Modified by Florian Vogel & Bjarne Bensel
	/// @source: https://www.youtube.com/watch?v=11ofnLOE8pw
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