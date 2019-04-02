using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project56
{
    public class PauseMenu : Menu
    {
        public Menu HUD;

        public void Resume()
        {
            HideMenu();
            HUD.ShowMenu();
            Time.timeScale = 1;
        }

        public void ReturnToMain()
        {
            HideMenu();
            SceneManager.LoadScene(1);
        }
    }
}