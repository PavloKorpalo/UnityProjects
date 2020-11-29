using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Destroy(gameObject, 0.8f);
    }
}
