using UnityEngine;

namespace InfiniteMap
{
    public class UIKeyBoard : GMono
    {
        private void Update()
        {
            OpenChacterUI();
            OpenInventoryUI();
        }

        private void OpenChacterUI()
        {
            if(!InputManager.Instance.CPressed) return;

            Game.Instance.Player.IsUIOpening = true;

            PlayerMenuUI.Instance.gameObject.SetActive(true);
            PlayerMenuUI.Instance.SetCurrentOption(1);
        }

        private void OpenInventoryUI()
        {
            if(!InputManager.Instance.BPressed) return;

            Game.Instance.Player.IsUIOpening = true;

            PlayerMenuUI.Instance.gameObject.SetActive(true);
            PlayerMenuUI.Instance.SetCurrentOption(0);
        }
    }
}