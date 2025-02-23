using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovements), typeof(EnemyWeapons), typeof(EnemyUIs))]public class Enemy : DamagableEntity
{
    [HideInInspector] public Player player;
    public int moneyReward;
    public float dist = 0.0f, dir = 0.0f;

    EnemyMovements m_enemyMovements;
    EnemyWeapons m_enemyWeapons;
    EnemyUIs m_enemyUIs;
    EnemySpawner spawner;
    public override void Awake()
    {
        GameManager.Instance.onGameOver.AddListener(Die);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        spawner.EnemyAdd();
        m_enemyMovements = GetComponent<EnemyMovements>();
        m_enemyWeapons = GetComponent<EnemyWeapons>();
        m_enemyUIs = GetComponent<EnemyUIs>();
        base.Awake();
    }
    private void Update()
    {
        m_enemyMovements.OnUpdate();
        m_enemyWeapons.OnUpdate();
        m_enemyUIs.OnUpdate();
    }
    public override void Die()
    {
        GameManager.Instance.EarnMoney(moneyReward);
        spawner.EnemyRemove();
        base.Die();
    }
}