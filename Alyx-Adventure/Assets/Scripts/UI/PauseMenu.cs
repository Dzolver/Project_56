using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    public class PauseMenu : Menu
    {
        public Menu HUD;

        private void Start()
        {

        }

        public void Resume()
        {
            HideMenu();
            HUD.ShowMenu();
            Time.timeScale = 1;
        }

        public void ReturnToMain()
        {
            HideMenu();
            MyEventManager.Instance.DeactivatePooledObjects.Dispatch();
            SceneManager.LoadScene(1);
        }
    }
}