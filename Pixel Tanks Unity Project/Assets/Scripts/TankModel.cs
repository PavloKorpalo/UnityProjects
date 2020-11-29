using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Cannon,
    MachineGun,
    Artillery
}

[Serializable]
public class TankModel
{
    public string Name;
    public float MovementSpeed;
    public float RotationSpeed;
    public float BulletSpeed;
    public float DelayTime;

    public TowerType Tower;

    public int HealthPoint;
    public int Damage;

    public GameObject Bullet;
    public GameObject AllyTankPrefab;
    public GameObject EnemyTankPrefab;
}
