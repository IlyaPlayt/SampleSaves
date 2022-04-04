
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private string recourseFileName = "ResourceData.json";
    private string playerFileName = "PlayerData.json";
    private string levelFileName = "LevelData.json";
    private string inventiryFileName = "InventoryData.json";

    public void StartGame()
    {
        string name="Level";
        for (int i = 1; i <= 2; i++)
        {
            File.Delete(Application.persistentDataPath+name+i+recourseFileName);
            Debug.Log(name+i.ToString());
        }
      
        File.Delete(Application.persistentDataPath + playerFileName);
        File.Delete(Application.persistentDataPath + levelFileName);
        File.Delete(Application.persistentDataPath+inventiryFileName);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath +  levelFileName;
        if (!File.Exists(path))
            StartGame();
        var data = File.ReadAllText(path);
        var levelData = JsonConvert.DeserializeObject<LevelInfo>(data);
        SceneManager.LoadScene(levelData._levelName);
    }
}