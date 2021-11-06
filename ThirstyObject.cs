using UnityEngine;

public abstract class ThirstyObject : MonoBehaviour
{
    /// <summary>
    /// Set the current water target to this script
    /// </summary>
    /// <param name="other">The colliding entity</param>
    private void OnTriggerEnter(Collider other)
    {
        // Only do it if the player has triggered it
        if (!other.CompareTag("Player")) return;
        
        // Set the water target to this
        WaterInteraction.WaterTarget = this;
        //Debug.Log("I am the water target", this);
    }
    
    /// <summary>
    /// Check if we need to unset the current water target
    /// </summary>
    /// <param name="other">The colliding entity</param>
    private void OnTriggerExit(Collider other)
    {
        // If this script was the current water target, unset it
        if (other.CompareTag("Player") && WaterInteraction.WaterTarget == this) WaterInteraction.WaterTarget = null;
    }

    /// <summary>
    /// Water the object. Functionality is set on a per object basis
    /// </summary>
    public abstract void Water();
    // Example of ThirstyObject child:
    //      Tree : ThirstyObject
    //      public override void Water(){
    //          MakeTreeGrow();
    //      }
}
