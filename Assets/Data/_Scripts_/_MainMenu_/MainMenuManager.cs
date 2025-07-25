using UnityEngine;

public class MainMenuManager : GMono
{
    private static MainMenuManager instance;

    public static MainMenuManager Instance => instance;

    [SerializeField] private NewGamePromptUI newGamePromptUI;

    public NewGamePromptUI NewGamePromptUI => newGamePromptUI;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 MainMenuManager");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        newGamePromptUI = FindObjectOfType<NewGamePromptUI>();
    }
}