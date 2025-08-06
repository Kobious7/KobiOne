using UnityEngine;

public class NewGameButtonClick : ButtonClick
{
    protected override void Click()
    {
        base.Click();

        if (SavingManager.Instance.IsDataExist)
        {
            MainMenuManager.Instance.NewGamePromptUI.gameObject.SetActive(true);
        }
        else
        {
            SavingManager.Instance.SavePlayerData();
            MainMenuManager.Instance.NameSetUI.gameObject.SetActive(true);
        }
    }
}