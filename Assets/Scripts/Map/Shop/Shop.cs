using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] List<WeaponPricePair> products;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject button;
    List<ShopButton> currentButtons = new List<ShopButton>();
    [SerializeField] GameObject buttonContainer;
    [SerializeField] Text WeaponName, WeaponDesc, WeaponPrice;
    [SerializeField] Dropdown SlotSelector;
    WeaponPricePair selected;
    PlayerWeapons player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapons>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            player.canFire = false;
            ShopMenu.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            player.canFire = true;
            ShopMenu.SetActive(false);
        }
    }
    public void OpenCategory(string tag)
    {
        foreach(ShopButton i in currentButtons)
        {
            i.Destroy();
        }
        currentButtons.Clear();
        List<WeaponPricePair> selected = new List<WeaponPricePair>();
        foreach (WeaponPricePair item in products)
        {
            foreach(string i in item.soldWeapon.tags)
            {
                if (i == tag)
                {
                    ShopButton tmp = button.Create(new Vector2(0.0f, 160.0f - currentButtons.Count*30.0f), Quaternion.identity, buttonContainer.transform).GetComponent<ShopButton>();
                    tmp.Set(item, this);
                    tmp.transform.GetChild(0).GetComponent<Text>().text = item.soldWeapon.name;
                    currentButtons.Add(tmp);
                    break;
                }
            }
        }
    }
    public void SetProduct(WeaponPricePair item)
    {
        selected = item;
        WeaponName.text = item.soldWeapon.name;
        WeaponDesc.text = item.soldWeapon.GetShopDesc();
        WeaponPrice.text = "Price: $" + item.price;
    }
    public void Purchase()
    {
        if(selected == null || selected.price > GameManager.Instance.money)
        {
            return;
        }
        else
        {
            player.Wield(selected.soldWeapon, SlotSelector.value);
            GameManager.Instance.UseMoney(selected.price, "Shop Purchase");
        }
    }
}
[System.Serializable] public class WeaponPricePair
{
    public WeaponData soldWeapon;
    public int price;
}