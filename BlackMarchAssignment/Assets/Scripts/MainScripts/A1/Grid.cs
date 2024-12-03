using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab; 
    public int gridSize = 10; 
    private GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }
#region Cube Generation
    void GenerateGrid()
    {
        grid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x, 0, z); 
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.name = $"Tile_{x}_{z}";
                TileInf tileInfo = tile.AddComponent<TileInf>();
                tileInfo.SetPosition(x, z);

                grid[x, z] = tile;
            }
        }
    }
}
#endregion