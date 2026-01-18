using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{ 
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = -20f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;

    public Transform orientation;

    Vector3 velocity;
    bool grounded;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private PlayerCam playerCam;

    void Start()
    {
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!IsOwner) return;

        GroundCheck();
        HandleMovement();
        playerCam.HandleCameraRotation();
    }

    void GroundCheck()
    {
        grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            playerHeight * 0.5f + 0.2f,
            whatIsGround
        );

        if (grounded && velocity.y < 0f)
            velocity.y = 0f;
    }

    public void HandleMovement()
    {
        Vector2 input = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 move =
            orientation.forward * input.y +
            orientation.right * input.x;

        rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
    }
}
