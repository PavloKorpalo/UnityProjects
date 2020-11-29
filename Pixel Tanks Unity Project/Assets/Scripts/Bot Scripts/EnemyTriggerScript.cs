using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerScript : MonoBehaviour
{
    private string _team;
    private bool _locked;

    private GameObject _target;
    void Start()
    {
        _team = gameObject.GetComponentInParent<TankEngine>().Team;
        _locked = false;
    }
    void Update()
    {
        if(_target!=null && _locked)
        {
            gameObject.GetComponentInParent<AIScript>().Attack(_target);
        }

        IsDestroyed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_locked)
        {
            TankEngine tankEngine = other.GetComponent<TankEngine>();
            if (tankEngine != null)
            {
                if (tankEngine.Team != _team)
                {
                    _target = other.gameObject;
                    _locked = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_target == other.gameObject)
        {
            _locked = false;
            gameObject.GetComponentInParent<AIScript>().StopAttack();
        }        
    }
    private void IsDestroyed()
    {
        if (_target != null)
        {
            if (_target.GetComponent<TankEngine>().GetHealthPoints() <= 0)
            {
                _locked = false;
                gameObject.GetComponentInParent<AIScript>().StopAttack();
            }
        }
      
    }
}
