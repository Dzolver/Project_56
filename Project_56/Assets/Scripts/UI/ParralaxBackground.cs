using UnityEngine;
namespace Project56
{


    public class ParralaxBackground : MonoBehaviour
    {

        //public float speed = 1.0f;
        //Renderer myRenderer;
        //Vector2 offset = Vector2.zero;
        //private Vector2 savedOffset;

        //private void Start()
        //{
        //    myRenderer = GetComponent<Renderer>();
        //    savedOffset = myRenderer.sharedMaterial.GetTextureOffset("_MainTex");
        //}

        //private void Update()
        //{
        //    float x = Mathf.Repeat(Time.time * speed, 1);
        //    Vector2 offset = new Vector2(x, savedOffset.y);
        //    Debug.Log("Offset-" + x);
        //    myRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        //}
        public float scrollSpeed;
        public float tileSizeZ;
        private Direction direction;

        private Vector3 startPosition;

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
            startPosition = transform.position;
        }

        void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + (Vector3.left * (int)direction) * newPosition;
        }
    }
}