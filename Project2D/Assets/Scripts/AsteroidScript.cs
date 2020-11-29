using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    [SerializeField] private float _speed;

    [SerializeField] private GameObject _heart;

    private Rigidbody2D _rigidbody;

    private Vector2 _screenBounds;

    private float _heartDropRate = 0.17f;

    public GameObject asteroidExplosion;

    


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(0, -_speed);
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z ));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _screenBounds.y*-2)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

       

        var lazer = other.gameObject;

        if(other.tag == "Asteroid")
        {
            return;
        }

        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);

        if (Random.Range(0f,1f)<=_heartDropRate)
        {
            Instantiate(_heart, transform.position, Quaternion.identity);
        }
      

        if (other.tag == "Player")
        {
            PlayerScript playerDamage = other.GetComponent<PlayerScript>();
            playerDamage.PlayerTakeDamage(20);
           
        }

        if(lazer.tag == "Lazer")
        {
            Destroy(lazer);
        }

       


        Destroy(gameObject);
       

    }
}
