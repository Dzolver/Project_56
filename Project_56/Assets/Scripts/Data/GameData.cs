using Project56;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : SingletonMonoBehaviour<GameData>
{


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
        MyEventManager.Instance.ChangeMoveDirection.AddListener(ChangeMoveDirection);

    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
            MyEventManager.Instance.ChangeMoveDirection.RemoveListener(ChangeMoveDirection);

        }
    }

    private void ChangeMoveDirection(Direction direction)
    {
        this.direction = direction;
    }

    private void OnGameStateChanged()
    {
        if (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            CurrentObjectPosX = theRunnerTransform.position.x;
        }
        if (GameStateManager.Instance.CurrentState == GameState.Death)
        {
            SceneManager.LoadScene(1);
        }
    }

    public float GetNextObjectPosX()
    {
        PreviousObjectPosX = CurrentObjectPosX;
        float distance = UnityEngine.Random.Range(18f, 25f);
        CurrentObjectPosX = theRunnerTransform.position.x + ((int)direction * distance);
        if (Mathf.Abs(PreviousObjectPosX - CurrentObjectPosX) < 7f)
        {
            CurrentObjectPosX += (int)direction * 7f;
        }
        return CurrentObjectPosX;
    }
}

public enum Direction
{
    Left = -1, Right = 1
}