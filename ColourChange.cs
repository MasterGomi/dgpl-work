using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that can change colours
/// </summary>
public class ColourChange : MonoBehaviour, IColourChange
{
    /// <summary>
    /// Potential colours for object to change to when told to change colours
    /// </summary>
    [SerializeField] private List<Color> possibleColours;
    /// <summary>
    /// How long, in seconds, a change in colour should take
    /// </summary>
    [SerializeField] private float changeTime = 1;
    /// <summary>
    /// The way an object changes colour across changeTime (0 = original colour, 1 = new colour)
    /// </summary>
    [SerializeField] private AnimationCurve changeVector;
    
    /// <summary>
    /// Colour at the start of the change
    /// </summary>
    private Color _startColour;
    /// <summary>
    /// Desired colour at the end of the change
    /// </summary>
    private Color _targetColour;
    private float _timePassed;
    private Material _material;

    /// <summary>
    /// Trigger this object to change colours to a random colour in the possibleColours list
    /// </summary>
    public void ChangeColour()
    {
        // If there's only one colour in the list, pick that one
        if (possibleColours.Count == 1)
        {
            // If there's only one colour, there's a possibility that the object is already that colour, and we don't
            //      need to change
            if (_material.color == possibleColours[0]) return;
            
            ChangeColour(possibleColours[0]);
            return;
        }
        // Change the colour to a random colour from the possibleColours list that isn't the current colour
        ChangeColour(possibleColours.GetRandom(_material.color));
    }
    /// <summary>
    /// Gradually change this object's colour to a specific colour
    /// </summary>
    /// <param name="targetColour">End colour for change</param>
    public void ChangeColour(Color targetColour)
    {
        // Allow the script to receive updates so it can gradually change colour
        this.enabled = true;
        // Set the current colour as the starting colour
        _startColour = _material.color;
        // Pick a random from the possibleColours list that isn't the current colour
        _targetColour = targetColour;
        // Set the time passed to 0
        _timePassed = 0;
    }

    /// <summary>
    /// Process the colour change
    /// </summary>
    private void Update()
    {
        // Increment how much time has passed
        _timePassed += Time.deltaTime;
        // Check if we're still changing colours
        if (_timePassed < changeTime)
        {
            // Set the colour in accordance with where we are in the change
            _material.color = 
                Color.Lerp(_startColour, _targetColour, changeVector.Evaluate(_timePassed / changeTime));
        }
        else
        {
            // If the time allotted to the change has passed, confirm the change
            _material.color = _targetColour;
            // And prevent the script from receiving further updates
            this.enabled = false;
        }
    }

    private void Start()
    {
        // Prevent this script from receiving update calls until it has been asked to change colours
        this.enabled = false;
        // Store a reference to this object's material component
        _material = gameObject.GetComponent<MeshRenderer>().material;
    }
}
