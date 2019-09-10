using UnityEngine;

public class Follow : MonoBehaviour
{
    private Vector3 offset;

    void Awake()
    {
        offset = Camera.main.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position - offset;
    }

}
