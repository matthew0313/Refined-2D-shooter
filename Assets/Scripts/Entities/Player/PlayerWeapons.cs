using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    Player player;

    [SerializeField] Transform firePoint;
    [SerializeField] WeaponData[] startWeapons = new WeaponData[5];
    public bool canFire = true;

    Weapon[] weapons = new Weapon[5];
    int currentWeapon = 0;
    public Weapon weapon { get { return weapons[currentWeapon]; } }
    public static float volume
    {
        get { return 1.0f; }
    }
    private void Awake()
    {
        player = GetComponent<Player>();
        for(int i = 0; i < 5; i++)
        {
            if (startWeapons[i] == null) continue;
            Wield(startWeapons[i], i);
        }
    }
    public void Wield(WeaponData data, int index)
    {
        AudioSource tmp;
        if (weapons[index] != null) tmp = weapons[index].audio;
        else
        {
            tmp = gameObject.AddComponent<AudioSource>();
            tmp.playOnAwake = false; tmp.volume = volume;
        }
        weapons[index] = data.CreateWeapon(this, false, tmp);
    }
    public void OnUpdate()
    {
        if (player.stunned || canFire == false) return;
        int a = IInput.WeaponChoose();
        if (a != -1) SwapWeapon(a);
        if (weapon.data.auto)
        {
            if (IInput.FireKey())
            {
                weapon.AttemptFire(firePoint, player.angle);
            }
        }
        else
        {
            if (IInput.FireKeyDown())
            {
                weapon.AttemptFire(firePoint, player.angle);
            }
        }
        if (weapon.data.Type() == WeaponType.Gun && IInput.ReloadKey())
        {
            (weapon as Gun).Reload(this);
        }
    }
    void SwapWeapon(int index)
    {
        if (weapons[index] == null) return;
        else
        {
            currentWeapon = index;
        }
    }
}
