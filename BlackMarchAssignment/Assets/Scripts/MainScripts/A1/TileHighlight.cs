using UnityEngine;
using TMPro;
public class TileHighlight : MonoBehaviour
{
    public TMP_Text positionDisplayText; 

    void Update()
    {
        DetectTile();
    }
#region Tile detection function through raycast
    void DetectTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TileInf tileInfo = hit.collider.GetComponent<TileInf>();
            if (tileInfo != null)
            {
                positionDisplayText.text = $"Tile: ({tileInfo.x}, {tileInfo.z})";
            }
        }
    }
}
#endregion