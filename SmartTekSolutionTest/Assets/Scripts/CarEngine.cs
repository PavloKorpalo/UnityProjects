using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{

    [SerializeField] private Transform _path;

    [Header("Car Settings")]

    [SerializeField] private float _maxSteerAngle;
    [SerializeField] private float _maxBrakeTorque;

    [SerializeField] private WheelCollider _frontLeftWheel;
    [SerializeField] private WheelCollider _frontRightWheel;
    [SerializeField] private WheelCollider _rearLeftWheel;
    [SerializeField] private WheelCollider _rearRightWheel;
    [SerializeField] private bool _isBraking = false;

    [Header("Sensors")]

    [SerializeField] private float _sensorLenght;
    [SerializeField] private Vector3 _frontSensorPosition = new Vector3(0f, 0.5f ,1.2f);
    [SerializeField] private float _frontSideSensorPosition = 0.35f;
    [SerializeField] private float _frontSensorAngle = 30f;

    private List<Transform> _nodes;
    private int _currentNode = 0;
    private bool _avoiding = false;
    


    private void Start()
    {
        Transform[] pathTransforms = _path.GetComponentsInChildren<Transform>();
        _nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
            if (pathTransforms[i] != _path.transform)
                _nodes.Add(pathTransforms[i]);
    }

    
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        Sensors();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * _frontSensorPosition.z;
        sensorStartPos += transform.up * _frontSensorPosition.y;
        float avoidMultiplier = 0;
        _avoiding = false;

       
     

        //front right sensor

        sensorStartPos += transform.right * _frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, _sensorLenght))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _avoiding = true;
                avoidMultiplier -= 1f;

            }
            else
            {
                if (hit.collider.CompareTag("TrafficLight"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = true;
                }
                else
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = false;
                }
            }
        }
       

        //front right angle sensor

        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(_frontSensorAngle, transform.up) * transform.forward, out hit, _sensorLenght))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _avoiding = true;
                avoidMultiplier -= 0.5f;

            }
            else
            {
                if (hit.collider.CompareTag("TrafficLight"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = true;
                }
                else
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = false;
                }
            }
        }
       

        //front left sensor

        sensorStartPos -= transform.right * _frontSideSensorPosition * 2;
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-_frontSensorAngle, transform.up) * transform.forward, out hit, _sensorLenght))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _avoiding = true;
                avoidMultiplier += 1f;

            }
            else
            {
                if (hit.collider.CompareTag("TrafficLight"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = true;
                }
                else
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = false;
                }
            }
        }
        

        //front left angle sensor

        else if (Physics.Raycast(sensorStartPos, transform.forward, out hit, _sensorLenght))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _avoiding = true;
                avoidMultiplier += 0.5f;

            }
            else
            {
                if (hit.collider.CompareTag("TrafficLight"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = true;
                }
                else
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = false;
                }
            }
        }

        //front center sensor


        if (avoidMultiplier == 0)
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                _avoiding = true;
                if(hit.normal.x < 0)
                {
                    avoidMultiplier = -1;
                }
                else
                {
                    avoidMultiplier = 1;
                }
            }
            else
            {
                if (hit.collider.CompareTag("TrafficLight"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = true;
                }
                else
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    _isBraking = false;
                }
            }

        }

        if (_avoiding)
        {
            _frontLeftWheel.steerAngle = _maxSteerAngle * avoidMultiplier;
            _frontRightWheel.steerAngle = _maxSteerAngle * avoidMultiplier;
        }
        
    }

    private void Drive()
    {

        if (!_isBraking)
        {
            _frontLeftWheel.motorTorque = 100f;
            _frontRightWheel.motorTorque = 100f;
        }
        else
        {
            _frontLeftWheel.motorTorque = 0;
            _frontRightWheel.motorTorque = 0;
        }
        
    }

    private void Braking()
    {
        if (_isBraking)
        {
            _rearLeftWheel.brakeTorque = _maxBrakeTorque;
            _rearRightWheel.brakeTorque = _maxBrakeTorque;
        }
        else
        {
            _rearLeftWheel.brakeTorque = 0;
            _rearRightWheel.brakeTorque = 0;
        }
    }

    private void ApplySteer()
    {
        //finds the closest waypoint
        if (_avoiding)
            return;

        Vector3 relativeVector = transform.InverseTransformPoint(_nodes[_currentNode].position);
        print(relativeVector);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * _maxSteerAngle;
        _frontLeftWheel.steerAngle = newSteer;
        _frontRightWheel.steerAngle = newSteer;
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, _nodes[_currentNode].position) < 0.5f)
        {
            if (_currentNode == _nodes.Count - 1)
            {
                _currentNode = 0;
            }
            else
            {
                _currentNode++;
            }
        }


    }   
    
}
