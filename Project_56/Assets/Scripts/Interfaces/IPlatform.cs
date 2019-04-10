using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatform
{
    void ActivateAndSetPosition(Vector3 position);

    void Deactivate();

    int GetPlatformId();
}