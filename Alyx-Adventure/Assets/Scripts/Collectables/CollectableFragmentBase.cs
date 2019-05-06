﻿using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFragmentBase : MonoBehaviour {
   
    public void ActivateAndSetPosition(Vector2 pos)
    {
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

    public void OnFragmentCollected()
    {
        MyEventManager.Instance.OnFragmentCollected.Dispatch(this);
        DeactivateFragment();
    }

}