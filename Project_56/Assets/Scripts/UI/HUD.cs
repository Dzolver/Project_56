using UnityEngine;

namespace Project56
{
    public class HUD : Menu
    {
        public PlayerController playerCtrl;

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
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

        private void OnGameStateChanged()
        {
            if (GameStateManager.Instance.CurrentState == GameState.Game)
            {
                ShowMenu();
                Time.timeScale = 1;
            }        
        }

        public void Pause()
        {
            HideMenu();
            Time.timeScale = 0;
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Paused);
        }
    }
}