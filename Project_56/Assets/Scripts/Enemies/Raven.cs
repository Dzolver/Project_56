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
            Debug.Log("Direction = " + direction + "\nValue = " + (Vector3.left * Time.deltaTime * GetMoveSpeed() * (int)direction));
        }
    }

}