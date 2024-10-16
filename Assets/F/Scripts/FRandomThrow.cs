using UnityEngine;

public class FRandomThrow : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 15f;
    public GameObject origin;

    private Rigidbody rb;
    private bool hasThrown = false;  // เพิ่มตัวแปรเพื่อตรวจสอบว่าลูกเต๋าถูกโยนแล้วหรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // ซ่อนลูกเต๋าในช่วงเริ่มต้น
        gameObject.SetActive(false);  // ซ่อนลูกเต๋า
    }

    void Update()
    {
        // เมื่อกด Space และยังไม่ได้ทอยลูกเต๋า
        if (Input.GetKeyDown(KeyCode.Space) && !hasThrown)
        {
            // ทำให้ลูกเต๋าปรากฏและโยนลูกเต๋า
            gameObject.SetActive(true);  // แสดงลูกเต๋า
            ThrowObject();
        }
    }

    public void ThrowObject()
    {
        if (origin == null)
        {
            Debug.LogError("Origin is not assigned!");
            return;
        }

        hasThrown = true;  // ป้องกันการโยนซ้ำ
        float randomMagnitude = Random.Range(minForce, maxForce);
        transform.rotation = origin.transform.rotation;
        Vector3 force = randomMagnitude * (origin.transform.forward - origin.transform.right);
        transform.position = origin.transform.position + origin.transform.right;
        rb.velocity = Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }
}