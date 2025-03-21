using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : GMono
{
    private static SavingManager instance;

    public static SavingManager Instance => instance;

    public CharacterSO characterSO;
    private string filePath;
    public static bool IsLoaded;

    protected override void Awake() {
        base.Awake();
        if(instance != null) return;

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        filePath = Application.persistentDataPath + "/playerdata.json";
        LoadCharacterSO();
        //LoadPlayerData();
    }

    protected void LoadCharacterSO()
    {
        if(characterSO != null) return;

        characterSO = Resources.Load<CharacterSO>("Character/Character");
    }

    public void SavePlayerData() {
        PlayerData playerData = new PlayerData {
            IsNew = characterSO.IsNew,
            Level = characterSO.Level,
            CurrentExp = characterSO.CurrentExp,
            RemainPoints = characterSO.RemainPoints,
            Power = characterSO.Power,
            Magic = characterSO.Magic,
            Strength = characterSO.Strength,
            Defense = characterSO.Defense,
            Dexterity = characterSO.Dexterity,
            SkillPoints = characterSO.SkillPoints,
            QSkill = new CurrentSkillData(characterSO.QSkill.Level, characterSO.QSkill.TreeIndex, characterSO.QSkill.NodeName),
            ESkill = new CurrentSkillData(characterSO.ESkill.Level, characterSO.ESkill.TreeIndex, characterSO.ESkill.NodeName),
            SpaceSkill = new CurrentSkillData(characterSO.SpaceSkill.Level, characterSO.SpaceSkill.TreeIndex, characterSO.SpaceSkill.NodeName),
            SkillTreeLevels = new List<SkillLevelData>(characterSO.SkillTreeLevels)
        };

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Game Saved!");
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadPlayerData()
    {
        if(IsLoaded) return;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            characterSO.Level = data.Level;
            characterSO.CurrentExp = data.CurrentExp;
            characterSO.RemainPoints = data.RemainPoints;
            characterSO.Power = data.Power;
            characterSO.Magic = data.Magic;
            characterSO.Strength = data.Strength;
            characterSO.Defense = data.Defense;
            characterSO.Dexterity = data.Dexterity;
            characterSO.SkillPoints = data.SkillPoints;
            characterSO.QSkill.Level = data.QSkill.Level;
            characterSO.QSkill.TreeIndex = data.QSkill.TreeIndex;
            characterSO.QSkill.NodeName = data.QSkill.NodeName;
            characterSO.ESkill.Level = data.ESkill.Level;
            characterSO.ESkill.TreeIndex = data.ESkill.TreeIndex;
            characterSO.ESkill.NodeName = data.ESkill.NodeName;
            characterSO.SpaceSkill.Level = data.SpaceSkill.Level;
            characterSO.SpaceSkill.TreeIndex = data.SpaceSkill.TreeIndex;
            characterSO.SpaceSkill.NodeName = data.SpaceSkill.NodeName;
            characterSO.SkillTreeLevels = data.SkillTreeLevels;

            Debug.Log("Game Loaded!");
        }
        else Debug.Log("No Save File Found, Creating New Data!");

        IsLoaded = true;
    }

}