using System;
using UnityEngine;

namespace AlyxAdventure
{
    public class MenuManager : SingletonMonoBehaviour<MenuManager>
    {
        
        public Menu CurrentMenu = null;
        public Menu PreviousMenu = null;

        private Menu PauseMenu;

        public void Start()
        {
            if (CurrentMenu != null)
                ShowMenu(CurrentMenu);
            else
                Debug.Log("Please Assign an initial menu first");
        }
    
        public void ShowMenuAndHideCurrent(Menu menuToShow)
        {
            if (CurrentMenu.IsModal)
            {
                HideMenu(PreviousMenu);
            }
            PreviousMenu = CurrentMenu;
            CurrentMenu = menuToShow;
            HideMenu(PreviousMenu);
            ShowMenu(menuToShow);
        }

        private void ShowMenu(Menu menuToShow)
        {
            menuToShow.ShowMenu();
        }

        private void HideMenu(Menu menuToHide)
        {
            menuToHide.HideMenu();
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    Debug.Log("Pressed Escape");
            //    if (GameStateManager.Instance.CurrentState == GameState.Game)
            //        PauseGame();
            //    else if (GameStateManager.Instance.CurrentState == GameState.Paused)
            //        ResumeGame();
            //}
        }

        //private void PauseGame()
        //{
        //    Time.timeScale = 0;
        //    GameStateManager.Instance.UpdateState(GameState.Paused);
        //    PauseMenu.ShowMenu();
        //}

        //private void ResumeGame()
        //{
        //    Time.timeScale = 1;
        //    GameStateManager.Instance.UpdateState(GameState.Game);
        //}
    }
}