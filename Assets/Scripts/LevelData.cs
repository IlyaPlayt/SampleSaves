
using System.IO;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct position
{
    public float x;
    public float y;
    public float z;
}

public class LevelInfo
{
    public int _health;
    public position _position;
    public string _levelName;

    public LevelInfo(int health, position position, string levelName)
    {
        _health = health;
        _position = position;
        _levelName = levelName;
    }
}

public class LevelData : MonoBehaviour
{
    [SerializeField] private PortalInfo _portalInfo;
    public static LevelData Instance;

    public string SceneName;
    private string fileName = "LevelData.json";
    private string resourcesFileName = "ResourceData.json";

    [SerializeField] private ResourcesController _resourcesController;
    [SerializeField] private PlayerParameters _playerParameters;
    [SerializeField] private Inventory _inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        SceneName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneName);
    }

    private void SaveData()
    {
        position pos;
        pos.x = _playerParameters.transform.position.x;
        pos.y = _playerParameters.transform.position.y;
        pos.z = _playerParameters.transform.position.z;
        LevelInfo info = new LevelInfo(_playerParameters.Health, pos, SceneName);
        string serializeData = JsonConvert.SerializeObject(info);
        File.WriteAllText(Application.persistentDataPath + fileName, serializeData);
    }

    public void LoadNextLevel()
    {
        _playerParameters.SaveData();
        _inventory.SaveData();
        File.Delete(Application.persistentDataPath + SceneName+ resourcesFileName);
        File.Delete(Application.persistentDataPath + fileName);

        SceneManager.LoadScene(_portalInfo.NextLevelName);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}