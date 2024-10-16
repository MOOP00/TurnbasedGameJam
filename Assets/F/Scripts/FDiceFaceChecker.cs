using UnityEngine;

public class FDiceFaceChecker : MonoBehaviour
{
    public int value = 0;
    public Transform[] diceFaces;
    public bool moving = false;
    public Rigidbody rb;

    void Update()
    {
        CheckTopFace();

        if (rb.velocity.magnitude < 0.001)
            moving = false;
        else
            moving = true;
    }

    void CheckTopFace()
    {
        Transform topFace = null;
        float maxDot = -1f;
        int count = 0;

        foreach (Transform face in diceFaces)
        {
            float dot = Vector3.Dot(face.forward, Vector3.up);
            count++;
            if (dot > maxDot)
            {
                maxDot = dot;
                topFace = face;
                value = count;
            }
        }
    }

    // Method สำหรับการโยนลูกเต๋า
    public void ThrowObject()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody ไม่ถูกตั้งค่าใน FDiceFaceChecker");
            return;
        }

        float randomForce = Random.Range(10f, 15f);
        Vector3 throwDirection = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));

        rb.AddForce(throwDirection * randomForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * randomForce, ForceMode.Impulse);
    }
}