using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    //[RequireComponent(typeof(Rigidbody2D))]
    public class Zombie :  AbstractEnemy
    {
        public Rigidbody2D myRigidbody;

        public override void Move(Direction direction)
        {
            myRigidbody.velocity = new Vector2(GetMoveSpeed() * (int)direction * (-1), myRigidbody.velocity.y);
        }
    }
}