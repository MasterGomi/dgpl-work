using System;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private Animator eagleAnimator;
    [SerializeField] private Material eagleMaterial;
    [SerializeField] private Color startColour = new Color(0.3f, 0.3f, 0.3f);
    [SerializeField] private Color endColour = Color.white;
    [SerializeField] private float changeTime;

    private float _timePassed = 0;
    private bool _changed = false;

    private void Start()
    {
        eagleAnimator.enabled = false;
        eagleMaterial.color = startColour;
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < changeTime)
        {
            eagleMaterial.color = Color.Lerp(startColour, endColour, _timePassed / changeTime);
        }
        else
        {
            eagleMaterial.color = endColour;
            eagleAnimator.enabled = true;
            this.enabled = false;
        }
    }

    public void ChangeColour()
    {
        if (_changed) return;
        
        this.enabled = true;
        _changed = true;
    }

    private void OnDestroy()
    {
        eagleMaterial.color = endColour;
    }
}