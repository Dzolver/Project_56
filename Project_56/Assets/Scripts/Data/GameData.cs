using Project56;
using UnityEngine;

public class GameData : SingletonMonoBehaviour<GameData>
{
    public enum Direction
    {
        Left = -1, Right = 1
    }

    public GameObject theRunner;

    public Transform theRunnerTransform;

    public Direction direction = Direction.Right; //-1 = left direction, 1= right direction
    public float platformWidth;
    public float CurrentObjectPosX;
    private float PreviousObjectPosX;

    private void Start()
    {
        MyEventManager.Instance.UpdateState.Dispatch(GameState.Game);
    }

    private void OnEnable()
    {
        MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged()
    {
        if (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            CurrentObjectPosX = theRunnerTransform.position.x;
        }
    }

    public float GetNextObjectPosX()
    {
        PreviousObjectPosX = CurrentObjectPosX;
        float distance = Random.Range(18f, 25f);
        CurrentObjectPosX = theRunnerTransform.position.x + ((int)direction * distance);
        if (Mathf.Abs(PreviousObjectPosX - CurrentObjectPosX) < 7f)
        {
            CurrentObjectPosX += (int)direction * 7f;
        }
        return CurrentObjectPosX;
    }
}