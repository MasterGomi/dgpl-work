using System;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector3 LastCheckpoint
    {
        set => _lastCheckpoint = value;
    }


    private static CharacterController _player;
    private static Vector3 _lastCheckpoint;

    private void Start()
    {
        _lastCheckpoint = PlayerManager.Instance.PlayerCoords.position;
        _player = PlayerManager.Instance.GetComponent<CharacterController>();
    }

    public static void SendToCheckpoint()
    {
        Vector3 playerPos = PlayerManager.Instance.PlayerCoords.position;
        Vector3 intermediatePoint = playerPos + Vector3.up * 10000;
        _player.Move(intermediatePoint - playerPos);
        _player.Move(_lastCheckpoint - intermediatePoint);
        //SevenMillionStrat();
    }

    
    private static readonly Vector3 _intermediaryPoint1 = new Vector3(7000000, -10, 7000000);
    private static readonly Vector3 _intermediaryPoint2 = new Vector3(0, 7000000, 0);
    private static void SevenMillionStrat()
    {
        _player.Move(_intermediaryPoint1 - PlayerManager.Instance.PlayerCoords.position);
        _player.Move(_intermediaryPoint2 - _intermediaryPoint1);
        _player.Move(_lastCheckpoint - _intermediaryPoint2);
    }
}