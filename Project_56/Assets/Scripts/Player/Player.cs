using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour, IPlayer
    {
        public bool attacked;

        public void ActivateAndSetPosition(Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void OnDie()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Zombie"))
            {
                if (!attacked)
                {
                    Deactivate();
                    GameStateManager.Instance.UpdateState(GameState.Death);
                }

            }
            if (collision.gameObject.tag.Equals("Block"))
            {
                Deactivate();
                GameStateManager.Instance.UpdateState(GameState.Death);
            }
        }

    }
}