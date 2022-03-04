using UnityEngine;

public class playerLook : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] float sensitivity = 3;
    // extension value
    [SerializeField] float aimSensitivity = 15;
    [SerializeField] float maxRot = 80;
    [SerializeField] float minRot = -80;
    [SerializeField] Transform mainCamera;
    [SerializeField] bool cursorVisible = true;
    [SerializeField] bool cursorLockState = true;
    float xRotation = 0f;
    public weaponAim aim;
    private void Start()
    {
        Cursor.visible = cursorVisible;
        if (cursorLockState)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Update()
    {
        GameManager.Instance.mouseX *= aim.isAiming ? aimSensitivity / 100 * Time.deltaTime : sensitivity / 100 * Time.deltaTime;
        GameManager.Instance.mouseY *= aim.isAiming ? aimSensitivity / 100 * Time.deltaTime : sensitivity / 100 * Time.deltaTime;

        xRotation += GameManager.Instance.mouseY;

        xRotation = Mathf.Clamp(xRotation, minRot, maxRot);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * GameManager.Instance.mouseX);
    }
}
