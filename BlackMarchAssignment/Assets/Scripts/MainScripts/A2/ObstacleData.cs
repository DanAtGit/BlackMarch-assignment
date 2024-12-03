using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Grid/ObstacleData")] //Asset Menu Tab
public class ObstacleData : ScriptableObject
{
    public bool[,] obstacleGrid = new bool[10, 10];
}