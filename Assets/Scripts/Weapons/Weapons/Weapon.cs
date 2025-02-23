using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    protected WeaponData m_data;
    public WeaponData data { get { return m_data; } }
    protected float counter = 0.0f;
    public float counterProg { get { return counter; } }
    protected bool enemy = false;
    public AudioSource audio;
    public Weapon(WeaponData m_m_data, MonoBehaviour user, bool m_enemy, AudioSource audioSource)
    {
        m_data = m_m_data;
        user.StartCoroutine(Cooldown());
        enemy = m_enemy;
        audio = audioSource;
    }
    IEnumerator Cooldown()
    {
        while (true)
        {
            counter += Time.deltaTime;
            yield return null;
        }
    }
    public bool AttemptFire(Transform firePoint, float dir)
    {
        bool tmp = CanFire();
        if (tmp) Fire(firePoint, dir);
        return tmp;
    }
    public virtual bool CanFire()
    {
        if (counter < data.fireRate) return false;
        else return true;
    }
    public virtual void Fire(Transform transform, float dir)
    {
        counter = 0.0f;
    }
    public virtual string AmmoDesc()
    {
        return "--/--";
    }
    public virtual void Destroy()
    {

    }
}
