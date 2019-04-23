using AlyxAdventure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBlock : MonoBehaviour
{
    CircleCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponentInChildren<CircleCollider2D>();
    }

    private void OnEnable()
    {
        MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
        ToLeft();
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
            //MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
        }
    }

    private void OnPowerupCollected(BasePowerup powerup)
    {
        if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            myCollider.isTrigger = true;
    }

    private void OnPowerupExhausted(BasePowerup powerup)
    {
        if (myCollider.isTrigger)
            myCollider.isTrigger = false;
    }

    private void ToLeft()
    {
        LeanTween.value(gameObject, OnRotationChanged, -45f, 45f, 2f).setOnComplete(ToRight);
    }

    private void OnRotationChanged(float val)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, val));
    }

    private void ToRight()
    {
        LeanTween.value(gameObject, OnRotationChanged, 45f, -45f, 2f).setOnComplete(ToLeft);
    }

}
