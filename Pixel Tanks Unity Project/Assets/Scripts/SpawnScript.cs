using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ChosedTank
{
    Medium,
    Artillery,
    Light
}
public class SpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoint1;

    [SerializeField] private List<GameObject> _spawnPoint2;

    [SerializeField] private List<GameObject> _destinations;

    public static int ChoosedTank;

    private TanksConfig _tanksConfig;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }
    private void Init()
    {
        string blue = "Blue";
        string red = "Red";
        
        ReadTankConfig();
        SpawnPlayer();
        SpawnBot(blue, _tanksConfig.Tanks[1], _spawnPoint1[1]);
        SpawnBot(blue, _tanksConfig.Tanks[2], _spawnPoint1[2]);
        SpawnBot(red, _tanksConfig.Tanks[0], _spawnPoint2[0]);
        SpawnBot(red, _tanksConfig.Tanks[1], _spawnPoint2[1]);
        SpawnBot(red, _tanksConfig.Tanks[2], _spawnPoint2[2]);
    }
    private void SpawnPlayer()
    {
        TankModel tankModel = _tanksConfig.Tanks[ChoosedTank];
        
        GameObject player = tankModel.AllyTankPrefab;
        Vector3 spawnPos = _spawnPoint1[0].transform.position;

        player.GetComponent<TankEngine>().Team = "Blue";
        player.GetComponent<TankEngine>().TankModel = _tanksConfig.Tanks[ChoosedTank];
        player.tag = "Player";
        player.GetComponentInChildren<EnemyTriggerScript>().enabled = false;
        player.GetComponent<AIScript>().enabled = false;
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        Instantiate(player, spawnPos, transform.rotation);
    }

    private void SpawnBot(string team, TankModel tankModel, GameObject spawnPosition)
    {
        GameObject bot;

        Vector3 spawnPos1 = spawnPosition.transform.position;
        
        if (team == "Red")
        {
            bot = tankModel.EnemyTankPrefab;
            bot.tag = "Enemy Bot";
        }
        else
        {
            bot = tankModel.AllyTankPrefab;
            bot.tag = "Ally Bot";
        }
        bot.GetComponent<TankEngine>().Team = team;
        bot.GetComponent<TankEngine>().TankModel = tankModel;

        bot.GetComponent<AIScript>().enabled = true;
        bot.GetComponent<NavMeshAgent>().enabled = true;
        bot.GetComponent<AIScript>().Destinations = _destinations;

        Instantiate(bot, spawnPos1, transform.rotation);        
    }

    private void ReadTankConfig()
    {
        const string Path = "Configs/TanksConfig";
        _tanksConfig = null;
        _tanksConfig = Resources.Load<TanksConfig>(Path);
    }
}
