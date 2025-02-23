using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionTime;
    protected override void UponDestroy()
    {
        Explosion tmp = explosion.Create(transform.position, Quaternion.identity).GetComponent<Explosion>();
        tmp.Set(origin as ExplosiveGunData, enemy, explosionSound);
    }
}
