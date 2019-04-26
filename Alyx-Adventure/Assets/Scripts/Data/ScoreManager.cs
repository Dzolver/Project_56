using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {

        private int GameScore;
        public int ScorePerSecond;


        public void UpdateScore(int multiplier)
        {
            int previousScore = GameScore;
            GameScore += multiplier;
                MyEventManager.Instance.OnScoreUpdated.Dispatch(previousScore, GameScore);
        }

        public int GetScore()
        {
            return GameScore;
        }
    }

}