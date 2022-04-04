using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class InventoryData
{
    public int _copperCount;
    public int _ironCount;
    public int _goldCount;

    public InventoryData(int copperCount, int ironCount, int goldCount)
    {
        _copperCount = copperCount;
        _ironCount = ironCount;
        _goldCount = goldCount;
    }
}
public class Inventory : MonoBehaviour
{ 
    public enum Metal 
    {
        iron,
        copper,
        gold
    }
    
    static  public  Inventory Instance;
    private int _copperCount;
    private int _ironCount;
    private int _goldCount;
    
    [SerializeField] private Text _copperUI;
    [SerializeField] private Text _ironUI;
    [SerializeField] private Text _goldUI;

    private string fileName = "InventoryData.json";

   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        LoadData();
        UpdateUI();
    }

    public void AddResource(Metal metal)
    {
        switch (metal)
        {
            case Metal.iron:
                _ironCount++;
                break;
            case Metal.copper:
                _copperCount++;
                break;
            case Metal.gold:
                _goldCount++;
                break;
        }

        UpdateUI();
        
    }

    private void UpdateUI()
    {
        _copperUI.text = _copperCount.ToString();
        _ironUI.text = _ironCount.ToString();
        _goldUI.text = _goldCount.ToString();
    }
    public void SaveData()
    {
        InventoryData data = new InventoryData(_goldCount,_ironCount,_goldCount);
        string serializeData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + fileName, serializeData);
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath  + fileName;
        if (!File.Exists(path))
            return;
        var data = File.ReadAllText(path);
        var inventoryData = JsonConvert.DeserializeObject<InventoryData>(data);
        _copperCount = inventoryData._copperCount;
        _ironCount = inventoryData._ironCount;
        _goldCount = inventoryData._goldCount;

    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
