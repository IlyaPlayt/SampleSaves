using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;


class PlayerData
{
    public int _level;
    public int _exp;


    public PlayerData(int level, int exp)
    {
        _level = level;
        _exp = exp;
    }
}

public class PlayerParameters : MonoBehaviour
{
    static public PlayerParameters Instance;

    [SerializeField] private PortalInfo _portalInfo;
    private string fileName = "PlayerData.json";
    private string levelFileName = "LevelData.json";

    [SerializeField] private GameObject portal;

    private int defaultHealth = 100;

    private int _health;
    private int _level;
    private int _exp;

    public int Health => _health;

    private int _copperExp = 1;
    private int _ironExp = 2;
    private int _goldExp = 3;

    [SerializeField] private Text _healthUI;
    [SerializeField] private Text _levelUI;


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
        if (_level >= _portalInfo.PlayerLevelToOpenPortal)
        {
            portal.SetActive(true);
        }
        else
        {
            portal.SetActive(false);
        }
    }

    public void AddExp(Inventory.Metal metal)
    {
        switch (metal)
        {
            case Inventory.Metal.iron:
                _exp += _ironExp;
                break;
            case Inventory.Metal.copper:
                _exp += _copperExp;
                break;
            case Inventory.Metal.gold:
                _exp += _goldExp;
                break;
        }

        while (_exp >= 10)
        {
            _level++;
            _exp -= 10;
        }

        if (_level >= _portalInfo.PlayerLevelToOpenPortal)
        {
            portal.SetActive(true);
        }

        UpdateUI();
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _healthUI.text = _health.ToString();
        _levelUI.text = _level.ToString();
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData(_level, _exp);
        string serializeData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + fileName, serializeData);
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + fileName;
        if (!File.Exists(path))
            return;
        var data = File.ReadAllText(path);
        var playerData = JsonConvert.DeserializeObject<PlayerData>(data);

        _level = playerData._level;
        _exp = playerData._exp;

        path = Application.persistentDataPath + levelFileName;
        if (!File.Exists(path))
            return;
        data = File.ReadAllText(path);
        var levelInfo = JsonConvert.DeserializeObject<LevelInfo>(data);
        if (levelInfo._levelName == LevelData.Instance.SceneName)
        {
            transform.position = new Vector3(levelInfo._position.x, levelInfo._position.y, levelInfo._position.z);
            _health = levelInfo._health;
        }
        else
        {
            transform.position = Vector3.zero;
            _health = defaultHealth;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}