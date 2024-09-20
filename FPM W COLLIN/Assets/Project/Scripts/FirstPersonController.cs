using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Player Settuings")]
    public float moveSpeed = 5.0f; 
    public float gravity = -9.80f;
    public float jumpForce = 1.0f;
    
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100.0f;
    public Transform playerCamera;
    public bool lockCursor = true;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0.0f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Lock the cursor if needed 
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }
    void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") = mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") = mouseSensitivity * Time.deltaTime;

        //Rotate the player left and right
        transform.Rotate(Vector3.up * mouseX);

        //Rotate the camera up & down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRoation, -90f, 90f); // limit the up/down rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure player is grounded 
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
