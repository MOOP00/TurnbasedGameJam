using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isTurnBasedMode = false;

    private CharacterController controller;
    private float moveSpeed = 4f;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    private float gravity = 9.81f;

    private static PlayerController instance;

    void Awake()
    {
        // ใช้ DontDestroyOnLoad สำหรับทั้ง Player 1 และ Player 2
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isTurnBasedMode)
        {
            Move();
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
        velocity.y -= Time.deltaTime * gravity;

        if (dir.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            controller.Move(velocity);
        }
    }

    public void EnterTurnBasedMode()
    {
        isTurnBasedMode = true;
        Debug.Log("Entering Turn-Based Mode");
    }
}