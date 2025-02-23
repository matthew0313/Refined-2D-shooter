using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : PooledPrefab
{
    Button origin;
    void Awake()
    {
        origin = GetComponent<Button>();
    }
    public void Destroy()
    {
        base.Destroy();
    }
    public void Set(WeaponPricePair pair, Shop shop)
    {
        origin.onClick.AddListener(delegate
        {
            shop.SetProduct(pair);
        });
    }
}