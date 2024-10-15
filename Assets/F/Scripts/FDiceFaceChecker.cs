using UnityEngine;

public class FDiceTopFaceChecker : MonoBehaviour
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
        /*
        if (topFace != null)
        {
            Debug.Log("Top face is: " + value);
        }
        */
    }
}