using Project56;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public Transform Runner;

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
            StartCoroutine(GenerateBlocks());
        }
    }

    private IEnumerator GenerateBlocks()
    {
        while (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            BlockBase block;
            float y;
            int blockType = UnityEngine.Random.Range(0, 2);
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f));
            if (blockType == 0)
            {
                block = ObjectPool.Instance.GetJumpBlock().GetComponent<BlockBase>();
                y = -0.6f;
            }
            else
            {
                block = ObjectPool.Instance.GetSlideBlock().GetComponent<BlockBase>();
                y = 0.2f;
            }
            block.ActivateAndSetPosition(new Vector2(Runner.position.x + (Camera.main.GetComponent<CameraController>().direction * 20), y));
        }
    }
}
