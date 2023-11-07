using System;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    private Ship _playerShip;
    private Animator _anim;
    private bool _blastShieldState;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!!_playerShip) 
        {
            HandleMovement();
        }

        if (!!_anim)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleBlastShield();
            }
        }
    }

    private void HandleMovement()
    {
        transform.Translate(new Vector3(
            Input.GetAxis("Horizontal") * _playerShip.speed,
            0,
            Input.GetAxis("Vertical") * _playerShip.speed
        ) * Time.deltaTime);
    }

    private void ToggleBlastShield() 
    {
        _anim.SetBool("Open", !_blastShieldState);
        _blastShieldState = !_blastShieldState;
    }
}