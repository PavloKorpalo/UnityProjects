using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TankEngine : MonoBehaviour
{
    [SerializeField] private GameObject _dulo;

    [SerializeField] private GameObject _tower;

    private int _healthPoints;

    private Joystick _moveJoystick;

    private Joystick _towerJoystick;

    private bool _canShoot = true   ;

    private Rigidbody _tankEngine;

    private Vector3 _firstPositionTouch;

    private Vector3 _lastPositionTouch;

    private float _dragDistance;

    public static bool Pause;

    public TankModel TankModel;


    public string Team;



    void Start()
    {
        _dragDistance = Screen.height * 15 / 100;
        Init();
    }

    void Init()
    {
        Pause = true;
        _tankEngine = GetComponent<Rigidbody>();
        _healthPoints = TankModel.HealthPoint;
        _moveJoystick = GameObject.FindWithTag("Move Joystick").GetComponent<Joystick>();
        _towerJoystick = GameObject.FindWithTag("Tower Joystick").GetComponent<Joystick>();
        if (gameObject.tag == "Player")
        {
            Camera.main.GetComponent<CameraScript>().Target = gameObject.transform;
            Camera.main.GetComponent<CameraScript>().Offset = Camera.main.transform.position - gameObject.transform.position;
        }
        StartCoroutine(StartGamePause());
    }

    void FixedUpdate()
    {
        if(!Pause)
        {
            Move();

            Rotates();

            RotateTower();
        }
    }
    private void Move()
    {
        if (gameObject.tag == "Player")
        {
            Vector3 move = transform.forward * _moveJoystick.Vertical * TankModel.MovementSpeed * Time.deltaTime;
            _tankEngine.MovePosition(_tankEngine.position + move);
        }

    }
    private void Rotates()
    {
        if (gameObject.tag == "Player")
        {        

            float rotation = _moveJoystick.Horizontal * TankModel.RotationSpeed * Time.deltaTime;

            Quaternion rotateAngle = Quaternion.Euler(0, rotation, 0);

            Quaternion currentAngle = _tankEngine.rotation * rotateAngle;

            _tankEngine.MoveRotation(currentAngle);
        }
    }
    private void RotateTower()
    {
        if (_towerJoystick.Horizontal > 0 && gameObject.tag == "Player")
        {
            float angleRotate = Time.deltaTime * TankModel.RotationSpeed;
            _tower.transform.Rotate(0, angleRotate, 0);
        }
        else if (_towerJoystick.Horizontal < 0 && gameObject.tag == "Player")
        {
            float angleRotate = Time.deltaTime * TankModel.RotationSpeed * (-1);
            _tower.transform.Rotate(0, angleRotate, 0);
        }

    }
    public void Shoot()
    {
        if (_canShoot && !Pause)
        {
            GameObject bullet = TankModel.Bullet;
            Vector3 spawnPoint = _dulo.transform.position;
            Quaternion spawnRoot = _dulo.transform.rotation;
            bullet.GetComponent<BulletScript>().Team = Team;
            bullet.GetComponent<BulletScript>().TankModel = TankModel;
            Instantiate(bullet, spawnPoint, spawnRoot);
            _canShoot = false;
            StartCoroutine(NoFire());
        }
    }
    public int GetHealthPoints()
    {
        return _healthPoints;
    }

    public void GetDamage(int damage)
    {
        _healthPoints -= damage;
        Debug.Log(_healthPoints);
        if (_healthPoints <= 0)
        {
            Destroyed();
        }
    }
    private void Destroyed()
    {
        Destroy(gameObject);
        Destroy(this);
    }
    IEnumerator NoFire()
    {
        yield return new WaitForSeconds(TankModel.DelayTime);
        _canShoot = true;
    }
    IEnumerator StartGamePause()
    {
        yield return new WaitForSeconds(3.5f);
        Pause = false;
    }

}
