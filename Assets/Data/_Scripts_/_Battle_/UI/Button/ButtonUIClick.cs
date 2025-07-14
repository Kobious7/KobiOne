using UnityEngine;

public class ButtonUIBClick : ButtonUIBAb
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