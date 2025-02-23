using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public int magLeft, ammoLeft;
    GunData dt;
    public Gun(GunData data, MonoBehaviour user, bool enemy, AudioSource audioSource) : base(data, user, enemy, audioSource)
    {
        dt = data;
        magLeft = dt.magSize;
        ammoLeft = dt.maxAmmo;
    }
    public override bool CanFire()
    {
        return base.CanFire() && magLeft > 0 && reloading == null;
    }
    public override void Fire(Transform firePoint, float dir)
    {
        magLeft--;
        audio.clip = dt.shot; audio.Play();
        Bullet bul = dt.bullet.Create(firePoint.position, Quaternion.Euler(0, 0, dir + Random.Range(-dt.spread, dt.spread))).GetComponent<Bullet>();
        bul.Set(dt, enemy);
        base.Fire(firePoint, dir);
    }
    IEnumerator reloading = null;
    public float reloadProgress = 0.0f;
    public void Reload(MonoBehaviour reloader)
    {
        if (reloading != null || magLeft >= dt.magSize || ammoLeft <= 0 && !enemy) return;
        reloading = Reloading();
        reloader.StartCoroutine(reloading);
    }
    public IEnumerator Reloading()
    {
        audio.clip = dt.reload; audio.Play();
        while(reloadProgress < dt.reloadTime)
        {
            reloadProgress += Time.deltaTime;
            yield return null;
        }
        int tmp = (enemy) ? dt.magSize : Mathf.Min(ammoLeft, dt.magSize - magLeft);
        magLeft += tmp;
        if(!enemy) ammoLeft -= tmp;
        reloadProgress = 0.0f;
        reloading = null;
        yield break;
    }
    public override string AmmoDesc()
    {
        return magLeft + "/" + ammoLeft;
    }
}
