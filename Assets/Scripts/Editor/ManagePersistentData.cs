using System.IO;
using UnityEditor;
using UnityEngine;

public class ManagePersistentData : EditorWindow
{
    [MenuItem("SavedData/Delete PlayerPrefs")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("SavedData/Delete PersistentDataPath files")]
    static void DeletePersistentDataPathFiles()
    {
        foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }

        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
    }

    [MenuItem("SavedData/Open PersistentDataPath")]
    private static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
