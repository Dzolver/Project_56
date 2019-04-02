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
        public bool isDead;

        public void ActivateAndSetPosition(Vector3 position)
        {
            gameObject.SetActive(true);
            isDead = false;
            if (GameData.Instance.direction == Direction.Right)
                transform.SetPositionAndRotation(position, Quaternion.identity);
            else
                transform.SetPositionAndRotation(position, Quaternion.Euler(new Vector3(0, 0, 180)));
        }

        public void Deactivate()
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
                gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
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
            if (collision.gameObject.tag.Equals("Weapon"))
            {
                Debug.Log("Hit");
                isDead = true;
                Deactivate();
            }

            else if (collision.gameObject.tag.Equals("Player"))
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
}