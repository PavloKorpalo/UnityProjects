using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDeployer : MonoBehaviour
{

    [SerializeField] private GameObject _heart;

    public void HeartSpawner()
    {
        int randomNumber = Random.Range(0, 1);
        if(randomNumber == 1)
        {
            Instantiate(_heart, transform.position, Quaternion.identity);
        }
      
    }
}
