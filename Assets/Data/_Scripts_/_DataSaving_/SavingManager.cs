using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : GMono
{
    private static SavingManager instance;

    public static SavingManager Instance => instance;

    public PlayerSO playerSO;
    public PlayerSO defaultSO;
    private string filePath;
    [SerializeField] private bool isLoaded;
    [SerializeField] private bool isDataExist;

    public bool IsDataExist => isDataExist;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;

        instance = this;

        DontDestroyOnLoad(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        filePath = Application.persistentDataPath + "/playerdata.json";
        if (playerSO == null) playerSO = Resources.Load<PlayerSO>("Player/Player");
        if (defaultSO == null) defaultSO = Resources.Load<PlayerSO>("Player/Default");
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData(playerSO);

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Game Saved!");
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadPlayerData()
    {
        if (isLoaded) return;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            playerSO.SetPlayerSO(data);

            isDataExist = true;

            Debug.Log("Game Loaded!");

        }
        else Debug.Log("No Save File Found!");

        isLoaded = true;
    }

    public void ResetDataToDefault()
    {
        File.Delete(filePath);
        playerSO = defaultSO;
    }
}