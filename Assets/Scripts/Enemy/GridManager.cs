using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2 gridWorldSize = new Vector2(50, 40);
    public float nodeRadius = 0.25f;
    public LayerMask obstacleMask;
    public bool drawGizmos = true;

    [HideInInspector] public Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2f;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position
                                - Vector3.right * gridWorldSize.x / 2f
                                - Vector3.up * gridWorldSize.y / 2f;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft +
                                     Vector3.right * (x * nodeDiameter + nodeRadius) +
                                     Vector3.up    * (y * nodeDiameter + nodeRadius);

                bool walkable = !Physics2D.OverlapBox(
                    worldPoint,
                    Vector2.one * (nodeRadius * 2.2f),
                    0,
                    obstacleMask
                );

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    /// <summary>
    /// Kiểm tra 1 điểm thế giới có nằm trong vùng grid hay không
    /// </summary>
    public bool IsInsideGrid(Vector3 worldPosition)
    {
        Vector3 worldBottomLeft = transform.position
                                - Vector3.right * gridWorldSize.x / 2f
                                - Vector3.up * gridWorldSize.y / 2f;

        Vector3 worldTopRight = worldBottomLeft + new Vector3(gridWorldSize.x, gridWorldSize.y, 0);

        return worldPosition.x >= worldBottomLeft.x && worldPosition.x <= worldTopRight.x &&
               worldPosition.y >= worldBottomLeft.y && worldPosition.y <= worldTopRight.y;
    }

    /// <summary>
    /// Lấy node gần nhất từ worldPosition (đã tính theo vị trí GridManager)
    /// </summary>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 worldBottomLeft = transform.position
                                - Vector3.right * gridWorldSize.x / 2f
                                - Vector3.up * gridWorldSize.y / 2f;

        float percentX = (worldPosition.x - worldBottomLeft.x) / gridWorldSize.x;
        float percentY = (worldPosition.y - worldBottomLeft.y) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt(gridSizeX * percentX), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(gridSizeY * percentY), 0, gridSizeY - 1);

        return grid[x, y];
    }

    public Node[] GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX &&
                    checkY >= 0 && checkY < gridSizeY)
                {
                    Node n = grid[checkX, checkY];

                    // Không cho đi chéo qua tường
                    if (x != 0 && y != 0)
                    {
                        Node n1 = grid[node.gridX + x, node.gridY];
                        Node n2 = grid[node.gridX, node.gridY + y];
                        if (!n1.walkable || !n2.walkable)
                            continue;
                    }

                    neighbours.Add(n);
                }
            }
        }

        return neighbours.ToArray();
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        // Vẽ khung grid
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        // Vẽ node
        if (grid != null)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Node n = grid[x, y];
                    Gizmos.color = n.walkable ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter * 0.9f));
                }
            }
        }

        // Vẽ path màu xanh (nếu có A*)
        AStarPathfinding pf = FindObjectOfType<AStarPathfinding>();
        if (pf != null && pf.lastPath != null)
        {
            Gizmos.color = Color.green;
            foreach (Node n in pf.lastPath)
            {
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter * 0.7f));
            }
        }
    }
}
