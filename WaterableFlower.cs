using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once IdentifierTypo
/// <summary>
/// Represents a flower that can be watered and change colours
/// </summary>
public class WaterableFlower : ThirstyObject
{
    [SerializeField] [CanBeNull] private GrowthController growNext;
    [SerializeField] [CanBeNull] private ColourChangeController environmentColourChanger;
    
    /// <summary>
    /// Determines if this flower's friends have growing left to do
    /// </summary>
    private bool _canGrow = true;
    
    /// <summary>
    /// Water this flower, change its colour, and grow its friends (if applicable)
    /// </summary>
    public override void Water()
    {
        // Change the colour of this flower (or children if this is controller)
        gameObject.GetComponent<IColourChange>()?.ChangeColour();
        if (environmentColourChanger) environmentColourChanger.ChangeColour();
        
        // Only grow the flowers if they haven't been grown yet
        if (!_canGrow) return;
        
        if (growNext) growNext.Grow();
        // Prevent further growth
        _canGrow = false;
    }
}