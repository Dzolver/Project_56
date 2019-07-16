
using AlyxAdventure;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, PlatformWidth;

    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;
    private PlatformType type = PlatformType.Easy;

    private void OnEnable()
    {
        MyEventManager.Instance.OnEnemyGenerated.AddListener(OnEnemyGenerated);
        MyEventManager.Instance.OnPowerupGenerated.AddListener(OnPowerupGenerated);
        MyEventManager.Instance.OnCoinWaveGenerated.AddListener(OnCoinWaveGenerated);
        MyEventManager.Instance.OnFragmentGenerated.AddListener(OnFragmentGenerated);
        MyEventManager.Instance.OnQuarterMinutePassed.AddListener(OnMinutesPassed);
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnEnemyGenerated.RemoveListener(OnEnemyGenerated);
            MyEventManager.Instance.OnPowerupGenerated.RemoveListener(OnPowerupGenerated);
            MyEventManager.Instance.OnCoinWaveGenerated.RemoveListener(OnCoinWaveGenerated);
            MyEventManager.Instance.OnFragmentGenerated.RemoveListener(OnFragmentGenerated);
            MyEventManager.Instance.OnQuarterMinutePassed.RemoveListener(OnMinutesPassed);

        }
    }

    private void Start()
    {
        CurrentPlatform = ObjectPool.Instance.GetPlatform(PlatformType.Easy);
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
        {
            ActivateRightPlatform();
        }
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > 0)
        {
            ActivateLeftPlatform();
        }
    }

    private void OnMinutesPassed(float mins)
    {
        if (mins == 0.25f)
        {
            type = PlatformType.Average;
        }
        else if (mins == 0.75f)
        {
            type = PlatformType.Hard;
        }
        else if (mins >= 1.5f)
        {
            type = PlatformType.VeryHard;
        }
    }

    private Platform GetPlatform()
    {
        int random = Random.Range((int)type - 1 == -1 ? 0 : (int)type - 1, (int)type + 1);
        return ObjectPool.Instance.GetPlatform((PlatformType)random);
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

        if (parent.GetPowerupPoint() != null)
        {
            Vector2 pos = parent.GetPowerupPoint().position;

            if (Mathf.Abs(pos.x - GameData.Instance.theRunnerTransform.position.x) > 13f)
                powerup.ActivateAndSetPosition(pos, parent.transform);
        }
    }


    private void OnCoinWaveGenerated(CoinWave coinwave)
    {
        Vector2 pos;
        Quaternion rotation;
        Platform parent;

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

        if (parent.GetCoinWavePoint() != null)
        {
            pos = parent.GetCoinWavePoint().position;

            if (Mathf.Abs(pos.x - GameData.Instance.theRunnerTransform.position.x) > 13f)
            {
                coinwave.ActivateAndSetPosition(pos, rotation, parent.transform);
            }
        }
    }


    private void OnFragmentGenerated(CollectableFragmentBase fragment)
    {
        Platform parent;
        if (GameData.Instance.direction == Direction.Right)
            parent = RightPlatform;
        else
            parent = LeftPlatform;

        if (parent.FragmentPoint != null)
        {
            Vector2 pos = parent.FragmentPoint.position;
            fragment.ActivateAndSetPosition(pos);
        }
    }


}