using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public float moveSpeed = 2f;
    public float heightOffset = 1.55f;
    public bool isMoving = false;           // Flag to track if the player is currently moving

    [HideInInspector]
    public Vector2Int gridPosition;

    void Start()
    {
        gridPosition = new Vector2Int(0, 0); 
        transform.position = new Vector3(gridPosition.x, heightOffset, gridPosition.y);
    }

    void Update()
    {
        // Disable input while moving
        if (isMoving) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector2Int targetPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.z));

            if (targetPosition.x >= 0 && targetPosition.x < 10 && targetPosition.y >= 0 && targetPosition.y < 10)
            {
                List<Vector2Int> path = pathfinding.FindPath(gridPosition, targetPosition);
                if (path != null)
                    StartCoroutine(MoveAlongPath(path));
            }
        }
    }

    IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        isMoving = true;                        // Set the flag to true to disable input

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

        isMoving = false;                       // Re-enable input after movement completes
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;

        return Vector3.zero;
    }
}
