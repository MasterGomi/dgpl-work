using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Represents a object with GrowthFlower children that are to all grow at once
/// </summary>
public class GrowthController : MonoBehaviour
{
    /// <summary>
    /// How much time (in seconds) before the children of this controller start growing
    /// </summary>
    [SerializeField] private float growthDelay;
    /// <summary>
    /// The next GrowthController to call when this starts growing (if applicable)
    /// </summary>
    [SerializeField] [CanBeNull] private GrowthController growNext;

    /// <summary>
    /// GrowthFlower components of children of this game object
    /// </summary>
    private readonly List<GrowthFlower> _children = new List<GrowthFlower>();
    /// <summary>
    /// Time passed since growing started
    /// </summary>
    private float _passedTime;
    /// <summary>
    /// Represents whether this controller has already grown
    /// </summary>
    private bool _canGrow = true;

    private bool _canColourChange = true;
    /// <summary>
    /// ColourChangeController component ref
    /// </summary>
    [CanBeNull] private ColourChangeController _colourChanger;
    
    private void Start()
    {
        // Find all the GrowthFlower elements of the children of this object
        _children.AddRange(gameObject.GetComponentsInChildren<GrowthFlower>());
        // Set the ColourChangeController if present
        _colourChanger = gameObject.GetComponent<ColourChangeController>();
        // Prevent this script from receiving update calls until it's time
        this.enabled = false;
    }

    /// <summary>
    /// Delay before growing children
    /// </summary>
    private void Update()
    {
        // Update how much time has passed
        _passedTime += Time.deltaTime;
        if (_canColourChange && _colourChanger)
        {
            _colourChanger.ChangeColour();
            _canColourChange = false;
        }
        // If we're still waiting, exit
        if (_passedTime < growthDelay) return;
        // Change colours
        // Go through all of the child GrowthFlowers and tell them to grow
        if (_canGrow)
        {
            foreach (GrowthFlower child in _children)
            {
                child.Grow();
            }
        }
        // Tell the next growth controller to start the countdown
        if(growNext) growNext.Grow();
        // No more growing
        _canGrow = false;
        // Prevent this script from receiving further updates
        this.enabled = false;
    }

    /// <summary>
    /// Start the countdown before all this controller's children grow
    /// </summary>
    public void Grow()
    {
        // Set passed time to zero
        _passedTime = 0;
        // Let this script start receiving update calls
        this.enabled = true;
    }
}