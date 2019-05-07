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

        public TextMeshProUGUI scoreText, HighScoreText, Kills;

        void Start()
        {
            HighScoreText.SetActive(false);
            Kills.SetActive(false);
            MyEventManager.Instance.DeactivatePooledObjects.Dispatch();
            LeanTween.value(0f, ScoreManager.Instance.GetScore(), .5f).setOnUpdate(OnUpdate).setOnComplete(OnComplete);
        }

        private void OnComplete()
        {
            if (ScoreManager.Instance.GetKills() > 0)
            {
                Kills.SetActive(true);
                Kills.text = "+" + ScoreManager.Instance.GetKills() + " Kills";
                LeanTween.moveY(Kills.rectTransform, 650f, 1f).setOnComplete(AddToScore);
            }
            else
            {
                CheckHighScore(ScoreManager.Instance.GetScore());
            }
        }

        private void AddToScore()
        {
            Kills.SetActive(false);
            Debug.Log(ScoreManager.Instance.GetKills() * ScoreManager.Instance.ScorePerKill);
            int score = ScoreManager.Instance.GetScore() + (ScoreManager.Instance.GetKills() * ScoreManager.Instance.ScorePerKill);

            LeanTween.value(ScoreManager.Instance.GetScore(), score, .5f).setOnUpdate(OnUpdate);
            CheckHighScore(score);
        }

        private void CheckHighScore(int score)
        {
            if (score > PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.Highscore, 0))
            {
                AnimateHighscore();
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.Highscore, score);
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