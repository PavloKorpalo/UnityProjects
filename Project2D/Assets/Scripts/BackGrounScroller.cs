using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounScroller : MonoBehaviour
{

    public float speed;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.Repeat(Time.time * speed, 33.4f);
        transform.position = startPosition + Vector2.down * movement;
    }
}
