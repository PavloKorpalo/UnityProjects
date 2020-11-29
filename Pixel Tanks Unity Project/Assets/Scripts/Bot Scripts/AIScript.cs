using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{    
    [SerializeField] private GameObject _dulo;

    [SerializeField] private GameObject _tower;

    [SerializeField] private GameObject _boxColliderTrigger;

    private NavMeshAgent _agent;

    private TankModel _tankModel;

    private Transform _target;

    private bool _changeDestination;

    private bool _attacking;

    public List<GameObject> Destinations;

    public string Team;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        AILogic();
    }

    private void LateUpdate()
    {
        Rotation();
    }

    private void Init()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _tankModel = gameObject.GetComponent<TankEngine>().TankModel;
        _agent.speed = _tankModel.MovementSpeed;
        _agent.updateRotation = false;
        _changeDestination = false;
        _attacking = false;
        StartCoroutine(StartGamePause());
    }
    private void Rotation()
    {
        if (_changeDestination && !_attacking && !TankEngine.Pause)
        {
            if (gameObject.tag == "Enemy Bot")
            {

                transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized) * Quaternion.Euler(0, 180f, 0); ;
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
            }
        }
    }
    private void AILogic()
    {
        int i;
        if (_changeDestination && !_attacking && !TankEngine.Pause)
        {
            i = Random.Range(0, 6);
            _target = Destinations[i].transform;
            MoveTowards(_target);
            StartCoroutine(ChangeDestinationTimer());
            _changeDestination = false;
        }

        if(TankEngine.Pause)
        {
            _agent.velocity = Vector3.zero;
        }

        if (transform.position==_agent.pathEndPosition)
        {
            _agent.isStopped = true;
            
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    public void Attack(GameObject target)
    {
        _attacking = true;
        _agent.SetDestination(target.transform.position);
        gameObject.GetComponent<TankEngine>().Shoot();
    }
    public void StopAttack()
    {
        _attacking = false;
    }

    private void MoveTowards(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject trigger = other.gameObject;
        if(trigger.GetComponent<TankEngine>() != null)
        {
            if (gameObject.GetComponent<TankEngine>().Team != gameObject.GetComponent<TankEngine>().Team)
            {
                gameObject.GetComponent<TankEngine>().Shoot();
            }
        }
    }
    IEnumerator ChangeDestinationTimer()
    {
        yield return new WaitForSeconds(8.8f);
        _changeDestination = true;
    }

    IEnumerator StartGamePause()
    {
        yield return new WaitForSeconds(3.5f);
        _changeDestination = true;
    }
}
