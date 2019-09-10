using System.Collections;
using UnityEngine;
namespace AlyxAdventure
{

    public class ParralaxBackground : MonoBehaviour
    {

        public float scrollSpeedFactor;
        private float factor;

        private Direction direction;
        private Renderer mRenderer;

        private void Awake()
        {
            mRenderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.ChangeMoveDirection.AddListener(ChangeMoveDirection);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.ChangeMoveDirection.RemoveListener(ChangeMoveDirection);
            }
        }

        private void ChangeMoveDirection(Direction direction)
        {
            this.direction = direction;
        }

        void Start()
        {
            mRenderer.material.mainTextureOffset = Vector2.zero;
            factor = GameData.Instance.theRunner.GetComponent<PlayerController>().moveSpeed / scrollSpeedFactor;
            StartCoroutine(ChangeOffset());
        }

        private IEnumerator ChangeOffset()
        {
            while (true)
            {
                float x = GameData.Instance.RunnerVelocity.x / factor;
                mRenderer.material.mainTextureOffset = mRenderer.material.mainTextureOffset + new Vector2(/*(int)direction **/x, 0f);
                yield return 0.1f;
            }
        }

    }
}