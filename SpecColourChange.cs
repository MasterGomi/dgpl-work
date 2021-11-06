using System;
using UnityEngine;

public class SpecColourChange : MonoBehaviour, IColourChange
{
    [SerializeField] private Color startColour = Color.black;
    [SerializeField] private Color endColour = Color.white;
    [SerializeField] private Color startSpecColour = new Color(0.9f, 0.9f, 0.9f);
    [SerializeField] private Color endSpecColour = Color.black;
    [SerializeField] private float changeTime;
    [SerializeField] private AnimationCurve changeRate = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private GameObject changeNext;

    private float _timePassed;
    private Material _material;
    private static readonly int SpecColor = Shader.PropertyToID("_SpecColor");
    private IColourChange _changeNext;

    private void Start()
    {
        if (changeNext) _changeNext = changeNext.GetComponent<IColourChange>();
        _material = GetComponentInChildren<MeshRenderer>().material;
        _material.color = startColour;
        _material.SetColor(SpecColor, startSpecColour);
        this.enabled = false;
    }

    private void Update()
    {
        // Increment how much time has passed
        _timePassed += Time.deltaTime;
        // Check if we're still changing colours
        if (_timePassed < changeTime)
        {
            // Set the colour in accordance with where we are in the change
            _material.color = 
                Color.Lerp(startColour, endColour, changeRate.Evaluate(_timePassed / changeTime));
            _material.SetColor(SpecColor, 
                Color.Lerp(startSpecColour, endSpecColour, changeRate.Evaluate(_timePassed / changeTime)));
        }
        else
        {
            // If the time allotted to the change has passed, confirm the change
            _material.color = endColour;
            _material.SetColor(SpecColor, endSpecColour);
            // And prevent the script from receiving further updates
            this.enabled = false;
        }
    }

    public void ChangeColour()
    {
        _timePassed = 0;
        this.enabled = true;
        _changeNext?.ChangeColour();
    }

    public void ChangeColour(Color targetColour)
    {
        ChangeColour();
    }

    private void OnDestroy()
    {
        _material.color = endColour;
        _material.SetColor(SpecColor, endSpecColour);
    }
}