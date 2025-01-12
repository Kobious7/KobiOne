using InfiniteMap;
using UnityEngine;
using UnityEngine.UIElements;

public class Keyboard : GMono
{
    private void Update()
    {
        if(InputManager.Instance.BPressed)
        {
            OpenAndCloseInventoryUI();
        }

        if(InputManager.Instance.CPressed)
        {
            OpenAndCloseCharacterUI();
        }
    }

    private void OpenAndCloseInventoryUI()
    {
        if(GameUI.Instance.InventoryUI.Closed)
        {
            GameUI.Instance.InventoryUI.Close();
        }
        else
        {
            GameUI.Instance.InventoryUI.Open();
        }
    }

    private void OpenAndCloseCharacterUI()
    {
        if(GameUI.Instance.CharacterUI.Closed)
        {
            GameUI.Instance.CharacterUI.Close();
        }
        else
        {
            GameUI.Instance.CharacterUI.Open();
        }
    }
}