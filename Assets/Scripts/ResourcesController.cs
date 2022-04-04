using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _resources;
    
    private string fileName = "ResourceData.json";

    private void Start()
    {
        LoadData();
    }

    private void SaveData()
    {
       
        List<bool> resourcesState = new List<bool>();
        foreach (var obj in _resources)
        {
         resourcesState.Add(obj.activeInHierarchy);
        }
        string serializeData = JsonConvert.SerializeObject(resourcesState);
        File.WriteAllText(Application.persistentDataPath +LevelData.Instance.SceneName+ fileName, serializeData);
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + LevelData.Instance.SceneName+ fileName;
        if(!File.Exists(path))
            return;
        var data =File.ReadAllText(path);
        var resourcesState=JsonConvert.DeserializeObject<List<bool>>(data);
        for (int i = 0; i < _resources.Count; i++)
        {
         _resources[i].SetActive(resourcesState[i]);   
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
    
}