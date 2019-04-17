using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWave : MonoBehaviour
{

    public void ActivateAndSetPosition(Vector2 position, Quaternion rotation, Transform parent)
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        gameObject.transform.SetParent(parent);
        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (!t.gameObject.activeInHierarchy)
                t.gameObject.SetActive(true);
        }
    }

    public void DeactivateWave()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.MainCamera))
        {
            DeactivateWave();
        }
    }
}
