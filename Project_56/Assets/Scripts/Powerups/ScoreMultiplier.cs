﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class ScoreMultiplier : BasePowerup
    {
        [SerializeField]
        private int Duration;

        [SerializeField]
        private int Multiplier;

        public override int GetPowerupDuration()
        {
            return Duration;
        }

        public int GetMultiplier()
        {
            return Multiplier;
        }
    }

}
