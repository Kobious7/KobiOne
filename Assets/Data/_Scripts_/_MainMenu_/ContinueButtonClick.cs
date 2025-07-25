using UnityEngine;

public class ContinueButtonClick : ButtonClick
{
    protected override void Click()
    {
        base.Click();
        LoadScene(INFINITEMAP);
    }
}