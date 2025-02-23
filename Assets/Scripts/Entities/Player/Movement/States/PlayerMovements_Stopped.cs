using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements_Stopped : State<PlayerMovements>
{
    public PlayerMovements_Stopped(PlayerMovements origin, Layer<PlayerMovements> parentLayer) : base(origin, parentLayer)
    {

    }
    public override void OnUpdate()
    {
        if (origin.player.stunned == false) parentLayer.SwitchState("Moving");
        base.OnUpdate();
    }
}
