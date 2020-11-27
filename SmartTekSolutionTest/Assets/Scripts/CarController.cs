using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _currentSteeringAngle;
    private float _currentBreakForce;
    private bool _isBreaking;

    [SerializeField] private float _motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _maxSteeringAngle;

    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;

    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheelTransform;
    [SerializeField] private Transform _rearLeftWheelTransform;
    [SerializeField] private Transform _rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
        UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }

    private void HandleSteering()
    {
        _currentSteeringAngle = _maxSteeringAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _currentSteeringAngle;
        _frontRightWheelCollider.steerAngle = _currentSteeringAngle;
    }

    private void HandleMotor()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _motorForce;
        _currentBreakForce = _isBreaking ? _breakForce : 0f;
        if (_isBreaking)
            ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;
        _rearLeftWheelCollider.brakeTorque = _currentBreakForce;
        _rearRightWheelCollider.brakeTorque = _currentBreakForce;
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }
}


