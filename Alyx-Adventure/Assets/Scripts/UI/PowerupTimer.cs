using AlyxAdventure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTimer : MonoBehaviour
{

    public Image FillImage;

    public void ActivateTimer(int duration)
    {
        gameObject.SetActive(true);
        FillImage.type = Image.Type.Filled;
        LeanTween.value(FillImage.gameObject, OnFillValueChanged, 1, 0, duration).setOnComplete(OnPowerupExhausted);
    }

    private void OnPowerupExhausted()
    {
        MyEventManager.Instance.OnPowerupExhausted.Dispatch(GameData.Instance.GetCurrentPoweruup());
        Deactivate();
    }

    private void OnFillValueChanged(float value)
    {
        FillImage.fillAmount = value;
    }

    public void Deactivate()
    {
        LeanTween.cancel(FillImage.gameObject);
        gameObject.SetActive(false);
    }
}
