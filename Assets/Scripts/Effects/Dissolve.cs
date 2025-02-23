using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : PooledPrefab
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] float disappearTime;
    float counter = 0.0f;
    public override void OnCreate()
    {
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f);
        counter = 0.0f;
        base.OnCreate();
    }
    private void Update()
    {
        if (counter < disappearTime)
        {
            counter += Time.deltaTime;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f - counter / disappearTime);
        }
        else Destroy();
    }
}
