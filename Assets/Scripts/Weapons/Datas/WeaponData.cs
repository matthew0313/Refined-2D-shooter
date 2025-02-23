using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField] float m_damage, m_fireRate, m_knockback;
    [SerializeField] bool m_auto;
    [SerializeField] string[] m_tags;
    public float damage { get { return m_damage; } }
    public float fireRate { get { return m_fireRate; } }
    public float knockback { get {  return m_knockback; } }
    public bool auto { get { return m_auto; } }
    public string[] tags { get { return m_tags; } }
    public virtual void GiveDamage(DamagableEntity target)
    {
        target.GetDamage(damage);
    }
    public virtual string GetShopDesc()
    {
        return
            "Damage: " + damage + "\n" +
            "FireRate: " + fireRate + "\n" +
            ((knockback > 0.0f) ? "Knockback: " + knockback + "\n" : "");
    }
    public virtual Weapon CreateWeapon(MonoBehaviour user, bool enemy, AudioSource audio)
    {
        return null;
    }
    public virtual WeaponType Type()
    {
        return WeaponType.Melee;
    }
}
public enum WeaponType
{
    Gun,
    Melee
}