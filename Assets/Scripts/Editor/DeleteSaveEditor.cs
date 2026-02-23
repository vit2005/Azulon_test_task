using UnityEngine;
using UnityEditor;
using System.IO;

public static class DeleteSaveEditor
{
    private const string SaveFileName = "player_data.json";

    [MenuItem("Tools/Delete Save File")]
    public static void DeleteSave()
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Save file deleted: {path}");
        }
        else
        {
            Debug.LogWarning($"Save file not found: {path}");
        }
    }
}