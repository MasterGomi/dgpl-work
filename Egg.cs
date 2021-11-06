using System;
using UnityEngine;

public class Egg : MonoBehaviour
{
    enum EggState
    {
        Drowning,
        Respawning,
    }

    [SerializeField] private float drownTime = 2;
    [SerializeField] private float regrowTime = 2;
    
    private Vector3 _startPos;
    private Vector3 _startScale;
    private EggState _state;
    private Transform _trans;
    private Rigidbody _rb;
    private float _timePassed;

    private void Start()
    {
        _trans = transform;
        _startPos = _trans.position;
        _startScale = _trans.localScale;
        _rb = GetComponent<Rigidbody>();
        this.enabled = false;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        switch (_state)
        {
            case EggState.Drowning:
                if (_timePassed < drownTime)
                {
                    _trans.localScale = Vector3.Lerp(_startScale, Vector3.zero, _timePassed / drownTime);
                }
                else
                {
                    _trans.localScale = Vector3.zero;
                    ResetPos();
                }
                return;
            case EggState.Respawning:
                if (_timePassed < regrowTime)
                {
                    _trans.localScale = Vector3.Lerp(Vector3.zero, _startScale, _timePassed / drownTime);
                }
                else
                {
                    _trans.localScale = _startScale;
                    this.enabled = false;
                }
                return;
            default:
                this.enabled = false;
                return;
        }
    }

    public void Drown()
    {
        GetComponent<InteractableMoveable>().Drop();
        _state = EggState.Drowning;
        _timePassed = 0;
        this.enabled = true;
    }

    private void ResetPos()
    {
        //_trans.position = _startPos;
        _trans.SetPositionAndRotation(_startPos, Quaternion.identity);
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _state = EggState.Respawning;
        _timePassed = 0;
        this.enabled = true;
    }
}