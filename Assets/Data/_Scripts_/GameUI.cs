using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameUI : GMono
{
    private static GameUI instance;

    public static GameUI Instance => instance;

    [SerializeField] private PlayerMenuUI playerMenuUI;

    public PlayerMenuUI PlayerMenuUI => playerMenuUI;

    [SerializeField] private CurrentEquipmentUI currentEquipmentUI;

    public CurrentEquipmentUI CurrentEquipmentUI => currentEquipmentUI;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 GameUI is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerMenuUI();
        LoadCurrentEquipmentUI();
    }

    private void LoadPlayerMenuUI()
    {
        if(playerMenuUI != null) return;

        playerMenuUI = FindObjectOfType<PlayerMenuUI>();
    }

    private void LoadCurrentEquipmentUI()
    {
        if(currentEquipmentUI != null) return;

        currentEquipmentUI = FindObjectOfType<CurrentEquipmentUI>();
    }
}