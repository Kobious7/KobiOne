using UnityEngine;
using UnityEngine.UI;

public class ShutdownBtnUI : GMono
{
    [SerializeField] private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (button == null) button = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(ShutdownClickListener);
    }

    private void ShutdownClickListener()
    {
       #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}