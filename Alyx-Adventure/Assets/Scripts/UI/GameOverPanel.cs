using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
namespace AlyxAdventure
{
    public class GameOverPanel : MonoBehaviour
    {

        public TextMeshProUGUI scoreText;
        // Use this for initialization
        void Start()
        {
            LeanTween.value(0f, ScoreManager.Instance.GetScore(), .5f).setOnUpdate(OnUpdate);
        }

        private void OnUpdate(float value)
        {
            scoreText.text = (int)value + "";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}