using UnityEngine.SceneManagement;

namespace Project56
{
    public class PauseMenu : Menu
    {
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
            if (GameStateManager.Instance.CurrentState == GameState.Paused)
                ShowMenu();
        }

        public void Resume()
        {
            HideMenu();
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Game);
        }

        public void ReturnToMain()
        {
            HideMenu();
            MyEventManager.Instance.UpdateState.Dispatch(GameState.MainMenu);
            SceneManager.LoadScene(1);
          
        }
    }
}