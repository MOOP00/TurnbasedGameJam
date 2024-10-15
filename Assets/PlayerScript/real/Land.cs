using UnityEngine;
using UnityEngine.SceneManagement;

public class Land : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the land block. Changing scene to: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}