using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NeoColourChange : MonoBehaviour, IColourChange
{
    /// <summary>
    /// How long, in seconds, a change in colour should take
    /// </summary>
    [SerializeField] private float changeTime = 1;
    /// <summary>
    /// The way an object changes colour across changeTime (0 = original colour, 1 = new colour)
    /// </summary>
    [SerializeField] private AnimationCurve changeVector;
    
    [SerializeField] private Material flowerMaterial;
    [SerializeField] private Color startFlowerColour;
    [FormerlySerializedAs("startStemTopColour")]
    [SerializeField] private Color startStemColour;
    [SerializeField] private Color endFlowerColour;
    [FormerlySerializedAs("endStemTopColour")]
    [SerializeField] private Color endStemColour;

    
    private static readonly int FlowerColour = Shader.PropertyToID("_FlowerColor");
    private static readonly int StemColour = Shader.PropertyToID("_StemColor");
    private float _timePassed;

    private void Start()
    {
        flowerMaterial.SetColor(FlowerColour, startFlowerColour);
        flowerMaterial.SetColor(StemColour, startStemColour);
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
            flowerMaterial.SetColor(FlowerColour, 
                Color.Lerp(startFlowerColour, endFlowerColour, 
                    changeVector.Evaluate(_timePassed / changeTime)));
            flowerMaterial.SetColor(StemColour, 
                Color.Lerp(startStemColour, endStemColour, 
                    changeVector.Evaluate(_timePassed / changeTime)));
        }
        else
        {
            // If the time allotted to the change has passed, confirm the change
            flowerMaterial.SetColor(FlowerColour, endFlowerColour);
            flowerMaterial.SetColor(StemColour, endStemColour);
            // And prevent the script from receiving further updates
            this.enabled = false;
        }
    }

    public void ChangeColour()
    {
        _timePassed = 0;
        this.enabled = true;
    }

    public void ChangeColour(Color targetColour)
    {
        ChangeColour();
    }

    private void OnDestroy()
    {
        flowerMaterial.SetColor(FlowerColour, endFlowerColour);
        flowerMaterial.SetColor(StemColour, endStemColour);
    }
}