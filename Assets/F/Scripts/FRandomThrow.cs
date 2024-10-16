using UnityEngine;

public class FRandomThrow : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 15f;
    public GameObject origin;  // จุดที่ลูกเต๋าจะถูกโยนออกมา

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not assigned on the dice!");
        }

        if (origin == null)
        {
            Debug.LogError("Origin is not assigned!");
        }
    }

    void Update()
    {
        // ตรวจสอบว่ากด Spacebar และโยนลูกเต๋า
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar Pressed!");
            ThrowObject();
        }
    }

    public void ThrowObject()
    {
        // ตรวจสอบว่า Origin มีการตั้งค่าไว้แล้ว
        if (origin == null)
        {
            Debug.LogError("Origin is not assigned! Cannot throw dice.");
            return;
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody is not assigned! Cannot apply force to dice.");
            return;
        }

        float randomMagnitude = Random.Range(minForce, maxForce);

        // ตั้งค่าการหมุนและตำแหน่งการโยน
        transform.rotation = origin.transform.rotation;
        Vector3 force = randomMagnitude * (origin.transform.forward - origin.transform.right);
        transform.position = origin.transform.position + origin.transform.right;

        rb.velocity = Vector3.zero;  // ตั้งค่าให้ความเร็วเป็น 0 ก่อนโยน
        rb.AddForce(force, ForceMode.Impulse);  // ใช้แรงในการโยนลูกเต๋า

        // หมุนลูกเต๋าแบบสุ่ม
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        Debug.Log("Dice thrown with force: " + force);
    }
}