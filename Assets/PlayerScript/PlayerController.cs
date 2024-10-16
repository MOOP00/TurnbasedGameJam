using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isTurnBasedMode = false; // ตัวแปรสำหรับการสลับโหมด Turn-Based

    private CharacterController controller;
    private float moveSpeed = 4f;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    private float gravity = 9.81f;

    private static PlayerController instance;

    void Awake()
    {
        // ป้องกันไม่ให้ PlayerController ถูกทำลายเมื่อเปลี่ยน Scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ผู้เล่นสามารถขยับได้เฉพาะเมื่อไม่อยู่ในโหมด Turn-Based
        if (!isTurnBasedMode)
        {
            Move();
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // การเคลื่อนที่ซ้ายขวา
        float vertical = Input.GetAxisRaw("Vertical");      // การเคลื่อนที่เดินหน้าถอยหลัง

        // ไม่ต้องกลับทิศทางในแกน Z และแกน X
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
        velocity.y -= Time.deltaTime * gravity;

        if (dir.magnitude >= 0.1f)
        {
            // หมุนตัวละครให้หันไปตามทิศทางการเดิน
            transform.rotation = Quaternion.LookRotation(dir);
            controller.Move(velocity);
        }
    }

    // ฟังก์ชันที่จะถูกเรียกเมื่อเข้าสู่โหมด Turn-Based
    public void EnterTurnBasedMode()
    {
        isTurnBasedMode = true; // ปิดการขยับของผู้เล่น
        Debug.Log("Entering Turn-Based Mode");
    }
}