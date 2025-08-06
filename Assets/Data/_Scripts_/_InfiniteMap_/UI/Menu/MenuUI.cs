using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : GMono
{
    [SerializeField] private TextMeshProUGUI playerName, level;
    [SerializeField] private Transform menu;
    [SerializeField] private Button faceBtn, logOutBtn;
    private InfiniteMapManager infiniteMapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (playerName == null) playerName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        if (level == null) level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        if (menu == null) menu = transform.Find("Menu");
        if (logOutBtn == null) logOutBtn = menu.Find("LogOut").GetComponent<Button>();
        if (faceBtn == null) faceBtn = transform.Find("Face").GetComponent<Button>();

        menu.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        infiniteMapManager = InfiniteMapManager.Instance;
        infiniteMapManager.Player.StatsSystem.OnLevelIncreasing += UpdateLevel;

        level.text = $"{infiniteMapManager.Player.StatsSystem.Level}";
        playerName.text = infiniteMapManager.PlayerData.PlayerName.Length >= 1 ? infiniteMapManager.PlayerData.PlayerName : "KobiOne";

        faceBtn.onClick.AddListener(OpenMenuClickListener);
        logOutBtn.onClick.AddListener(LogOutClickListener);
    }

    private void UpdateLevel(int lev)
    {
        level.text = $"{lev}";
    }

    private void OpenMenuClickListener()
    {
        menu.gameObject.SetActive(true);
    }

    private void LogOutClickListener()
    {
        if (SavingManager.Instance != null)
        {
            SavingManager.Instance.SavePlayerData();
        }

        LoadScene(MAINMENU);
    }
}