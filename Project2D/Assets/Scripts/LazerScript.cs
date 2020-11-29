using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up *_speed;
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 0.83f);
    }

}
