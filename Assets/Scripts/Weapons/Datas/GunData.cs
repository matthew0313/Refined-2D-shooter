using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Scriptables/Weapon Datas/Gun Datas/Gun Data", order = 0)]
public class GunData : WeaponData
{
    [SerializeField] int m_magSize, m_maxAmmo;
    [SerializeField] float m_range, m_bulletSpeed;
    [SerializeField] float m_spread;
    [SerializeField] float m_reloadTime;
    [SerializeField] GameObject m_bullet;
    [SerializeField] AudioClip m_shot, m_reload;
    public int magSize { get { return m_magSize; } }
    public int maxAmmo { get { return m_maxAmmo; } }
    public float range { get { return m_range; } }
    public float bulletSpeed { get { return m_bulletSpeed; } }
    public float spread { get { return m_spread; } }
    public float reloadTime { get { return m_reloadTime; } }
    public GameObject bullet { get { return m_bullet; } }
    public AudioClip shot { get { return m_shot; } }
    public AudioClip reload { get { return m_reload; } }
    public override Weapon CreateWeapon(MonoBehaviour user, bool enemy, AudioSource audio)
    {
        return new Gun(this, user, enemy, audio);
    }
    public override string GetShopDesc()
    {
        return base.GetShopDesc() +
            "Mag Size: " + magSize + "\n" +
            "Ammo: " + maxAmmo + "\n" +
            "Spread: " + spread;
    }
    public override WeaponType Type()
    {
        return WeaponType.Gun;
    }
}
