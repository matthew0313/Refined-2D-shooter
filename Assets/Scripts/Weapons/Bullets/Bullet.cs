using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledPrefab
{
    protected GunData origin;
    protected bool enemy = false;
    public void Set(GunData m_origin, bool m_enemy)
    {
        origin = m_origin;
        enemy = m_enemy;
        Debug.Log("EEE");
    }
    float counter = 0.0f;
    public override void OnCreate()
    {
        counter = 0.0f;
        base.OnCreate();
    }
    private void Update()
    {
        Debug.Log(origin.name);
        if (counter * origin.bulletSpeed < origin.range)
        {
            counter += Time.deltaTime;
            Vector3 dir = new Vector3(1, 1, (transform.rotation.z >= 0) ? transform.rotation.z : transform.rotation.z + 360);
            transform.Translate(Vector2.right * dir * origin.bulletSpeed * Time.deltaTime);
        }
        else Destroy();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("BulletIgnore")) return;
        if (!enemy)
        {
            if (collision.CompareTag("EnemyReflector"))
            {
                Reflect(collision);
                return;
            }
            if (collision.CompareTag("PlayerHitbox") || collision.CompareTag("PlayerReflector")) return;
            if (collision.CompareTag("Enemy"))
            {
                Hit(collision);
            }
        }
        if (enemy)
        {
            if (collision.CompareTag("PlayerReflector"))
            {
                Reflect(collision);
                return;
            }
            if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyReflector")) return;
            if (collision.CompareTag("PlayerHitbox"))
            {
                Hit(collision);
            }
        }
        Destroy();
    }
    void Reflect(Collider2D collision)
    {
        enemy = !enemy;
        transform.rotation = collision.transform.rotation;
        return;
    }
    public virtual void Hit(Collider2D collision)
    {
        DamagableEntity a = collision.transform.parent.GetComponent<DamagableEntity>();
        origin.GiveDamage(a);
        if(origin.knockback > 0.0f)
        {
            a.GetKnockback(transform.right.normalized * origin.knockback);
        }
    }
    public bool IsEnemyBullet()
    {
        return enemy;
    }
}
