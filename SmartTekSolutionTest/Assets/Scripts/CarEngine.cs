 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{

    [SerializeField] private Transform _path;
    [SerializeField] private float _maxSteerAngle;

    [SerializeField] private WheelCollider _frontLeftWheel;
    [SerializeField] private WheelCollider _frontRightWheel;


    private List<Transform> _nodes;
    private int _currentNode = 0;


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
    }

    private void Drive()
    {
        _frontLeftWheel.motorTorque = 100f;
        _frontRightWheel.motorTorque = 100f;
    }

    private void ApplySteer()
    {
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
