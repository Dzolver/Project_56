using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour,IBlock
{
    public void ActivateAndSetPosition(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, Quaternion.Euler(new Vector3(0,0,0)));
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MainCamera"))
        {
            Deactivate();
        }
    }
}
