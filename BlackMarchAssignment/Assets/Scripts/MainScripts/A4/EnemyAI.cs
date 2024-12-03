using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public Pathfinding pathfinding;
    public float moveSpeed = 2f;
    public float heightOffset = 1.55f; 

    private Vector2Int gridPosition;
    public bool isMoving = false;

    void Start()
    {
        gridPosition = new Vector2Int(9, 9);
        transform.position = new Vector3(gridPosition.x, heightOffset, gridPosition.y);
    }

    public void MoveTowardsTarget(Vector2Int playerPosition)
    {
        if (isMoving) return;

        List<Vector2Int> path = pathfinding.FindPath(gridPosition, playerPosition);

        if (path != null && path.Count > 1)
        {
            // Exclude the last tile (player's position) to stop adjacent
            path.RemoveAt(path.Count - 1);
            StartCoroutine(MoveAlongPath(path));
        }
    }

    IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        isMoving = true;

        foreach (Vector2Int position in path)
        {
            Vector3 target = new Vector3(position.x, heightOffset, position.y);
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }
            gridPosition = position;
        }

        isMoving = false;
    }
}
