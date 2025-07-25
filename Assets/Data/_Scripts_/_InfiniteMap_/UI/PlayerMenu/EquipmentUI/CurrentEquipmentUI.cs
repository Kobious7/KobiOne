using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CurrentEquipmentUI : GMono
{
    [SerializeField] private List<EquipUI> currentEquipments;
    [SerializeField] private EquipUI currentEquip;

    public EquipUI CurrentEquip
    {
        get => currentEquip;
        set => currentEquip = value;
    }

    [SerializeField] private List<EquipmentInvUISpawner> equipInvSpawners;
    [SerializeField] private EquipmentInvUISpawner currentSpawner;

    public EquipmentInvUISpawner CurrentSpawner
    {
        get => currentSpawner;
        set => currentSpawner = value;
    }

    [SerializeField] private Transform close;

    private Equipment equipment;
    private Inventory inventory;
    private List<InventoryEquip> weaponList;
    private List<InventoryEquip> helmetList;
    private List<InventoryEquip> armorList;
    private List<InventoryEquip> armwearList;
    private List<InventoryEquip> bootsList;
    private List<InventoryEquip> specialList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCurrentEquipments();
        LoadEquipInvSpawners();
        LoadClose();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private void LoadCurrentEquipments()
    {
        if (currentEquipments.Count > 0) return;

        currentEquipments = GetComponentsInChildren<EquipUI>().ToList();
    }

    private void LoadEquipInvSpawners()
    {
        if (equipInvSpawners.Count > 0) return;

        equipInvSpawners = transform.parent.GetComponentsInChildren<EquipmentInvUISpawner>().ToList();
    }

    private void LoadClose()
    {
        if (close != null) return;

        close = transform.parent.Find("Close");
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        equipment = InfiniteMapManager.Instance.Equipment;
        inventory = InfiniteMapManager.Instance.Inventory;
        weaponList = inventory.WeaponList;
        helmetList = inventory.HelmetList;
        armorList = inventory.ArmorList;
        armwearList = inventory.ArmwearList;
        bootsList = inventory.BootsList;
        specialList = inventory.SpecialList;

        SpawnEquipInventory();
        ShowCurrentEquipments();
        equipment.Unequip.OnEquipDisarming += UpdateUnequippedEquipment;
        inventory.EquipWearing.OnEquipWearing += UpdateEquippedEquipment;
        inventory.OnEquipAdding += UpdateEquipUIInSpawner;
        inventory.Soulize.OnEquipSoulized += DespawnSoulizedEquip;
    }

    private void ShowCurrentEquipments()
    {
        for (int i = 0; i < currentEquipments.Count; i++)
        {
            if (i == 0)
            {
                ShowCurrentEquip(equipment.Weapon, i);
            }
            if (i == 1)
            {
                ShowCurrentEquip(equipment.Helmet, i);
            }
            if (i == 2)
            {
                ShowCurrentEquip(equipment.Armor, i);
            }
            if (i == 3)
            {
                ShowCurrentEquip(equipment.Armwear, i);
            }
            if (i == 4)
            {
                ShowCurrentEquip(equipment.Boots, i);
            }
            if (i == 5)
            {
                ShowCurrentEquip(equipment.Special, i);
            }

            int index = i;
            currentEquipments[i].Button.onClick.AddListener(() => Click(currentEquipments[index], index));
        }
    }

    private void SpawnEquipInventory()
    {
        for (int i = 0; i < equipInvSpawners.Count; i++)
        {
            if (i == 0)
            {
                equipInvSpawners[0].SpawnEquips(weaponList);
            }
            if (i == 1)
            {
                equipInvSpawners[1].SpawnEquips(helmetList);
            }
            if (i == 2)
            {
                equipInvSpawners[2].SpawnEquips(armorList);
            }
            if (i == 3)
            {
                equipInvSpawners[3].SpawnEquips(armwearList);
            }
            if (i == 4)
            {
                equipInvSpawners[4].SpawnEquips(bootsList);
            }
            if (i == 5)
            {
                equipInvSpawners[5].SpawnEquips(specialList);
            }
        }
    }

    private void ShowCurrentEquip(InventoryEquip equip, int index)
    {
        if (equip.Level > 0)
        {
            currentEquipments[index].ShowEquip(equip);
        }
        else
        {
            currentEquipments[index].ShowEmpty();
        }
    }

    private void UpdateEquippedEquipment(InventoryEquip equip)
    {
        int index = GetEquipIndex(equip.EquipSO.EquipType);

        ShowCurrentEquip(equip, index);
        equipInvSpawners[index].DespawnEquip(equip);
    }

    private void UpdateUnequippedEquipment(InventoryEquip equip)
    {
        int index = GetEquipIndex(equip.EquipSO.EquipType);

        InventoryEquip newEquip = new InventoryEquip();

        ShowCurrentEquip(newEquip, index);
        equipInvSpawners[index].SpawnEquip(equip);
    }

    public int GetEquipIndex(EquipType equipType)
    {
        if (equipType == EquipType.Weapon) return 0;
        else if (equipType == EquipType.Helmet) return 1;
        else if (equipType == EquipType.Armor) return 2;
        else if (equipType == EquipType.Armwear) return 3;
        else if (equipType == EquipType.Boots) return 4;
        else if (equipType == EquipType.Special) return 5;
        else return 6;
    }

    private void Click(EquipUI equipUI, int index)
    {
        close.gameObject.SetActive(true);
        if (currentEquip != null) currentEquip.OnSelected.gameObject.SetActive(false);

        equipUI.OnSelected.gameObject.SetActive(true);

        currentEquip = equipUI;

        EquipmentDetailsUI.Instance.gameObject.SetActive(true);
        if (equipUI.Equip.Level > 0)
        {
            EquipmentDetailsUI.Instance.ShowDetails(equipUI.Equip, true);
            EquipmentDetailsUI.Instance.AddUnequipClickListener(equipUI.Equip);
        }
        else
        {
            if (EquipmentDetailsUI.Instance.gameObject.activeSelf) EquipmentDetailsUI.Instance.gameObject.SetActive(false);
        }

        if (currentSpawner != null)
        {
            currentSpawner.gameObject.SetActive(false);
            if (currentSpawner.CurrentEquip != null && currentSpawner.CurrentEquip.OnSelected.gameObject.activeSelf)
            {
                currentSpawner.CurrentEquip.OnSelected.gameObject.SetActive(false);
                currentSpawner.CurrentEquip = null;
            }
        }

        equipInvSpawners[index].gameObject.SetActive(true);

        currentSpawner = equipInvSpawners[index];
    }

    private void UpdateEquipUIInSpawner(InventoryEquip equip)
    {
        int index = GetEquipIndex(equip.EquipSO.EquipType);

        equipInvSpawners[index].SpawnEquip(equip);
    }

    private void DespawnSoulizedEquip(InventoryEquip equip)
    {
        int index = GetEquipIndex(equip.EquipSO.EquipType);

        equipInvSpawners[index].DespawnEquip(equip);
    }
}