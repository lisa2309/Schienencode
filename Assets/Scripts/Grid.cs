using UnityEngine;

/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// A grid is created and points are adjusted to the grid
/// </summary>
/// @source: https://www.youtube.com/watch?v=VBZFYGWvm4A
/// @author Jason Weimann 
public class Grid : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Size of a single grid field
    /// </summary>
    private float size = 2f;

    /// <summary>
    /// Transforms a coordinate into a point adapted to the grid system
    /// Variables:
    /// xCount: X coordinate for a single grid point
    /// yCount: Y coordinate for a single grid point
    /// zCount: Z coordinate for a single grid point
    /// result: Vector X, Y and Z coordinate for a single grid point
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
    /// point: Variable of the X, Y and Z coordinate for a single grid point
    /// x: Index for the for loop
    /// z: Index for the for loop
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 100; x += size)
        {
            for (float z = 0; z < 55; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }             
        }
    }

}