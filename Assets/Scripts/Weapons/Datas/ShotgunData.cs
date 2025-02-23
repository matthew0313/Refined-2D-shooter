using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun Data", menuName = "Scriptables/Weapon Datas/Gun Datas/Shotgun Data", order = 1)]
public class ShotgunData : GunData
{
    [SerializeField] int m_shotCount;
    public int shotCount { get { return m_shotCount; } }
    public override Weapon CreateWeapon(MonoBehaviour user, bool enemy, AudioSource audio)
    {
        return new Shotgun(this, user, enemy, audio);
    }
    public override string GetShopDesc()
    {
        return
            "Damage: " + damage + "x" + shotCount + "\n" +
            "FireRate: " + fireRate + "\n" +
            ((knockback > 0.0f) ? "Knockback: " + knockback + "\n" : "") +
            "Mag Size: " + magSize + "\n" +
            "Ammo: " + maxAmmo + "\n" +
            "Spread: " + spread;
    }
}
