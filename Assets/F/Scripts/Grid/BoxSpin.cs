using System.Collections;
using UnityEngine;

public class BoxSpin : MonoBehaviour
{
    public float spinSpeed = 90f;        // Speed of rotation (degrees per second)
    public float disappearAfterSeconds = 3f;  // Time in seconds before the object disappears

    private bool isSpinning = true;

    void Start()
    {
        // Start the coroutine to make the object disappear after the set time
        StartCoroutine(DisappearAfterTime(disappearAfterSeconds));
    }

    void Update()
    {
        if (isSpinning)
        {
            // Rotate the object around its Y-axis (spins it in the air)
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            transform.position = transform.position + Vector3.up*Time.deltaTime;
        }
    }

    IEnumerator DisappearAfterTime(float time)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(time);

        // Optionally, you can add a fading effect here if you'd like

        // Set the object inactive (or you can use Destroy to remove it completely)
        Destroy(gameObject);  // This will completely remove the object from the scene
    }
}
