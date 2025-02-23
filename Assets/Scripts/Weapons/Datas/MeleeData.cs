using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Data", menuName = "Scriptables/Weapon Datas/Melee Data", order = 1)]
public class MeleeData : WeaponData
{
    [SerializeField] GameObject m_slash;
    [SerializeField] float m_slashLifeTime;
    [SerializeField] AudioClip m_slashing;
    public GameObject slash { get { return m_slash; } }
    public float slashLifeTime { get { return m_slashLifeTime; } }
    public AudioClip slashing { get { return m_slashing; } }
    public override Weapon CreateWeapon(MonoBehaviour user, bool enemy, AudioSource audio)
    {
        return new Melee(this, user, enemy, audio);
    }
    public override string GetShopDesc()
    {
        return "Melee\n" + base.GetShopDesc();
    }
    public override WeaponType Type()
    {
        return WeaponType.Melee;
    }
}
