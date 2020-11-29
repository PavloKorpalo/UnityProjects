using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletScript : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public TankModel TankModel;

    public string Team;

    void Start()
    {
        //Можно сделать эффект взрыва и вставить сюда
        Debug.Log("Бум");
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * TankModel.BulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject collider = other.gameObject;
        if(other.gameObject.tag != "Trigger Box")
        {
            TankEngine tankEngine = collider.GetComponent<TankEngine>();
            if (tankEngine != null)
            {
                if (tankEngine.Team != Team)
                {
                    tankEngine.GetDamage(TankModel.Damage);
                    Destroy(gameObject);
                }
            }
            tankEngine = collider.GetComponentInParent<TankEngine>();
            if (tankEngine != null)
            {
                if (tankEngine.Team != Team)
                {
                    tankEngine.GetDamage(TankModel.Damage);
                    Destroy(gameObject);
                }
            }
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


