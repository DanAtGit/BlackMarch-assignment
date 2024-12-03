using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public ObstacleData obstacleData;

// Using A* Algorithm for grid based search
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        PriorityQueue<Vector2Int> openSet = new PriorityQueue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, int> fScore = new Dictionary<Vector2Int, int>();

        openSet.Enqueue(start, 0);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, current);

            closedSet.Add(current);

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || IsObstacle(neighbor))
                    continue;

                int tentativeGScore = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return null;                // if no path found
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new List<Vector2Int> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    bool IsObstacle(Vector2Int position)
    {
        return obstacleData.obstacleGrid[position.x, position.y];
    }

    List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            position + Vector2Int.up,
            position + Vector2Int.down,
            position + Vector2Int.left,
            position + Vector2Int.right
        };

        // Filter out-of-bounds neighbors
        neighbors.RemoveAll(n => n.x < 0 || n.x >= 10 || n.y < 0 || n.y >= 10);
        return neighbors;
    }
}
