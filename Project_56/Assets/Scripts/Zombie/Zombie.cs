using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    //[RequireComponent(typeof(Rigidbody2D))]
    public class Zombie : MonoBehaviour, IZombie
    {
        //private Rigidbody2D m_Rigidbody;
        private float m_MoveSpeed = 2.0f;

        public void ActivateAndSetPosition(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            if (GameData.Instance.direction == Direction.Left)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public float GetMoveSpeed()
        {
            return m_MoveSpeed;
        }

        public void Move()
        {
            transform.Translate(Vector3.left * Time.deltaTime * m_MoveSpeed);
        }

        private void Start()
        {
            //m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Move();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {




        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameStrings.MainCamera))
            {
                Deactivate();
            }
            if (collision.gameObject.CompareTag(GameStrings.Weapon))
            {
                GameData.Instance.AddKills();
                Deactivate();
            }
        }
    }
}