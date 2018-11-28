using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    public void ActivateAndSetPosition(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }

    public void Deactivate()
    {
        Debug.Log("Deactivating - " + name);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Deactivate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MainCamera"))
        {
            Deactivate();
        }
    }
}
