using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.5f;
    [SerializeField] float jumpHeight = 15;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float playerSpeed;
    [SerializeField] float sprintSpeed = 5;
    [SerializeField] float walkSpeed = 3;
    Vector3 velocity = Vector3.zero;
    bool isGrounded;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<CharacterController>(out controller);
        playerSpeed = walkSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction)
    {
        direction = transform.right * direction.x + transform.forward * direction.z;
        controller.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
            return;
        }
        else if (isGrounded) velocity.y = jumpHeight;
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;
        controller.Move(velocity);
    }

    public void Sprint(bool Trigger)
    {
        if (Trigger) playerSpeed = sprintSpeed;
        else playerSpeed = walkSpeed;
    }
}
