using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButtonScript : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<TankEngine>().Shoot();
    }
}
