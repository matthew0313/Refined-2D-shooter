using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [HideInInspector] public Player player;

    public Transform movingPart, rotator;

    public float moveSpeed;
    Layer<PlayerMovements> topLayer;

    public float dashLength;
    public float dashSpeed;
    public float dashCooldown;
    public float dashCooldownCounter = 0.0f;
    public GameObject dashEffect;
    public float dashEffectRate;
    public PlayerMovements()
    {
        topLayer = new PlayerMovements_TopLayer(this);
    }
    private void Awake()
    {
        player = GetComponent<Player>();
        topLayer.OnStateEnter();
    }
    public void OnUpdate()
    {
        if (!player.canMove) return;
        Vector2 mousePos = IInput.AimingPosition();
        float dx = rotator.position.x - mousePos.x;
        float dy = rotator.position.y - mousePos.y;
        float dir = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        dir += 180;
        rotator.rotation = Quaternion.Euler(0, 0, dir);
        player.angle = dir;
        topLayer.OnUpdate();
    }
    private void FixedUpdate()
    {
        topLayer.OnFixedUpdate();
    }
}
