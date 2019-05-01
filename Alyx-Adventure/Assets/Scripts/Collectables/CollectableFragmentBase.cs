using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFragmentBase : MonoBehaviour {
    
    [SerializeField]
    float minsToUnlock; 
     
    public float GetMinsToUnlock()
    {
        return minsToUnlock;
    }

    public void OnFragmentCollected()
    {
        MyEventManager.Instance.OnFragmentCollected.Dispatch(this);
        DeactivateFragment();
    }

    public void ActivateAndSetPosition(Vector2 pos, Transform parent)
    {
        transform.SetParent(parent);
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void DeactivateFragment()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.Player)) {
            OnFragmentCollected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameStrings.MainCamera))
        {
            DeactivateFragment();
        }
    }
}