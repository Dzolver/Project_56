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
                ShowMenu();
        }

        public void PlayAgain()
        {
            MyEventManager.Instance.UpdateState.Dispatch(GameState.Game);

            HideMenu();
        }

        public void ReturnToMain()
        {
            HideMenu();
            MyEventManager.Instance.UpdateState.Dispatch(GameState.MainMenu);
            //GameStateManager.Instance.UpdateState(GameState.MainMenu);
        }
    }
}