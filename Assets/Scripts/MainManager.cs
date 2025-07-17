using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color TeamColor;

    private static string COLOR_DATA_FILE_PATH => Application.persistentDataPath + "/saveColorFile.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        /*
        Equivalent to the following:
            SaveData data = new SaveData();
            data.teamColor = TeamColor;
        */
        SaveData data = new()
        {
            teamColor = TeamColor
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(COLOR_DATA_FILE_PATH, json);
    }

    public void LoadColor()
    {
        if (File.Exists(COLOR_DATA_FILE_PATH))
        {
            string json = File.ReadAllText(COLOR_DATA_FILE_PATH);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            TeamColor = data.teamColor;
        }
    }
}
