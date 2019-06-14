using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.Enemy))
        {
            CameraShake.Instance.shakeDuration = .2f;
            ScoreManager.Instance.AddKills();
            collision.gameObject.GetComponent<AbstractEnemy>().Deactivate();
        }
    }
}
