using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour

{
    [SerializeField] private Transform _lazerGun1;

   

    [SerializeField] private GameObject _lazerShot1;

    

    [SerializeField] private GameObject _playerExplosion;

    [SerializeField] private float _speed;
  
    [SerializeField] private float _delay;

    private float _nextShot;

    public int playerMaxHealth = 100;

    public int playerCurrentHealth;

    public AudioSource playerExplosionSound;

    public SliderScript healthBar;


    Rigidbody2D player;




    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        PlayerShot();
   
    }

   
    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        player.velocity = new Vector2(moveHorizontal, moveVertical) * _speed;


    }

    

    private void PlayerShot()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time > _nextShot)
        {
            Instantiate(_lazerShot1, _lazerGun1.position, Quaternion.Euler(0, 0, 90f));
            
            _nextShot = Time.time + _delay;
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage;

        healthBar.SetHealth(playerCurrentHealth);

        Debug.Log("Ship is damaged");

        if(playerCurrentHealth <= 0)
        {

            Instantiate(playerExplosionSound);

            Instantiate(_playerExplosion, transform.position, Quaternion.identity);

            FindObjectOfType<GameManager>().EndGame();

            Destroy(gameObject);
        }
    }


    public void PlayerTakeHeal(int heal)
    {
        playerCurrentHealth += heal;

        healthBar.SetHealth(playerCurrentHealth);

        Debug.Log("Ship is healed");

        if (playerCurrentHealth >= 100)
        {
            playerCurrentHealth = 100;
        }
    }


}
