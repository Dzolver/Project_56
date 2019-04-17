using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class Invincibility : BasePowerup
    {
        [SerializeField]
        private int Duration;

        public override int GetPowerupDuration()
        {
            return Duration;
        }
    }

}