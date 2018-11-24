﻿using UnityEngine;

namespace Project56
{
    public class MyEventManager : SingletonMonoBehaviour<MyEventManager>
    {
        public MyEvent<Vector3> OnPlatformGenerated = new MyEvent<Vector3>();
        public MyEvent OnJumpClicked = new MyEvent();
        public MyEvent OnFallClicked = new MyEvent();
        public MyEvent OnGameStateChanged = new MyEvent();
    }
}