using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuUI : GMono
{
    private static PlayerMenuUI instance;

    public static PlayerMenuUI Instance => instance;

    [SerializeField] private List<PlayerMenuOptionUI> playerMenu;
    [SerializeField] private Button next;
    [SerializeField] private Button prev;
    [SerializeField] private Button closeBtn;
    [SerializeField] private int currentIndex;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 PlayerMenuUI is allowed to exist!");

        instance = this;

        transform.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerMenu();
        LoadNextButton();
        LoadPrevButton();
        LoadCloseButton();
    }

    protected override void Start()
    {
        base.Start();
        AddClickListeners();
    }

    private void LoadPlayerMenu()
    {
        if(playerMenu.Count > 0) return;

        playerMenu = GetComponentsInChildren<PlayerMenuOptionUI>().ToList();
    }

    private void LoadNextButton()
    {
        if(next != null) return;

        next = transform.Find("Next").GetComponent<Button>();
    }

    private void LoadPrevButton()
    {
        if(prev != null) return;

        prev = transform.Find("Prev").GetComponent<Button>();
    }

    private void LoadCloseButton()
    {
        if(closeBtn != null) return;

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
    }

    private void AddClickListeners()
    {
        next.onClick.AddListener(() => NextClick(currentIndex + 1));
        prev.onClick.AddListener(() => PrevClick(currentIndex - 1));
        closeBtn.onClick.AddListener(CloseBtnClick);
    }

    private void NextClick(int index)
    {
        playerMenu[currentIndex].gameObject.SetActive(false);

        int trueIndex = index == playerMenu.Count ? 0 : index;

        playerMenu[trueIndex].gameObject.SetActive(true);

        currentIndex = trueIndex;
    }

    private void PrevClick(int index)
    {
        playerMenu[currentIndex].gameObject.SetActive(false);

        int trueIndex = index <= 0 ? playerMenu.Count - 1 : index;

        playerMenu[trueIndex].gameObject.SetActive(true);

        currentIndex = trueIndex;
    }

    public void SetCurrentOption(int index)
    {
        playerMenu[currentIndex].gameObject.SetActive(false);

        int trueIndex = index;

        playerMenu[trueIndex].gameObject.SetActive(true);

        currentIndex = trueIndex;
    }

    private void ResetOption()
    {
        playerMenu[currentIndex].gameObject.SetActive(false);

        playerMenu[0].gameObject.SetActive(true);

        currentIndex = 0;
    }

    private void CloseBtnClick()
    {
        transform.gameObject.SetActive(false);
        ResetOption();
        
        Game.Instance.Player.IsUIOpening = false;
    }
}