using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance = null;
    
    [SerializeField] private float attackTime;
    [SerializeField] private float displayTime;
    [SerializeField] private float decayTime;

    private readonly List<TutorialPrompt> _prompts = new List<TutorialPrompt>();
    private readonly List<TutorialPrompt> _toRemove = new List<TutorialPrompt>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        TutorialPrompt.SetValues(attackTime, displayTime, decayTime);
        this.enabled = false;
    }

    public void Notify(string message)
    {
        Debug.Log("Tutorial broadcasting: " + message);
        foreach (TutorialPrompt prompt in _prompts)
        {
            prompt.Notify(message);   
        }
        foreach (TutorialPrompt prompt in _toRemove)
        {
            _prompts.Remove(prompt);
        }
        _toRemove.Clear();
    }

    public void Register(TutorialPrompt prompt)
    {
        if (!_prompts.Contains(prompt)) _prompts.Add(prompt);
    }

    public void Deregister(TutorialPrompt prompt)
    {
        if (_prompts.Contains(prompt)) _toRemove.Add(prompt);
    }
}