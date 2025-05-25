using UnityEngine;

public class ButtonUIBClick : ButtonUIAb
{
    protected override void Start()
    {
        base.Start();
        ButtonUI.Btn.onClick.AddListener(Click);
    }

    private void Click()
    {
        OnClick();
    }

    protected virtual void OnClick()
    {
        //Override
    }
}