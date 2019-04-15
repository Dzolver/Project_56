using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWave : MonoBehaviour
{

    public void ActivateAndSetPosition(Vector2 position, Quaternion rotation)
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (!t.gameObject.activeInHierarchy)
                t.gameObject.SetActive(true);
        }       
    }

    private void DeactivateWave()
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
