using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownScript : MonoBehaviour
{

    private float _currentTime = 0f;
    private float _startTime = 3f;

    [SerializeField] private TMP_Text _text;

    [SerializeField] private GameObject _textPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= 1 * Time.deltaTime;
        _text.text = _currentTime.ToString("0");

        
            if(_currentTime <= 0.5)
            {
                _currentTime = 0;
           
            _textPrefab.SetActive(false);


            }
        
    }
}
