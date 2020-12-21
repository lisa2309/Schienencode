using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 2f;

    /// <summary>
    /// Transforms a coordinate into a point adapted to the grid system
    /// </summary>
    /// <param name="position">The point clicked in the game world</param>
    /// <returns>A point adapted to the grid</returns>
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            0.00f,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    /// <summary>
    /// Creates a grid of yellow dots that serve as orientation when designing a level and other objects
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 30; x += size)
        {
            for (float z = 0; z < 30; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
                
        }
    }
}