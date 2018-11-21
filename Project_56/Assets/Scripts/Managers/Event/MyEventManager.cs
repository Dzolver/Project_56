using UnityEngine;

namespace Project56
{
    public class MyEventManager : SingletonMonoBehaviour<MyEventManager>
    {
        public MyEvent<Vector3> OnPlatformGenerated = new MyEvent<Vector3>();
        public MyEvent OnJumpClicked = new MyEvent();
        public MyEvent OnGameStateChanged = new MyEvent();
        public MyEvent<PowerupType> OnPowerupCollected = new MyEvent<PowerupType>();
        public MyEvent<CollectibleType> OnCollectibleCollected = new MyEvent<CollectibleType>();
    }
}