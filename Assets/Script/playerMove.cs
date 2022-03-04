using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Value")]
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public float jumpHeight = 2;
    public LayerMask groundMask;

    public float playerSpeed = 0;
    public float sprintSpeed = 5;
    public float walkSpeed = 3;

    Vector3 velocity;
    bool isGrounded;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveJump();
        Sprint();
    }

    void MoveJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        if (GameManager.Instance.jump > 0 && isGrounded)
        {
            velocity.y = Mathf.Sqrt(GameManager.Instance.jump * -2f * gravity);
        }

        Vector3 move = transform.forward * GameManager.Instance.vertical + transform.right * GameManager.Instance.horizotal;
        controller.Move(move * playerSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (GameManager.Instance.sprint == 1 && GameManager.Instance.vertical > 0)
        {
            playerSpeed = sprintSpeed;
        }
        else
        {
            playerSpeed = walkSpeed;
        }
    }
}
