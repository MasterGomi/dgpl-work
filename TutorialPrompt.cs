using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPrompt : MonoBehaviour
{
    private enum DisplayState
    {
        Attack,
        Display,
        Decay,
        Hide
    };

    /// <summary>
    /// Messages that need to be sent (in order) from tutorial triggers to show this tutorial prompt
    /// </summary>
    [SerializeField] private string[] respondTo;
    /// <summary>
    /// Messages that will hide this message if triggered
    /// </summary>
    [SerializeField] private List<string> leaveOn;

    private int _seen = 0;
    private RawImage _img;
    private float _timePassed = 0;
    private static float _displayTime;
    private static float _attackTime;
    private static float _decayTime;
    private DisplayState _state;
    private static readonly Color Transparent = new Color(1, 1, 1, 0);
    private static readonly Color Opaque = new Color(1, 1, 1, 1);

    private void Start()
    {
        bool showAtStart = respondTo.Length == 0;
        for (int i = 0; i < respondTo.Length; i++)
        {
            respondTo[i] = respondTo[i].ToLower();
        }
        for (int i = 0; i < leaveOn.Count; i++)
        {
            leaveOn[i] = leaveOn[i].ToLower();
        }
        _img = GetComponent<RawImage>();
        // Opaque if show at start, transparent otherwise
        _img.color = showAtStart ? Opaque : Transparent;
        this.enabled = showAtStart;
        if (showAtStart) _state = DisplayState.Display;
        TutorialManager.Instance.Register(this);
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        switch (_state)
        {
            case DisplayState.Attack:
                if (_timePassed < _attackTime)
                {
                    _img.color = Color.Lerp(Transparent, Opaque, _timePassed / _attackTime);
                    return;
                }
                else
                {
                    _img.color = Opaque;
                    _timePassed = 0;
                    _state = DisplayState.Display;
                    return;
                }
            case DisplayState.Display:
                if (_timePassed < _displayTime) return;
                else
                {
                    _timePassed = 0;
                    _state = DisplayState.Decay;
                    return;
                }
            case DisplayState.Decay:
                if (_timePassed < _decayTime)
                {
                    _img.color = Color.Lerp(Opaque, Transparent, _timePassed / _decayTime);
                    return;
                }
                else
                {
                    _img.color = Transparent;
                    _state = DisplayState.Hide;
                    this.enabled = false;
                    return;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void SetValues(float attackTime, float displayTime, float decayTime)
    {
        _attackTime = attackTime;
        _displayTime = displayTime;
        _decayTime = decayTime;
    }

    public void Notify(string message)
    {
        // If we've seen all the messages, we're receiving messages to see if we need to end the prompt early
        if (_seen >= respondTo.Length)
        {
            LateNotify(message);
            return;
        }
        // If we don't respond to the message... don't respond to the message
        if (respondTo[_seen] != message) return;
        // Confirm that we've seen the message
        _seen++;
        // Check if we've now seen them all. If we haven't, we're done for now
        if (_seen < respondTo.Length) return;
        // If we're here, this is the first and only time we will have seen all messages and gotten to this point in 
        //      execution, so we can start displaying the message
        BeginAttack();
    }

    private void LateNotify(string message)
    {
        if (_state == DisplayState.Hide || leaveOn.Contains(message))
        {
            TutorialManager.Instance.Deregister(this);
            if (leaveOn.Contains(message))
            {
                _img.color = Transparent;
                _state = DisplayState.Hide;
                this.enabled = false;
            }
        }
    }
    
    private void BeginAttack()
    {
        _state = DisplayState.Attack;
        this.enabled = true;
    }
}