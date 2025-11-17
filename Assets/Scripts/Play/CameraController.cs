using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;  // 플레이어 캐릭터
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float maxPitch = 90f; // 위쪽 제한
    [SerializeField] private float minPitch = -90f; // 아래쪽 제한

    private float pitch = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 수직 회전 (Pitch)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // 수평 회전 (Yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
