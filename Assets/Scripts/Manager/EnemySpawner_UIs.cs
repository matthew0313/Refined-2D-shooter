using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class EnemySpawner_UIs : MonoBehaviour
{
    EnemySpawner origin;
    [SerializeField] Text WaveText;
    void Awake()
    {
        origin = GetComponent<EnemySpawner>();
    }
    public void OnUpdate()
    {
        if (origin.resting)
        {
            WaveText.text = "Wave " + (origin.currentWaveNum + 2) + "\nStarts in: " + Mathf.CeilToInt(origin.restTimer);
        }
        else
        {
            WaveText.text = "Wave " + (origin.currentWaveNum + 1) + ((origin.lastWave) ? " (Last Wave)" : "") + "\nEnemies: " + origin.enemyLeft;
        }
    }
    public void End()
    {
        WaveText.gameObject.SetActive(false);
    }
}