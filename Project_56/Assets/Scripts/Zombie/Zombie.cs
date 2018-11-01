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
            transform.SetPositionAndRotation(new Vector3(position.x, position.y - 0.5f, position.z), Quaternion.identity);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void GetMoveSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            transform.Translate(Vector3.left * Time.deltaTime * m_MoveSpeed);
        }

        // Use this for initialization
        private void Start()
        {
            //m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision  " + collision.gameObject.name);
            if (collision.gameObject.tag.Equals("Player"))
            {
                Deactivate();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Trigger Exit  " + collision.gameObject.name);

            if (collision.gameObject.tag.Equals("MainCamera"))
            {
                Deactivate();
            }
        }
    }
}