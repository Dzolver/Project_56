using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlock
{
    void ActivateAndSetPosition(Vector2 position);

    void Deactivate();
}
