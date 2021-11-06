using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform respawnTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Checkpoint set");
        CheckpointManager.LastCheckpoint = respawnTarget.position;
    }
}