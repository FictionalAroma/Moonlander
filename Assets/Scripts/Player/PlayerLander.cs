using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLander : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputBindings _controls;
    private InputAction _thrust;
    private InputAction _right;
    private InputAction _left;

    [field: SerializeField] public float ThrustForce { get; private set; }
    [field: SerializeField] public float RotationForce { get; private set; }
    [field: SerializeField] public float FuelUseFactor { get; private set; } = 1;
    [field: SerializeField] public float StartingFuel { get; private set; } = 100;
    [SerializeField] private SliderDisplay _fuelDisplay;

    public float CurrentFuel { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        CacheControls();
        _fuelDisplay.MaxValue = StartingFuel;
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        CurrentFuel = StartingFuel;
        _fuelDisplay.SetToMax();
    }

    private void OnEnable()
    {
        EnableControls();
    }
    private void CacheControls()
    {
        _controls = new PlayerInputBindings();
        _thrust = _controls.Player.Thrust;
        _right = _controls.Player.Right;
        _left = _controls.Player.Left;
    }

    private void EnableControls()
    {
        _controls.Enable();
        _controls.Player.Enable();

    }

    private void FixedUpdate()
    {
        if (CurrentFuel > 0)
        {
            if (_thrust.IsPressed())
            {
                ThrustUp();
            }

            if (_right.IsPressed())
            {
                TurnRight();
            }
            else if (_left.IsPressed())
            {
                TurnLeft();
            }
        }
    }

    private void LateUpdate()
    {
        _fuelDisplay.SetValues(CurrentFuel);
    }

    private void TurnLeft()
    {
        _rb.AddTorque(RotationForce, ForceMode2D.Force);
        CurrentFuel -= TurningFuelUse;

    }

    private void TurnRight()
    {
        _rb.AddTorque(-RotationForce, ForceMode2D.Force);
        CurrentFuel -= TurningFuelUse;

    }

    private float TurningFuelUse => ThrustFuelUse / 2;
    private float ThrustFuelUse => Time.fixedDeltaTime * FuelUseFactor;

    private void ThrustUp()
    {
        _rb.AddForce(_rb.GetRelativeVector(Vector2.up) * ThrustForce, ForceMode2D.Force);
        CurrentFuel -= ThrustFuelUse;
    }



}
