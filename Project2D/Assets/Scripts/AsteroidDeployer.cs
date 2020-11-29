using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDeployer : MonoBehaviour
{

    [SerializeField] private GameObject[] _asteroids = new GameObject[3];

    private float _respawnTime = 1.2f;

    private Vector2 _screenBounds;



    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(AsteroidWave());
    }


    private void SpawnAsteroid()
    {
        int rand = Random.Range(0, _asteroids.Length - 1);
        GameObject a = Instantiate(_asteroids[rand]) as GameObject;
        a.transform.position = new Vector3(Random.Range(-_screenBounds.x*0.85f,   _screenBounds.x*0.85f), _screenBounds.y * 1.5f, -1);
    }

    IEnumerator AsteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(_respawnTime);
            SpawnAsteroid();
        }
    }
}
