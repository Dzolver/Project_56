using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    
    public class FastRunInvincibility : BasePowerup
    {
        [SerializeField]
        private int Duration;

        [SerializeField]
        private int Speed;

        public override int GetPowerupDuration()
        {
            return Duration;
        }

        public int GetSpeed()
        {
            return Speed;
        }
    }
   
}

