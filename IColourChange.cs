using UnityEngine;

/// <summary>
/// Represents a colour changing object or a controller of colour changing objects
/// </summary>
public interface IColourChange
{
    void ChangeColour();
    void ChangeColour(Color targetColour);
}