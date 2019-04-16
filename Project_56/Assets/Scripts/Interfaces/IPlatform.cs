using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public interface IPlatform
    {
        void ActivateAndSetPosition(Vector3 position);

        void Deactivate();

        PlatformType GetPlatformType();
    }
}
