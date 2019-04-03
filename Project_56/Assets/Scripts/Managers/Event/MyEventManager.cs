using UnityEngine;

namespace Project56
{
    public class MyEventManager : SingletonMonoBehaviour<MyEventManager>
    {
        public MyEvent<Vector3> OnPlatformGenerated = new MyEvent<Vector3>();
        public MyEvent OnJumpClicked = new MyEvent();
        public MyEvent OnFallOrSlideClicked = new MyEvent();
        public MyEvent OnAttackClicked = new MyEvent();
        public MyEvent OnObjectInstantiated = new MyEvent();
        public MyEvent<Direction> ChangeMoveDirection = new MyEvent<Direction>();
        public MyEvent<int,int> OnScoreUpdated = new MyEvent<int,int>();
        public MyEvent<int> OnEnemyKilled = new MyEvent<int>();

        //public MyEvent<GameState> UpdateState = new MyEvent<GameState>();
       // public MyEvent OnGameStateChanged = new MyEvent();
        public MyEvent<PowerupType> OnPowerupCollected = new MyEvent<PowerupType>();
        public MyEvent<CollectibleType> OnCollectibleCollected = new MyEvent<CollectibleType>();
    }
}