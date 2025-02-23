using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    ShotgunData dt;
    public Shotgun(ShotgunData data, MonoBehaviour user, bool enemy, AudioSource audioSource) : base(data, user, enemy, audioSource)
    {
        dt = data;
        magLeft = dt.magSize;
        ammoLeft = dt.maxAmmo;
    }
    public override void Fire(Transform firePoint, float dir)
    {
        magLeft--;
        audio.clip = dt.shot; audio.Play();
        for(int i = 0; i < dt.shotCount; i++)
        {
            Bullet bul = dt.bullet.Create(firePoint.position, Quaternion.Euler(0, 0, dir + Random.Range(-dt.spread, dt.spread))).GetComponent<Bullet>();
            bul.Set(dt, enemy);
        }
        counter = 0.0f;
    }
}
