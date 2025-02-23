using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] Transform rotator, movingPart;
    [SerializeField] float moveSpeed, backStepSpeed, minDist, maxDist;
    Enemy origin;
    private void Awake()
    {
        origin = GetComponent<Enemy>();
    }
    public void OnUpdate()
    {
        Vector2 pos = origin.player.transform.position;
        float dx = rotator.transform.position.x - pos.x;
        float dy = rotator.transform.position.y - pos.y;
        float dir = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        dir += 180;
        origin.dir = dir;
        rotator.rotation = Quaternion.Euler(0, 0, dir);
        if (origin.stunned)
        {
            return;
        }
        origin.dist = Vector2.Distance(movingPart.position, origin.player.transform.position);
        if (origin.dist < minDist)
        {
            movingPart.Translate((movingPart.position - origin.player.transform.position).normalized * backStepSpeed * Time.deltaTime);
        }
        else if (origin.dist > maxDist)
        {
            movingPart.Translate((origin.player.transform.position - movingPart.position).normalized * moveSpeed * Time.deltaTime);
        }
    }
}