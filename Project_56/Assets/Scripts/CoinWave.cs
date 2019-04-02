using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWave : MonoBehaviour
{

    public float length;

    public void ActivateAndSetPosition(Vector2 position)
    {
        Debug.Log("activating wave");
        gameObject.SetActive(true);
        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (!t.gameObject.activeInHierarchy)
                t.gameObject.SetActive(true);
        }
        gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
        StartCoroutine(DeactivateWave());
    }

    private IEnumerator DeactivateWave()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Deactivating wave");
        gameObject.SetActive(false);
    }

}
