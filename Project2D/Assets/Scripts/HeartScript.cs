using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HeartScript : MonoBehaviour
{

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    private Vector2 _screenBounds;

    public AudioSource heartSound;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(0, -_speed);
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _screenBounds.y * -2)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 4.0f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Asteroid" || other.tag == "Lazer")
        {
            return;
        }

        if (other.tag == "Player")
        {

            Instantiate(heartSound);
            PlayerScript playerHeal = other.GetComponent<PlayerScript>();
            playerHeal.PlayerTakeHeal(10);

        }

        Destroy(gameObject);
    }

}
