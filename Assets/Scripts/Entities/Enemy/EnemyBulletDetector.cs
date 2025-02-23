using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyBulletDetector : MonoBehaviour
{
    [HideInInspector] public UnityEvent onBulletDetection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet tmp = collision.GetComponent<Bullet>();
            if(tmp != null && tmp.IsEnemyBullet() == false)
            {
                onBulletDetection.Invoke();
            }
        }
    }
}