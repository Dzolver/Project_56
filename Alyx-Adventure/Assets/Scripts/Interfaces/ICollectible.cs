using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public interface ICollectible
    {
        CollectibleType GetCollectibleType();

        void OnCollectibleCollected();

    }

    public enum CollectibleType
    {
        None,
        Type1,
        Type2
    }
}