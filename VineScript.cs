using System;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour, IColourChange
{
    [SerializeField] private Material vineMaterial;
    [SerializeField] private float changeTime;
    [SerializeField] private AnimationCurve changeRate = AnimationCurve.Linear(0, 0, 1, 1);

    private readonly Color _invis = new Color(0, 0, 0, 0);
    private readonly Color _visible = new Color(1, 1, 1, 1);
    private float _timePassed = 0;
    private bool _revealed = false;
    private BoxCollider[] _colliders;

    private void Start()
    {
        _colliders = GetComponents<BoxCollider>();
        foreach (BoxCollider box in _colliders) box.enabled = false;
        vineMaterial.color = _invis;
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < changeTime)
        {
            vineMaterial.color = Color.Lerp(_invis, _visible, changeRate.Evaluate(_timePassed / changeTime));
        }
        else
        {
            vineMaterial.color = _visible;
            this.enabled = false;
        }
    }

    public void ChangeColour()
    {
        if (_revealed) return;
        
        _revealed = true;
        foreach (BoxCollider box in _colliders) box.enabled = true;
        this.enabled = true;
    }

    public void ChangeColour(Color targetColour)
    {
        ChangeColour();
    }

    private void OnDestroy()
    {
        vineMaterial.color = _visible;
        foreach (BoxCollider box in _colliders) box.enabled = true;
    }
}