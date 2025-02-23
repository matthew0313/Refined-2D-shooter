using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    Enemy origin;
    [SerializeField] Transform firePos;
    [SerializeField] List<WeaponRangePair> weapons = new List<WeaponRangePair>();
    [SerializeField] EnemyBulletDetector meleeGuarder;
    [SerializeField] float meleeGuardCooldown = 3.0f;
    Melee equippedMelee;
    public static float volume
    {
        get { return 0.25f; }
    }
    public void Awake()
    {
        weapons.Sort((WeaponRangePair w1, WeaponRangePair w2) => { return w1.useRange.CompareTo(w2.useRange); });
        foreach(WeaponRangePair i in weapons)
        {
            i.Wield(this);
            if(meleeGuarder.gameObject.activeSelf && i.weapon.data.Type() == WeaponType.Melee && equippedMelee == null)
            {
                equippedMelee = i.weapon as Melee;
            }
        }
        origin = GetComponent<Enemy>();
        if(meleeGuarder.gameObject.activeSelf)
        {
            meleeGuarder.onBulletDetection.AddListener(MeleeGuard);
        }
    }
    float meleeGuardCounter = 0.0f;
    void MeleeGuard()
    {
        if (origin.stunned || meleeGuardCounter < meleeGuardCooldown || equippedMelee == null) return;
        else
        {
            meleeGuardCounter = 0.0f;
            equippedMelee.AttemptFire(firePos, origin.dir);
        }
    }
    public void OnUpdate()
    {
        if (origin.stunned) return;
        if (meleeGuarder.gameObject.activeSelf) meleeGuardCounter += Time.deltaTime;
        foreach(WeaponRangePair i in weapons)
        {
            if(origin.dist <= i.useRange)
            {
                i.weapon.AttemptFire(firePos, origin.dir);
                if (i.weapon.data.Type() == WeaponType.Gun && (i.weapon as Gun).magLeft == 0)
                {
                    (i.weapon as Gun).Reload(this);
                }
                else break;
            }
            else
            {
                if(i.weapon.data.Type() == WeaponType.Gun)
                {
                    (i.weapon as Gun).Reload(this);
                }
            }
        }
    }
    private void OnDestroy()
    {
        foreach(WeaponRangePair i in weapons)
        {

        }
    }
}
[System.Serializable] public class WeaponRangePair
{
    public void Wield(MonoBehaviour user)
    {
        if (data != null)
        {
            AudioSource tmp = user.gameObject.AddComponent<AudioSource>();
            tmp.playOnAwake = false;
            tmp.volume = EnemyWeapons.volume;
            weapon = data.CreateWeapon(user, true, tmp);
        }
    }
    [SerializeField] WeaponData data;
    public Weapon weapon;
    public float useRange;
}
