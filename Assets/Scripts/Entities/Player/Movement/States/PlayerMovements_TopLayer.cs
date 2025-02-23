using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements_TopLayer : Layer<PlayerMovements>
{
    public PlayerMovements_TopLayer(PlayerMovements origin) : base(origin, null)
    {
        defaultState = new PlayerMovements_Moving(origin, this);
        AddState("Moving", defaultState);
        AddState("Dashing", new PlayerMovements_Dashing(origin, this));
        AddState("Stopped", new PlayerMovements_Stopped(origin, this));
    }
}
