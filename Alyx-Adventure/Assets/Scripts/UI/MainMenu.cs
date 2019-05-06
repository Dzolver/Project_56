using UnityEngine.SceneManagement;
using TMPro;

namespace AlyxAdventure
{
    public class MainMenu : Menu
    {
        public TextMeshProUGUI FragmentCount;

        private void Start()
        {
            FragmentCount.text = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.FragmentCount, 0).ToString();
        }
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