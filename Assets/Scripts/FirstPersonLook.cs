using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public Transform playerBody;      // 좌우 회전 담당
    public float mouseSensitivity = 60f;

    private float xRotation = 0f;     // 상하 회전값 저장

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 상하 회전
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 좌우 회전
        playerBody.Rotate(Vector3.up * mouseX);
    }
}