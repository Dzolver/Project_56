using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public interface IPowerup
    {
        PowerupType GetPowerupType();

        void OnPowerupCollected();
        void ActivatePowerup();
        void DeactivatePowerup();
        int GetPowerupDuration();
    }

    public enum PowerupType
    {
        None,
        Invincibility,
        Type2
    }

    
  
}