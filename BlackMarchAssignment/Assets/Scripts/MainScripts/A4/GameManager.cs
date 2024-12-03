using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public EnemyAI enemyAI;

    void Update()
    {
        if (!enemyAI.isMoving && Input.GetMouseButtonDown(0))
        {
            Invoke(nameof(MoveEnemy), 0.5f);            // Enemy movement delay
        }
    }

    void MoveEnemy()
    {
        enemyAI.MoveTowardsTarget(playerController.gridPosition);
    }
}
