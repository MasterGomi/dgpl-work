using UnityEngine;

// ReSharper disable once IdentifierTypo
public class WaterableTree : ThirstyObject
{
    [SerializeField] private float growthScale;
    
    public override void Water()
    {
        gameObject.transform.localScale *= growthScale;
    }
}
