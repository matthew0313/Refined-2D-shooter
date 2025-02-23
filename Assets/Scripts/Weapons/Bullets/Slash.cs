using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : PooledPrefab
{
    [SerializeField] SpriteRenderer rend;
    MeleeData origin;
    bool enemy = false;
    public void Set(MeleeData m_origin, bool m_enemy)
    {
        origin = m_origin;
        enemy = m_enemy;
    }
    float counter = 0.0f;
    public override void OnCreate()
    {
        if (!enemy)
        {
            gameObject.tag = "PlayerReflector";
        }
        else
        {
            gameObject.tag = "EnemyReflector";
        }
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f);
        counter = 0.0f;
        base.OnCreate();
    }
    private void Update()
    {
        if (counter < origin.slashLifeTime)
        {
            counter += Time.deltaTime;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f - counter / origin.slashLifeTime);
        }
        else Destroy();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemy && collision.CompareTag("Enemy")) Hit(collision);
        if (enemy && collision.CompareTag("PlayerHitbox")) Hit(collision);
    }
    void Hit(Collider2D collision)
    {
        DamagableEntity a = collision.transform.parent.GetComponent<DamagableEntity>();
        origin.GiveDamage(a);
        if(origin.knockback > 0.0f)
        {
            a.GetKnockback(transform.right.normalized * origin.knockback);
        }
    }
}
