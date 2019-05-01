using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    public class GameOverPanel : MonoBehaviour
    {

        public TextMeshProUGUI scoreText, HighScoreText;

        void Start()
        {
            HighScoreText.SetActive(false);
            MyEventManager.Instance.DeactivatePooledObjects.Dispatch();
            LeanTween.value(0f, ScoreManager.Instance.GetScore(), .5f).setOnUpdate(OnUpdate);
            if (ScoreManager.Instance.GetScore() > PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.Highscore, 0))
            {
                AnimateHighscore();
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.Highscore, ScoreManager.Instance.GetScore());
            }
        }

        private void OnUpdate(float value)
        {
            scoreText.text = (int)value + "";
        }

        private void AnimateHighscore()
        {
            HighScoreText.SetActive(true);
            LeanTween.value(.8f, 1f, .5f).setOnUpdate(OnValueChanged).setLoopPingPong();
        }

        private void OnValueChanged(float value)
        {
            HighScoreText.gameObject.transform.localScale = new Vector3(value, value, 0);
        }

        public void PlayAgain()
        {
            LeanTween.cancelAll();
            SceneManager.LoadScene(2);
        }

        public void ReturnToMain()
        {
            LeanTween.cancelAll();
            SceneManager.LoadScene(1);
        }
    }


}