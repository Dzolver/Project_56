using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    public class DeathMenu : Menu
    {
        private void Start()
        {
            ShowMenu();
            MyEventManager.Instance.DeactivatePooledObjects.Dispatch();
        }

        public void PlayAgain()
        {
            HideMenu();
            SceneManager.LoadScene(2);
        }

        public void ReturnToMain()
        {
            HideMenu();
            SceneManager.LoadScene(1);
        }
    }
}