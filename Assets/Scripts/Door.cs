using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    private CameraController cam;

    private void Awake()
    {
        // Attempt to find the CameraController in the scene
        cam = FindObjectOfType<CameraController>();

        // Optional: Check if the CameraController was found
        if (cam == null)
        {
            Debug.LogError("CameraController not found in the scene!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cam == null)
            {
                Debug.LogError("CameraController is null! Cannot move to a new room.");
                return;
            }

            if (previousRoom == null || nextRoom == null)
            {
                Debug.LogError("PreviousRoom or NextRoom is null! Cannot transition.");
                return;
            }

            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }
}
