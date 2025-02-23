using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements_Dashing : State<PlayerMovements>
{
    public PlayerMovements_Dashing(PlayerMovements origin, Layer<PlayerMovements> parentLayer) : base(origin, parentLayer)
    {

    }
    Vector2 dashDir;
    public override void OnStateEnter()
    {
        counter = 0.0f;
        effectCounter = 0.0f;
        origin.player.invincible = true;
        dashDir = new Vector2(IInput.HorizontalAxis(), IInput.VerticalAxis()).normalized;
        base.OnStateEnter();
    }
    float counter = 0.0f;
    float effectCounter = 0.0f;
    public override void OnUpdate()
    {
        if (effectCounter < origin.dashEffectRate) effectCounter += Time.deltaTime;
        else
        {
            origin.dashEffect.Create(origin.transform.position, origin.rotator.rotation);
            effectCounter -= origin.dashEffectRate;
        }
        if (counter * origin.dashSpeed < origin.dashLength)
        {
            counter += Time.deltaTime;
            origin.movingPart.Translate(dashDir * origin.dashSpeed * Time.deltaTime, Space.World);
        }
        else parentLayer.SwitchState("Moving");
        base.OnFixedUpdate();
    }
    public override void OnStateExit()
    {
        origin.player.invincible = false;
        base.OnStateExit();
    }
}
