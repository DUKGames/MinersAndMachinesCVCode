using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Camera camera;

    public float GetOrthographicSize()
    {
        return camera.orthographicSize;
    }

    public void SetOrthographicSize(float value)
    {
        camera.orthographicSize = value;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return camera.transform.position;
    }

    private void Awake()
    {
        camera = Camera.main;
    }


}
