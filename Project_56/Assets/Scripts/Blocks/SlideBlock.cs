using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBlock : BlockBase
{
    //public GameObject pivot;

    private void OnEnable()
    {
        ToLeft();
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
