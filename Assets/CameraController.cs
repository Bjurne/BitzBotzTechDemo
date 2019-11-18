using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    new public Camera camera;

    public enum CameraType { DullCamera, ActionCamera, ZoomingActionCamera };
    public CameraType cameraType;

    private Vector3 targetPosition;
    private Vector2 mousePosition;
    private Vector2 playerPosition;

    private void Update()
    {
        playerPosition = player.transform.position;
        if (cameraType == CameraType.DullCamera )
        {
            targetPosition = playerPosition;
            targetPosition.z = -10f;
            transform.position = targetPosition;
        }
        else
        {
            mousePosition = Input.mousePosition;
            targetPosition = playerPosition + mousePosition / 100f;
            targetPosition.x -= 10f;
            targetPosition.y -= 5f;


            targetPosition.z = -10f;


            transform.position = targetPosition;

            if (cameraType == CameraType.ZoomingActionCamera)
            {
                targetPosition.y -= 2.5f;
                camera.orthographicSize = Mathf.Clamp((new Vector2(targetPosition.x, targetPosition.y * 1.6f) - playerPosition).magnitude, 6f, 10f);
                
            }
        }
    }
}
