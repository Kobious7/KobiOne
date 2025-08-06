using UnityEngine;

public class MainMenuManager : GMono
{
    private static MainMenuManager instance;

    public static MainMenuManager Instance => instance;

    [SerializeField] private NewGamePromptUI newGamePromptUI;
    [SerializeField] private NameSetUI nameSetUI;

    public NewGamePromptUI NewGamePromptUI => newGamePromptUI;
    public NameSetUI NameSetUI => nameSetUI;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 MainMenuManager");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (newGamePromptUI == null) newGamePromptUI = FindObjectOfType<NewGamePromptUI>();
        if (nameSetUI == null) nameSetUI = FindObjectOfType<NameSetUI>();
    }
}