using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    void OnDie();

    void Deactivate();

    void ActivateAndSetPosition(Vector3 vector3);
}