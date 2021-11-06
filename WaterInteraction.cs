using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterInteraction : MonoBehaviour
{
    public static bool InWater = false;
    public static ThirstyObject WaterTarget = null;
    /// <summary>
    /// A collection of the colours the player will be at different levels of water. Also determines max charges
    /// </summary>
    [SerializeField] private Color[] waterColours;
    [SerializeField] private float colourDrainTime;
    [SerializeField] private float colourFillTime;
    [SerializeField] private GameObject playerModel;

    private Material _mat;
    private int _waterCharges = 0;
    private int _maxCharges;
    private Color _targetColour;
    private Color _startColour;
    private float _timeForChange;
    private float _timePassed;

    private void Start()
    {
        _mat = playerModel.GetComponent<MeshRenderer>().material;
        _mat.color = waterColours[0];
        _maxCharges = waterColours.Length - 1;
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < _timeForChange)
        {
            _mat.color = Color.Lerp(_startColour, _targetColour, _timePassed / _timeForChange);
        }
        else
        {
            _mat.color = _targetColour;
            this.enabled = false;
        }
    }

    public void WaterInteract(InputAction.CallbackContext context)
    {
        // We only want to do things on initial press
        if (!context.started) return;

        if (_waterCharges < _maxCharges && InWater)
        {
            // TODO: trigger water collection animation
            ChangeWaterLevel(_maxCharges);

            AudioManager.Instance.PlaySound(AudioManager.ESound.WaterPickup, this.gameObject);
            
            //Debug.Log("Picked up water");
        }
        else if (_waterCharges > 0 && WaterTarget != null)
        {
            // TODO: trigger water placement animation
            ChangeWaterLevel(_waterCharges - 1);
            
            // Water the object
            WaterTarget.Water();
            AudioManager.Instance.PlaySound(AudioManager.ESound.WaterDeposit, this.gameObject);

            //Debug.Log("Water dumped");
        }
    }

    private void ChangeWaterLevel(int targetLevel)
    {
        bool gain = targetLevel > _waterCharges;
        _startColour = _mat.color;
        _targetColour = waterColours[targetLevel];
        _timeForChange += (gain ? colourFillTime : colourDrainTime) * Math.Abs(targetLevel - _waterCharges) -
                          _timePassed;
        _timePassed = 0;
        _waterCharges = targetLevel;
        this.enabled = true;
    }
}
