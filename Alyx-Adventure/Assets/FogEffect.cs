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
        mRenderer.material.mainTextureOffset = Vector2.zero;
        StartCoroutine(ChangeOffset());
    }

    private IEnumerator ChangeOffset()
    {
        while (time < 10)
        {
            mRenderer.material.mainTextureOffset = mRenderer.material.mainTextureOffset + new Vector2(0.01f, 0f);
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }
}
