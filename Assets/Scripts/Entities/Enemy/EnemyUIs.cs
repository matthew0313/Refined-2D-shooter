using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIs : MonoBehaviour
{
    [SerializeField] RectTransform HpScaler;
    Enemy origin;
    public void OnUpdate()
    {
        origin = GetComponent<Enemy>();
        origin.onDamage.AddListener(delegate
        {
            HpScaler.localScale = new Vector2(origin.health / origin.maxHealth, HpScaler.localScale.y);
        });
    }
}