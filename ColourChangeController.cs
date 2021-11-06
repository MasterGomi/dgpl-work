using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to manage and trigger multiple colour changes simultaneously
/// </summary>
public class ColourChangeController : MonoBehaviour, IColourChange
{
    /// <summary>
    /// Colour changing objects or objects that control other colour changing objects that this object manages
    /// </summary>
    private List<IColourChange> _children;

    private void Start()
    {
        _children = new List<IColourChange>(GetComponentsInChildren<IColourChange>());
        _children.Remove(this);
    }

    /// <summary>
    /// Trigger a colour change on all children of this controller
    /// </summary>
    public void ChangeColour()
    {
        foreach(IColourChange child in _children)
        {
            child.ChangeColour();
        }
    }

    /// <summary>
    /// Trigger a change to a certain colour on all children of this controller
    /// </summary>
    /// <param name="targetColour">Target colour for the colour change</param>
    public void ChangeColour(Color targetColour)
    {
        foreach (IColourChange child in _children)
        {
            child.ChangeColour(targetColour);
        }
    }
}