using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectible
{
    CollectibleType CType;

    public void OnCollectibleCollected()
    {
       
    }

    public CollectibleType GetCollectibleType()
    {
        return CType;
    }

}
