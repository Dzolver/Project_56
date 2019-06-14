using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class Raven : AbstractEnemy
    {

        public override void Move(Direction direction)
        {
            transform.Translate(Vector3.left * Time.deltaTime * GetMoveSpeed() * (int)direction);
        }

        private void FixedUpdate()
        {
            Collider2D collide = Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0f);
            if (collide.CompareTag(GameStrings.Ground) || collide.CompareTag(GameStrings.Platform))
            {
                Debug.Log("Moving up " + gameObject.name);
                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }
        }
    }

}