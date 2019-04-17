using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombie
{
    void Move();

    void ActivateAndSetPosition(Vector3 position, Transform Parent);

    float GetMoveSpeed();

    void Deactivate();
}