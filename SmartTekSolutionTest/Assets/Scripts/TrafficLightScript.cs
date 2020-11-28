using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightScript : MonoBehaviour
{
    private float _switchTime;

    [SerializeField] private GameObject _greenLight;
    [SerializeField] private GameObject _yellowLight;
    [SerializeField] private GameObject _redLight;
    [SerializeField] private GameObject _sensorCollider;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _redLight.SetActive(true);
        _sensorCollider.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer == 10)
        {
            _redLight.SetActive(false);
            _yellowLight.SetActive(true);
        }

        else if(_timer == 12)
        {
            _yellowLight.SetActive(false);
            _sensorCollider.SetActive(false);
            _greenLight.SetActive(true);
        }
        else if(_timer == 20)
        {
            _greenLight.SetActive(false);
            _sensorCollider.SetActive(true);
            _yellowLight.SetActive(true);
        }
        else if(_timer == 22)
        {
            _yellowLight.SetActive(false);
            _redLight.SetActive(true);
            _timer = 0;
        }
    }
}
