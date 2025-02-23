using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveGun Data", menuName = "Scriptables/Weapon Datas/Gun Datas/ExplosiveGun Data", order = 2)]
public class ExplosiveGunData : GunData
{
    [SerializeField] float m_explosionSize;
    [SerializeField] float m_explosionTime;
    public float explosionSize { get { return m_explosionSize; } }
    public float explosionTime { get { return m_explosionTime; } }
    public override string GetShopDesc()
    {
        return base.GetShopDesc() + "\n" +
            "Explosion: " + explosionSize;
    }
}
