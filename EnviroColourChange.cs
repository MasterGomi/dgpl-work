using System;
using System.Collections.Generic;
using UnityEngine;

public class EnviroColourChange : MonoBehaviour, IColourChange
{
    /// <summary>
    /// Change time (in seconds) between each step
    /// </summary>
    [SerializeField] private float changeTime = 10;
    [SerializeField] private uint changeSteps = 1;
    [SerializeField] private AnimationCurve changeRate;
    [SerializeField] private Color startColour;
    [SerializeField] private Color[] endColours;
    [SerializeField] private Color startSpecColour = new Color(0.9f, 0.9f, 0.9f, 1);
    [SerializeField] private Color endSpecColour = Color.black;
    [SerializeField] private Material[] materialsToChange;
    [SerializeField] private AudioZone audioZone;

    private float _timePassed = 0;
    private float _timeTarget = 0;
    private float _timeTotal;
    private uint _changedTimes = 0;
    private static readonly int SpecColor = Shader.PropertyToID("_SpecColor");

    private void Start()
    {
        _timeTotal = changeSteps * changeTime;
        foreach (Material mat in materialsToChange)
        {
            mat.color = startColour;
            mat.SetColor(SpecColor, startSpecColour);
        }
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        bool pastTime = _timePassed >= _timeTarget;
        Color newSpecColour = Color.Lerp(startSpecColour, endSpecColour, changeRate.Evaluate((pastTime ? _timeTarget : _timePassed) / _timeTotal));
        this.enabled = !pastTime;
        for (int i = 0; i < materialsToChange.Length; i++)
        {
            Material mat = materialsToChange[i];
            mat.color = Color.Lerp(startColour, endColours[i], changeRate.Evaluate((pastTime ? _timeTarget : _timePassed) / _timeTotal));
            mat.SetColor(SpecColor, newSpecColour);
        }
    }

    public void ChangeColour()
    {
        if (_changedTimes >= changeSteps) return;
        _timeTarget += changeTime;
        _changedTimes++;
        this.enabled = true;
        if (_changedTimes != changeSteps) return;
        AudioManager.Instance.PlaySound(AudioManager.ESound.PuzzleCompleteSting);
        AudioManager.Instance.SwitchBackgroundTrack(AudioManager.ESound.BackgroundTrackLively);
        audioZone.SetPuzzleStatus(true);
    }

    public void ChangeColour(Color targetColour)
    {
        ChangeColour();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < materialsToChange.Length; i++)
        {
            Material mat = materialsToChange[i];
            mat.color = endColours[i];
            mat.SetColor(SpecColor, endSpecColour);
        }
    }
}