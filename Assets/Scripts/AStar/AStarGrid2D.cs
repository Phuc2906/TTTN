using System.Collections.Generic;
using UnityEngine;

public class AStarGrid2D : MonoBehaviour
{
    public static AStarGrid2D Instance;

    [Header("Grid")]
    public LayerMask obstacleMask;
    public float nodeSize = 0.5f;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        Instance = this;
    }

    public class Node
    {
        public Vector2 worldPos;
        public Vector2Int gridPos;
        public bool walkable;

        public int gCost;
        public int hCost;
        public int fCost => gCost + hCost;

        public Node parent;
    }

    Vector2Int WorldToGrid(Vector2 pos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(pos.x / nodeSize),
            Mathf.RoundToInt(pos.y / nodeSize)
        );
    }

    Vector2 GridToWorld(Vector2Int gridPos)
    {
        return new Vector2(gridPos.x * nodeSize, gridPos.y * nodeSize);
    }

    Node GetNode(Vector2Int gridPos)
    {
        if (grid.ContainsKey(gridPos))
            return grid[gridPos];

        Vector2 world = GridToWorld(gridPos);

        bool blocked = Physics2D.OverlapCircle(world, nodeSize * 0.4f, obstacleMask);

        Node node = new Node
        {
            gridPos = gridPos,
            worldPos = world,
            walkable = !blocked
        };

        grid.Add(gridPos, node);
        return node;
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = GetNode(WorldToGrid(start));
        Node targetNode = GetNode(WorldToGrid(target));

        List<Node> open = new List<Node>();
        HashSet<Node> closed = new HashSet<Node>();

        open.Add(startNode);

        while (open.Count > 0)
        {
            Node current = open[0];

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost ||
                    open[i].fCost == current.fCost && open[i].hCost < current.hCost)
                {
                    current = open[i];
                }
            }

            open.Remove(current);
            closed.Add(current);

            if (current == targetNode)
                return Retrace(startNode, targetNode);

            foreach (Node n in GetNeighbours(current))
            {
                if (!n.walkable || closed.Contains(n))
                    continue;

                int newCost = current.gCost + GetDistance(current, n);

                if (newCost < n.gCost || !open.Contains(n))
                {
                    n.gCost = newCost;
                    n.hCost = GetDistance(n, targetNode);
                    n.parent = current;

                    if (!open.Contains(n))
                        open.Add(n);
                }
            }
        }

        return null;
    }

    List<Node> GetNeighbours(Node node)
    {
        List<Node> list = new List<Node>();

        Vector2Int p = node.gridPos;

        list.Add(GetNode(new Vector2Int(p.x + 1, p.y)));
        list.Add(GetNode(new Vector2Int(p.x - 1, p.y)));
        list.Add(GetNode(new Vector2Int(p.x, p.y + 1)));
        list.Add(GetNode(new Vector2Int(p.x, p.y - 1)));

        return list;
    }

    int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a.gridPos.x - b.gridPos.x) +
               Mathf.Abs(a.gridPos.y - b.gridPos.y);
    }

    List<Vector2> Retrace(Node start, Node end)
    {
        List<Vector2> path = new List<Vector2>();

        Node current = end;

        while (current != start)
        {
            path.Add(current.worldPos);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
}