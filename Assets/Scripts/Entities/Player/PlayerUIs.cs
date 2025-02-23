using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIs : MonoBehaviour
{
    PlayerMovements movements;
    PlayerWeapons weapons;
    Player player;

    [SerializeField] RectTransform HpBar, ReloadBar, DashBar;
    [SerializeField] Text WeaponName, Ammo;
    [SerializeField] GameObject Stun;
    private void Awake()
    {
        player = GetComponent<Player>();
        player.onDamage.AddListener(HpBarScale);
        movements = GetComponent<PlayerMovements>();
        weapons = GetComponent<PlayerWeapons>();
    }
    public void OnUpdate()
    {
        ReloadBarScale();
        WeaponDetails();
        DashBarScale();
        StunText();
    }
    void HpBarScale()
    {
        HpBar.localScale = new Vector2(player.health / player.maxHealth, HpBar.localScale.y);
    }
    void ReloadBarScale()
    {
        if (weapons.weapon.data.Type() == WeaponType.Gun && (weapons.weapon as Gun).reloadProgress > 0.0f)
        {
            ReloadBar.localScale = new Vector2(1.0f - (weapons.weapon as Gun).reloadProgress / (weapons.weapon.data as GunData).reloadTime, ReloadBar.localScale.y);
        }
        else if(weapons.weapon.data.Type() == WeaponType.Melee && (weapons.weapon as Melee).counterProg < weapons.weapon.data.fireRate)
        {
            ReloadBar.localScale = new Vector2(1.0f - (weapons.weapon as Melee).counterProg / weapons.weapon.data.fireRate, ReloadBar.localScale.y);
        }
        else ReloadBar.localScale = new Vector2(0.0f, ReloadBar.localScale.y);
    }
    void WeaponDetails()
    {
        WeaponName.text = weapons.weapon.data.name;
        Ammo.text = weapons.weapon.AmmoDesc();
    }
    void DashBarScale()
    {
        DashBar.localScale = new Vector2(Mathf.Min(1.0f, movements.dashCooldownCounter / movements.dashCooldown), DashBar.localScale.y);
    }
    void StunText()
    {
        Stun.SetActive(player.stunned);
    }
}
