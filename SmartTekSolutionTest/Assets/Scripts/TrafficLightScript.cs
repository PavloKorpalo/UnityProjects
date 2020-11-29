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

  
  
    void Start()
    {
        _greenLight.SetActive(true);
        _yellowLight.SetActive(false);
        _redLight.SetActive(false);
        _sensorCollider.SetActive(false);
        Invoke("SwitchGreen", 6f);
    }


 

    public void SwitchGreen()
    {
        _greenLight.SetActive(false);
        _yellowLight.SetActive(true);
        Invoke("SwitchYellow", 2f);
    }

    public void SwitchYellow()
    {
        _yellowLight.SetActive(false);
        _redLight.SetActive(true);
        _sensorCollider.SetActive(true);
        Invoke("SwitchRed", 8f);
    }

    public void SwitchRed()
    {
        _redLight.SetActive(false);
        _sensorCollider.SetActive(false);
        _greenLight.SetActive(true);
        Invoke("SwitchGreen", 6f);
    }
}
