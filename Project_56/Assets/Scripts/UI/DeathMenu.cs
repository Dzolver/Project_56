using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project56
{
    public class DeathMenu : Menu
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
            if (GameStateManager.Instance.CurrentState == GameState.Death)
            {
                ShowMenu();
                Time.timeScale = 0;
            }
                
        }

        public void PlayAgain()
        {
            HideMenu();
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Game);
        }

        public void ReturnToMain()
        {
            HideMenu();
            MyEventManager.Instance.UpdateState.Dispatch(GameState.MainMenu);
            SceneManager.LoadScene(1);            
            //MyEventManager.Instance.UpdateState.Dispatch(GameState.Main);
        }
    }
}