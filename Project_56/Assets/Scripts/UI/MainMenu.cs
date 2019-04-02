using UnityEngine.SceneManagement;

namespace Project56
{
    public class MainMenu : Menu
    {
        //private void OnEnable()
        //{
        //    MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        //}

        //private void OnDisable()
        //{
        //    if (MyEventManager.Instance != null)
        //    {
        //        MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
        //    }
        //}

        //private void OnGameStateChanged()
        //{
        //    if (GameStateManager.Instance.CurrentState == GameState.MainMenu)
        //        ShowMenu();
        //}

        public void Play()
        {
            HideMenu();
            SceneManager.LoadScene(2);
        }

        public void Leaderboard()
        {
        }

        public void Shop()
        {
        }

        public void Achievements()
        {
        }
    }
}