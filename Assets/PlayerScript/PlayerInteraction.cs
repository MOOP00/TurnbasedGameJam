using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to our PlayerController component
        playerController = transform.parent.GetComponent<PlayerController>();

        // ตรวจสอบว่า playerController ถูกกำหนดค่าเรียบร้อยหรือไม่
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on parent object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 4))
        {
            // You can add other interaction logic here if needed
        }
    }

    // Triggered when the player presses the tool button
    public void Interact()
    {
        Debug.Log("Interact called.");
    }
}