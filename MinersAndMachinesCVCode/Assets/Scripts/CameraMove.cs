using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraHandler cameraHandler;

    [Header("Variables")]
    [SerializeField] private bool isCameraMovable;
    [SerializeField] private float mouseMoveSensitivity = 1f;
    [SerializeField] private float moveCameraSpeed = 5f;
    [SerializeField] private float maxDownPosition = 0f;

    private Vector3 newCameraPosition;
    private bool isCameraMoving;

    private float ratioMultiplier;
    private float moveRange;
    private float maxDownPositionResized;

    public event EventHandler OnCameraWentToDestination;

    public void SetIsCameraMovable(bool newValue)
    {
        isCameraMovable = newValue;
    }

    public void SetNewXCameraPosition(float newXPosition)
    {
        float canRatio = ratioMultiplier * (moveRange - cameraHandler.GetOrthographicSize());
        float clampedXPosition = Mathf.Clamp(newXPosition, -canRatio, canRatio);

        newCameraPosition = new Vector3(clampedXPosition, newCameraPosition.y, -10f);
        isCameraMoving = true;
    }

    public void CalculateMaxDownPositionResized()
    {
        maxDownPositionResized = maxDownPosition - (5f - cameraHandler.GetOrthographicSize());
    }

    public CameraHandler GetCameraHandler()
    {
        return cameraHandler;
    }

      public void CalculateMoveRange(float width, float height)
    {
        float calculated = 1920f / width * height / 2f;
        calculated = calculated / 100.1f;
        calculated = calculated * 0.94f;
        calculated -= 0.1f;
        moveRange = calculated;
    }

    private void Awake()
    {
        CalculateMaxDownPositionResized();
    }

    private void Update()
    {
        if (!isCameraMovable) return;

        CheckClickMove();
        MoveCamera();
    }

    // Need change to new Input System
    private void CheckClickMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            SetNewYCameraPosition(cameraHandler.GetPosition().y + mouseMoveSensitivity, true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            SetNewYCameraPosition(cameraHandler.GetPosition().y - mouseMoveSensitivity, false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            SetNewXCameraPosition(cameraHandler.GetPosition().x - mouseMoveSensitivity);
        }

        if (Input.GetKey(KeyCode.D))
        {
            SetNewXCameraPosition(cameraHandler.GetPosition().x + mouseMoveSensitivity);
        }
    }

    private void SetNewYCameraPosition(float newYPosition, bool goingUp)
    {
        if (goingUp)
        {
            if (cameraHandler.GetPosition().y == 0) return;
        }
        else
        {
            if (cameraHandler.GetPosition().y == maxDownPositionResized) return;
        }

        float checkedYPosition = Mathf.Clamp(newYPosition, maxDownPositionResized, 0);

        newCameraPosition = new Vector3(newCameraPosition.x, checkedYPosition, -10f);
        isCameraMoving = true;
    }

    private void MoveCamera()
    {
        if (!isCameraMoving) return;
        if (Vector3.Distance(cameraHandler.GetPosition(), newCameraPosition) < 0.01)
        {   
            isCameraMoving = false; 
            OnCameraWentToDestination?.Invoke(this, EventArgs.Empty);
            return; 
        }

        cameraHandler.SetPosition((Vector3.Lerp(cameraHandler.GetPosition(), newCameraPosition, moveCameraSpeed) * Time.deltaTime));
    }
}
