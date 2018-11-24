using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
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