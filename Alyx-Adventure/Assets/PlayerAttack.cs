using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GameStrings.Enemy))
        {
            GameData.Instance.AddKills();
            collision.gameObject.GetComponent<AbstractEnemy>().Deactivate();
        }

    }
}
