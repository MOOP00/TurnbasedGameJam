using UnityEngine;

public class FRandomThrow : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 15f;

    public GameObject origin;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ThrowObject();
        }
    }  

    public void ThrowObject()
    {
        float randomMagnitude = Random.Range(minForce, maxForce);
        transform.rotation = origin.transform.rotation;
        Vector3 force = randomMagnitude * (origin.transform.forward - origin.transform.right);
        transform.position = origin.transform.position + origin.transform.right;
        rb.velocity = Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(Random.Range(0, 360),Random.Range(0, 360),Random.Range(0, 360));
    }
}
