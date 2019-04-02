using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project56
{
    public class DeathMenu : Menu
    {
        private void Start()
        {
            ShowMenu();    
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