using UnityEngine;

namespace Project56
{
    public class HUD : Menu
    {
        public PlayerController playerCtrl;
        public Menu PauseMenu;

        private void Start()
        {
            ShowMenu();
            Time.timeScale = 1;
        }

        public void OnJumpClicked()
        {
            MyEventManager.Instance.OnJumpClicked.Dispatch();
        }

        public void OnFallOrSlideClicked()
        {
            Debug.Log("Falling");
            MyEventManager.Instance.OnFallOrSlideClicked.Dispatch();
        }

        public void OnAttackClicked()
        {
            MyEventManager.Instance.OnAttackClicked.Dispatch();
        }

        public void Pause()
        {
            HideMenu();
            PauseMenu.ShowMenu();
            Time.timeScale = 0;
        }
    }
}