using UnityEngine;

public class cChangeSceneOnTrigger : MonoBehaviour
{
    public string sceneToLoad;  // ชื่อของ Scene ที่ต้องการโหลด
    private SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();  // ค้นหา SceneTransition ในฉาก
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneTransition.ChangeSceneWithWarp(sceneToLoad);
        }
    }
}