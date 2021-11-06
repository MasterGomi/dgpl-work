using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    /// <summary>
    /// Message to send to tutorial prompts.
    /// Matches the "Respond To" field on the tutorial prompt(s) you want to trigger
    /// </summary>
    [SerializeField] private string message;

    /// <summary>
    /// Number of times to broadcast the message. Set 0 for unlimited (send every time)
    /// </summary>
    [SerializeField] private uint timesToShow = 1;


    private uint _sentMessages = 0;
    private bool _showUnlimited;

    private void Start()
    {
        _showUnlimited = timesToShow == 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        TutorialManager.Instance.Notify(message.ToLower());
        if (_showUnlimited) return;

        _sentMessages++;
        // If we've sent all the messages we intend to, disable the script and obliterate
        //      the collider so Unity doesn't waste time on them
        if (_sentMessages >= timesToShow)
        {
            Destroy(GetComponent<Collider>());
            this.enabled = false;
        }
    }
}