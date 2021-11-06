using System;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private float currentStrength;

    private CharacterController _playerController;
    private Transform _playerTransform;

    private void Start()
    {
        _playerController = PlayerManager.Instance.GetComponent<CharacterController>();
        _playerTransform = PlayerManager.Instance.PlayerCoords;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        // Apply gentle movement to mimic current
        Vector3 worldDir = transform.TransformDirection(transform.right);
        //Vector3 playerDir = _playerTransform.InverseTransformDirection(worldDir);
        _playerController.Move(worldDir * currentStrength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Egg")) return;
        
        other.GetComponent<Egg>().Drown();
    }
}