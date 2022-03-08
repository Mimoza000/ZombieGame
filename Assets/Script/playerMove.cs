using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Value")]
    float gravity = -19.62f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.5f;
    float jumpHeight = 2;
    [SerializeField] LayerMask groundMask;

    float playerSpeed;
    float sprintSpeed = 5;
    float walkSpeed = 3;
    Vector3 velocity;
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
    public void Move(Vector2 direction)
    {
        Vector3 localDirection = transform.right * direction.x + transform.forward * direction.y;
        controller.Move(localDirection * playerSpeed * Time.deltaTime);
    }

    public void Gravity(bool isJumped)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (isGrounded && isJumped)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Sprint(bool Trigger)
    {
        if (Trigger) playerSpeed = sprintSpeed;
        else playerSpeed = walkSpeed;
    }
}
