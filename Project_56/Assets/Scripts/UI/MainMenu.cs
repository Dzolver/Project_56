namespace Project56
{
    public class MainMenu : Menu
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
            if (GameStateManager.Instance.CurrentState == GameState.MainMenu)
                ShowMenu();
        }

        public void Play()
        {
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Game);
            HideMenu();
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