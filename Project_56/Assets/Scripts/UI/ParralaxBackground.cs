using UnityEngine;

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

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + (-Vector3.right) * newPosition;
    }
}
