using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectible
{
    CollectibleType CType;

    public void ActivateAndSetPosition(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.Player))
        {
            OnCollectibleCollected();
        }
    }

    public void OnCollectibleCollected()
    {
        MyEventManager.Instance.OnCollectibleCollected.Dispatch(CType);
        Deactivate();
    }

    public CollectibleType GetCollectibleType()
    {
        return CType;
    }

}
