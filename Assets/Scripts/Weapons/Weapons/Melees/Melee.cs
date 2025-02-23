using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    MeleeData dt;
    public Melee(MeleeData data, MonoBehaviour user, bool enemy, AudioSource audioSource) : base(data, user, enemy, audioSource)
    {
        dt = data;
    }
    public override void Fire(Transform firePos, float dir)
    {
        audio.clip = dt.slashing; audio.Play();
        Slash a = dt.slash.Create(firePos.position, Quaternion.Euler(0, 0, dir)).GetComponent<Slash>();
        a.Set(dt, enemy);
        base.Fire(firePos, dir);
    }
}
