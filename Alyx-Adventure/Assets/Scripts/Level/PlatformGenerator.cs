using AlyxAdventure;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, PlatformWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void OnEnable()
    {
        MyEventManager.Instance.OnEnemyGenerated.AddListener(OnEnemyGenerated);
        MyEventManager.Instance.OnPowerupGenerated.AddListener(OnPowerupGenerated);
        MyEventManager.Instance.OnCoinWaveGenerated.AddListener(OnCoinWaveGenerated);

    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnEnemyGenerated.RemoveListener(OnEnemyGenerated);
            MyEventManager.Instance.OnPowerupGenerated.RemoveListener(OnPowerupGenerated);
            MyEventManager.Instance.OnCoinWaveGenerated.RemoveListener(OnCoinWaveGenerated);
        }
    }

    private void Start()
    {
        CurrentPlatform = ObjectPool.Instance.GetPlatform(PlatformType.VeryEasy);
        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);
        CurrentPlatformWidth = CurrentPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;

        LeftPlatform = GetPlatform();
        PlatformWidth = LeftPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;
        LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - PlatformWidth, startPoint.position.y));

        RightPlatform = GetPlatform();
        RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + CurrentPlatformWidth, startPoint.position.y));

    }

    private void FixedUpdate()
    {
        if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > CurrentPlatformWidth)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > 0)
            ActivateLeftPlatform();

    }

    private Platform GetPlatform()
    {
        PlatformType type;
        if (GameData.Instance.MinutesSinceGame <= 0.5f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.VeryEasy, (int)PlatformType.Easy + 1);
        }
        else if (GameData.Instance.MinutesSinceGame <= 1.5f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.VeryEasy, (int)PlatformType.Average + 1);
        }
        else if (GameData.Instance.MinutesSinceGame <= 3f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.Easy, (int)PlatformType.Hard + 1);
        }
        else
            type = (PlatformType)Random.Range((int)PlatformType.Average, (int)PlatformType.Hard + 1);

        return ObjectPool.Instance.GetPlatform(type);
    }

    private void ActivateRightPlatform()
    {
        PlatformWidth = RightPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;
        Platform platform = GetPlatform();
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + PlatformWidth, startPoint.position.y));
        LeftPlatform.Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
        CurrentPlatformWidth = PlatformWidth;
    }

    private void ActivateLeftPlatform()
    {
        Platform platform = GetPlatform();
        PlatformWidth = platform.GetComponent<BoxCollider2D>().size.x - 0.01f;
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - PlatformWidth, startPoint.position.y));
        RightPlatform.Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
        CurrentPlatformWidth = CurrentPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
    }

    private void OnEnemyGenerated(AbstractEnemy enemy)
    {
        Platform parent;
        if (GameData.Instance.direction == Direction.Right)
            parent = RightPlatform;
        else
            parent = LeftPlatform;
        MyEventManager.Instance.OnGotEnemyParent.Dispatch(enemy, parent);
    }

    private void OnPowerupGenerated(BasePowerup powerup)
    {
        Platform parent;
        if (GameData.Instance.direction == Direction.Right)
            parent = RightPlatform;
        else
            parent = LeftPlatform;
        Vector2 pos = parent.GetPowerupPoint().position;
        Debug.Log("Power up  activated;Parent = " + parent + " pos = " + pos);
        powerup.ActivateAndSetPosition(pos, parent.transform);
    }


    private void OnCoinWaveGenerated(CoinWave coinwave)
    {
        Vector2 pos;
        Quaternion rotation;
        Platform parent;
        do
        {
            if (GameData.Instance.direction == Direction.Right)
            {
                rotation = Quaternion.identity;
                parent = RightPlatform;
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                parent = LeftPlatform;
            }
            pos = parent.GetCoinWavePoint().position;
        }
        while (Mathf.Abs(pos.x - GameData.Instance.theRunnerTransform.position.x) < 13f);

        coinwave.ActivateAndSetPosition(pos, rotation, parent.transform);
    }


}