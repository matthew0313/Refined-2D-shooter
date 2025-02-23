using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DamagableEntity : MonoBehaviour
{
    Rigidbody2D rb;
    public bool kbImmune = false;
    public bool stunned = false;

    public float maxHealth;
    public float health;
    public bool invincible = false;

    public float knockbackStopLevel = 0.5f;

    public virtual void Awake()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator currentKnockback = null;
    public UnityEvent onDamage = new UnityEvent();
    public void GetDamage(float damage)
    {
        if (invincible || health==0) return;
        health -= damage;
        health = Mathf.Max(health, 0);
        onDamage.Invoke();
        if (health == 0)
        {
            Die();
        }
    }
    public void GetKnockback(Vector2 knockback)
    {
        if (invincible || health == 0 || kbImmune) return;
        rb.velocity += knockback;
        if (currentKnockback == null)
        {
            currentKnockback = ApplyKnockback();
            StartCoroutine(currentKnockback);
        }
    }
    IEnumerator ApplyKnockback()
    {
        stunned = true;
        while (Mathf.Abs(rb.velocityX) > knockbackStopLevel || Mathf.Abs(rb.velocityY) > knockbackStopLevel)
        {
            yield return null;
        }
        rb.velocity = Vector2.zero;
        stunned = false;
        currentKnockback = null;
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
