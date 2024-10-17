using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import SceneManagement

public class Skip : MonoBehaviour
{
    [SerializeField] private Button skipButton; // Reference to the Skip button
    [SerializeField] private string sceneToLoad; // Name of the scene to load

    private void Start()
    {
        // Ensure the button reference is assigned and add a listener
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(OnSkipButtonClicked);
        }
        else
        {
            Debug.LogError("Skip Button is not assigned in the inspector!");
        }
    }

    private void OnSkipButtonClicked()
    {
        // Load the specified scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the inspector!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the button listener when the object is destroyed
        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(OnSkipButtonClicked);
        }
    }
}
