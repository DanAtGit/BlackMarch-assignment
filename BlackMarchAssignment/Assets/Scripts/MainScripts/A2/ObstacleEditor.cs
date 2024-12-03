using UnityEngine;
using UnityEditor;

public class ObstacleEditor : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Editor")]
    public static void OpenEditor()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Editor", EditorStyles.boldLabel);

        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleData), false); // Select the ObstacleData asset

        if (obstacleData == null) return;

        // Render the grid of toggle buttons
        for (int x = 0; x < 10; x++)
        {
            GUILayout.BeginHorizontal();
            for (int z = 0; z < 10; z++)
            {
                bool isBlocked = obstacleData.obstacleGrid[x, z];
                bool newStatus = GUILayout.Toggle(isBlocked, "");
                if (newStatus != isBlocked)
                {
                    obstacleData.obstacleGrid[x, z] = newStatus;
                    EditorUtility.SetDirty(obstacleData); 
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
