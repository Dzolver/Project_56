using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEffect : MonoBehaviour
{
    float time;
    private Renderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        time = 0;
        gameObject.SetActive(true);
        mRenderer.material.mainTextureOffset = new Vector2(0, 0.3f);
        LeanTween.value(0.3f, 0f, 2).setOnUpdate(UpdateOffset).setOnComplete(OnComplete);        
    }

    private void UpdateOffset(float yOffset)
    {
        mRenderer.material.mainTextureOffset = new Vector2(0, yOffset);
    }

    private void OnComplete()
    {
        StartCoroutine(ChangeOffset());
    }

    private IEnumerator ChangeOffset()
    {
        while (time < 10)
        {
            mRenderer.material.mainTextureOffset = mRenderer.material.mainTextureOffset + new Vector2(0.07f * (int)GameData.Instance.direction, 0f);
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        LeanTween.value(0f, 0.3f, 2).setOnUpdate(UpdateOffset).setOnComplete(Deactivate);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
