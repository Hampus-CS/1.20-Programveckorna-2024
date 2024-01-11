using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public float shake = 0f;

    // Update is called once per frame

    void Start()
    {
        shake = 5f;
    }

    void Update()
    {
        shake += (0f - shake) * 0.1f;

        transform.position = new Vector3(0f + Random.Range(-shake, shake), 1f + Random.Range(-shake, shake), -10f);
    }
}
