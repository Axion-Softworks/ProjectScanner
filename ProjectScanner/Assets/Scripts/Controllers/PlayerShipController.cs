using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShipController : MonoBehaviour
{
    [Header("Objects")]
    private Ship _playerShip;
    private Animator _anim;
    private Rigidbody _rb;

    [Header("Variables")]
    private bool _blastShieldState;
    public bool accelerating;
    public bool reversing;

    #region Monobehaviour API

    // Start is called before the first frame update
    void Start()
    {
        _playerShip = this.GetComponent<Ship>();

        if (_playerShip == null)
            Debug.LogError("Parameter _playerShip is null");

        _anim = this.GetComponent<Animator>();

        if (_anim == null)
            Debug.LogError("Parameter _anim is null");
        else 
        {
            _blastShieldState = _anim.GetBool("Open");
        }

        _rb = this.GetComponent<Rigidbody>();

        if (_rb == null)
            Debug.LogError("Parameter _rb is null");
        else 
        {
            _rb.mass = _playerShip.Mass;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!!_anim)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleBlastShield();
            }
        }
    }

    // FixedUpdate is called every 0.02sec and is independent of framerate
    void FixedUpdate()
    {
        if (!!_playerShip && !!_rb) 
        {
            HandleMovement();
        }
    }

    #endregion

    #region Movement API

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.X))
        {
            ApplyBrake();
        }
        else
        {
            var zAxis = Input.GetAxisRaw("Vertical");
            
            ApplyThrust(zAxis * _playerShip.Acceleration); 
        }

        var xAxis = Input.GetAxis("Horizontal");
        ApplyRotation(xAxis * _playerShip.RotationSpeed);

        
        if (!Input.GetKey(KeyCode.X) && _rb.drag > 0)
        {
            ResetBrake();
        }
    }

    private void ApplyThrust(float amount)
    {
        Vector3 force = new Vector3(0, 0, amount);

        if (amount == 0)
        {
            accelerating = false;
            reversing = false;
        }
        else if (float.IsNegative(amount))
        {
            accelerating = false;
            reversing = true;
        }
        else if (!float.IsNegative(amount)) 
        {
            accelerating = true;
            reversing = false;
        }

        if (_rb.velocity.z < _playerShip.MaxSpeed || 
            _rb.velocity.z == _playerShip.MaxSpeed && amount < 0 ||
            _rb.velocity.z == -_playerShip.MaxSpeed && amount > 0)
            _rb.AddRelativeForce(force);
            
        ClampVelocity();
    }

    private void ApplyRotation(float amount)
    {
        _rb.AddTorque(0, amount, 0);
    }

    private void ApplyBrake() 
    {
        _rb.drag = 1;
        accelerating = false;
        reversing = false;
    }

    private void ResetBrake()
    {
        _rb.drag = 0;
    }

    private void ClampVelocity()
    {
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _playerShip.MaxSpeed);
    }

    #endregion

    #region Misc Controls

    private void ToggleBlastShield() 
    {
        _anim.SetBool("Open", !_blastShieldState);
        _blastShieldState = !_blastShieldState;
    }

    #endregion
}