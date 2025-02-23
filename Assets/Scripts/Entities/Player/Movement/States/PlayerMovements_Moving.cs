using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements_Moving : State<PlayerMovements>
{
    public PlayerMovements_Moving(PlayerMovements origin, Layer<PlayerMovements> parentLayer) : base(origin, parentLayer)
    {

    }
    public override void OnUpdate()
    {
        if(origin.player.stunned == true)
        {
            parentLayer.SwitchState("Stopped");
        }
        origin.dashCooldownCounter += Time.deltaTime;
        float moveX = IInput.HorizontalAxis();
        float moveY = IInput.VerticalAxis();
        origin.movingPart.Translate(new Vector2(moveX, moveY) * origin.moveSpeed * Time.deltaTime, Space.World);
        if (IInput.DashKeyDown() && origin.dashCooldownCounter >= origin.dashCooldown && (IInput.HorizontalAxisRaw()!=0 || IInput.VerticalAxisRaw()!=0))
        {
            origin.dashCooldownCounter = 0.0f;
            parentLayer.SwitchState("Dashing");
        }
        base.OnFixedUpdate();
    }
}
