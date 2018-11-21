using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public interface IPowerup
    {
        PowerupType GetPowerupType();

        void OnPowerupCollected();
    }

    public enum PowerupType
    {
        None,
        Type1,
        Type2
    }
}