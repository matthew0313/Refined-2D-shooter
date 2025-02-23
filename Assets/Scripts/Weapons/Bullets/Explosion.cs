using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PooledPrefab
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] AudioSource audio;
    ExplosiveGunData origin;
    bool enemy = false;
    float counter = 0.0f;
    public void Set(ExplosiveGunData m_origin, bool m_enemy, AudioClip sound)
    {
        audio.volume = (m_enemy) ? EnemyWeapons.volume : PlayerWeapons.volume;
        audio.clip = sound; audio.Play();
        origin = m_origin;
        transform.localScale = new Vector2(origin.explosionSize, origin.explosionSize);
        enemy = m_enemy;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f);
        counter = 0.0f;
    }
    private void Update()
    {
        if (counter < origin.explosionTime)
        {
            counter += Time.deltaTime;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f - counter/origin.explosionTime);
        }
        else
        {
            Destroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemy && collision.CompareTag("Enemy")) Hit(collision);
        if (enemy && collision.CompareTag("PlayerHitbox")) Hit(collision);
    }
    void Hit(Collider2D collision)
    {
        DamagableEntity a = collision.transform.parent.GetComponent<DamagableEntity>();
        Debug.Log(a.gameObject.name);
        origin.GiveDamage(a);
        if (origin.knockback > 0.0f)
        {
            a.GetKnockback((collision.transform.position - transform.position).normalized * origin.knockback);
        }
    }
}
