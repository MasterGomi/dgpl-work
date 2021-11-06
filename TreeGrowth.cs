using System;
using UnityEngine;

public class TreeGrowth : ThirstyObject
{
    [SerializeField] private int growTimes = 1;
    [SerializeField] private float growTimeframe;
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private AnimationCurve growthRate;
    [SerializeField] private Transform trans;

    private bool _canGrow = true;
    private int _grownTimes = 0;
    private float _timePassed = 0;
    private float _targetTime;
    private float _growTimeIncrement;
    private SphereCollider _wateringTrigger;

    private void Start()
    {
        trans.localScale = initialScale;
        _wateringTrigger = GetComponent<SphereCollider>();
        _growTimeIncrement = growTimeframe / growTimes;
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < _targetTime)
        {
            trans.localScale =
                Vector3.Lerp(initialScale, finalScale, growthRate.Evaluate(_timePassed / growTimeframe));
        }
        else
        {
            trans.localScale =
                Vector3.Lerp(initialScale, finalScale, growthRate.Evaluate(_targetTime / growTimeframe));
            this.enabled = false;
        }
    }

    private void StartGrow()
    {
        _grownTimes++;
        if (_grownTimes >= growTimes)
        {
            GetComponent<IColourChange>()?.ChangeColour();
            _canGrow = false;
            _wateringTrigger.enabled = false;
            // Strictly speaking, this is a temporary fix
            WaterInteraction.WaterTarget = null;
        }
        _targetTime += _growTimeIncrement;
        this.enabled = true;
    }
    
    public override void Water()
    {
        if (_canGrow) StartGrow();
    }
}