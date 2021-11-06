using UnityEngine;

/// <summary>
/// Represents a flower that can grow
/// </summary>
public class GrowthFlower : MonoBehaviour
{
    /// <summary>
    /// The amount of time (in seconds) the flower takes to grow
    /// </summary>
    [SerializeField] private float growthTime;
    /// <summary>
    /// The way the flower grows over time (0 = 0 scale, 1 = full scale)
    /// </summary>
    [SerializeField] private AnimationCurve growthRate;

    /// <summary>
    /// The scale the flower will grow to (Default is starting size)
    /// </summary>
    private Vector3 _growthTarget;
    /// <summary>
    /// Reference to this object's transform
    /// </summary>
    private Transform _transform;
    /// <summary>
    /// How much time has passed since the growth process started
    /// </summary>
    private float _passedTime;

    private void Start()
    {
        // Store a reference to this object's transform
        _transform = gameObject.transform;
        // Set the starting scale as the growth target;
        _growthTarget = _transform.localScale;
        // Shrink the flower down to nothing
        _transform.localScale = Vector3.zero;
        //Disable this script so it doesn't receive updates until necessary
        this.enabled = false;
    }

    /// <summary>
    /// Process the growth
    /// </summary>
    private void Update()
    {
        // Update how much time has passed since growth started
        _passedTime += Time.deltaTime;
        // Check if there's still growth time left
        if (_passedTime < growthTime)
        {
            // Update the objects scale to match where it is in its growth
            _transform.localScale =
                Vector3.Lerp(Vector3.zero, _growthTarget, growthRate.Evaluate(_passedTime / growthTime));
            // NOTE: this implementation forces the growth to start from zero. If the flower is able to grow twice
            //      or something, this will need to be changed. Similarly, _growthTarget is always going to be the 
            //      starting scale, therefore the flower can never grow past its starting size without changes
        }
        else
        {
            // If growth time is up, finalise the growth
            _transform.localScale = _growthTarget;
            // And prevent the script from processing further updates
            this.enabled = false;
        }
    }

    /// <summary>
    /// Trigger growth
    /// </summary>
    public void Grow()
    {
        // Let this script receive updates to process the growth
        this.enabled = true;
        // Set the passed time to zero
        _passedTime = 0;
    }
}