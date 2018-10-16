using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombie
{
    void Move();

    void ActivateAndSetPosition(Vector3 position);

    void GetMoveSpeed();

    void Deactivate();
}