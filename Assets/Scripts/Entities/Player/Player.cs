using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovements), typeof(PlayerWeapons), typeof(PlayerUIs))]
public class Player : DamagableEntity
{
    PlayerMovements m_playerMovements;
    PlayerWeapons m_playerWeapons;
    PlayerUIs m_playerUIs;

    public bool active = true;
    public bool canMove = true;
    public float angle = 0.0f;

    public override void Awake()
    {
        GameManager.Instance.onGameOver.AddListener(delegate { canMove = false; });
        m_playerMovements = GetComponent<PlayerMovements>();
        m_playerWeapons = GetComponent<PlayerWeapons>();
        m_playerUIs = GetComponent<PlayerUIs>();
        base.Awake();
    }
    private void Update()
    {
        if (!active) return;
        m_playerMovements.OnUpdate();
        m_playerWeapons.OnUpdate();
        m_playerUIs.OnUpdate();
    }
    public override void Die()
    {
        GameManager.Instance.GameOver();
    }
}
