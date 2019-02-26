using UnityEngine;

namespace Project56
{
    public class HUD : Menu
    {
        public PlayerController playerCtrl;
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


        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
        }

        private void OnGameStateChanged()
        {
            if (GameStateManager.Instance.CurrentState == GameState.Game)
            {
                ShowMenu();
                Time.timeScale = 1;
            }
            else
                HideMenu();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Paused);
        }
    }
}