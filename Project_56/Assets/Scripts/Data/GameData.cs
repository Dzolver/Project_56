using Project56;
using UnityEngine;

public class GameData : SingletonMonoBehaviour<GameData>
{
    public GameObject theRunner;

    public Transform theRunnerTransform;

    public float direction = 1; //-1 = left direction, 1= right direction
    public float platformWidth;
    public float CurrentObjectPosX;
    private float PreviousObjectPosX;

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
        Debug.Log("Previous : " + PreviousObjectPosX);
        float distance = Random.Range(18f, 25f);
        Debug.Log("Random: " + distance);
        Debug.Log("Runner: " + theRunnerTransform.position.x);
        CurrentObjectPosX = theRunnerTransform.position.x + (direction * distance);
        Debug.Log("CurrentObjectPos: " + CurrentObjectPosX);
        if (Mathf.Abs(PreviousObjectPosX - CurrentObjectPosX) < 7f)
        {
            CurrentObjectPosX += direction * 7f;
            Debug.Log("Current close to previous. New current = " + CurrentObjectPosX);
        }
        return CurrentObjectPosX;
    }
}
