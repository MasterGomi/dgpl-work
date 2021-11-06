using UnityEngine;

public class InteractableWater : MonoBehaviour
{
    /// <summary>
    /// Entered water, so we can let the water interaction script know that water can be picked up
    /// </summary>
    /// <param name="other">The colliding entity</param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Can now pick up water");
        WaterInteraction.InWater = true;
    }
    
    /// <summary>
    /// Exited water, so let the water interaction script know that we can't pick up water
    /// </summary>
    /// <param name="other">The colliding entity</param>
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("No water for you");
        WaterInteraction.InWater = false;
    }
}
