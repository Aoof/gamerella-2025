using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraPosition;

    void Start()
    {
        cameraPosition.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
