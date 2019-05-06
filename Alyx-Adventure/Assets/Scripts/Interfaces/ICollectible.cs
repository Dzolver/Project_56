using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public interface ICollectible
    {
        CollectibleType GetCollectibleType();

        void OnCollectibleCollected();

        void ActivateAndSetPosition(Vector2 pos);
        void Deactivate();
    }

    public enum CollectibleType
    {
        None,
        Type1,
        Type2
    }
}