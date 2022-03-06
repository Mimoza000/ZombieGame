using UnityEngine;

public class playerLook : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] float sensitivity = 0.3f;
    [SerializeField] float aimSensitivity = 0.1f;
    [SerializeField] float maxRot = 80;
    [SerializeField] float minRot = -80;
    float xRotation;
    void Start()
    {
        xRotation = Camera.main.transform.localRotation.x;
    }
    public void Look(Vector2 direction)
    {
        direction.x *= sensitivity * Time.deltaTime;
        direction.y *= sensitivity * Time.deltaTime;

        xRotation += direction.y;
        xRotation = Mathf.Clamp(xRotation, minRot, maxRot);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * direction.x);
    }

    public void Aim(bool Trigger)
    {
        if (Trigger) return;
        else sensitivity = aimSensitivity;
    }
}
