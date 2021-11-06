using System;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("You're dead, pal");
        CheckpointManager.SendToCheckpoint();
    }
}