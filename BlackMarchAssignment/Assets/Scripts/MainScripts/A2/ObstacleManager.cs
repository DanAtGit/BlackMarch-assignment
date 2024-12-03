using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData; 
    public GameObject obstaclePrefab; 
    private List<GameObject> activeObstacles = new List<GameObject>(); 
    void Start()
    {
        GenerateObstacles();
    }
#region Function to Generate Obstacles based on ObstacleData
    public void GenerateObstacles()
    {
        ClearObstacles();

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                if (obstacleData.obstacleGrid[x, z])
                {
                    Vector3 position = new Vector3(x, 0.5f, z); // Offset the obstacle slightly above the grid
                    GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
                    activeObstacles.Add(obstacle);
                }
            }
        }
    }

    // Clear existing obstacles
    void ClearObstacles()
    {
        foreach (var obstacle in activeObstacles)
        {
            Destroy(obstacle);
        }
        activeObstacles.Clear();
    }
}
#endregion