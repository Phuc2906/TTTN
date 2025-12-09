using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinding : MonoBehaviour
{
    public GridManager grid;

    // Đường đi cuối cùng để GridManager vẽ Gizmos
    [HideInInspector] public List<Node> lastPath;

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Nếu 1 trong 2 điểm nằm ngoài grid thì bỏ qua A*, cho dùng fallback
        if (!grid.IsInsideGrid(startPos) || !grid.IsInsideGrid(targetPos))
        {
            lastPath = null;
            return null;
        }

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // RESET toàn bộ node trước khi tìm đường mới
        foreach (Node n in grid.grid)
        {
            n.gCost = 0;
            n.hCost = 0;
            n.parent = null;
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        startNode.gCost = 0;
        startNode.hCost = GetDistance(startNode, targetNode);

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < current.fCost ||
                    (openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost))
                {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == targetNode)
            {
                lastPath = RetracePath(startNode, targetNode);
                return lastPath;
            }

            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newCost = current.gCost + GetDistance(current, neighbour);
                if (newCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = current;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        // Không tìm được đường
        lastPath = null;
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    int GetDistance(Node a, Node b)
    {
        int dx = Mathf.Abs(a.gridX - b.gridX);
        int dy = Mathf.Abs(a.gridY - b.gridY);

        // Diagonal = 14, straight = 10
        return 14 * Mathf.Min(dx, dy) + 10 * Mathf.Abs(dx - dy);
    }
}
