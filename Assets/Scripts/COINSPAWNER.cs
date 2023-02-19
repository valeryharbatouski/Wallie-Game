using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class COINSPAWNER : MonoBehaviour
{
    [SerializeField] private GameObject coins;
    [SerializeField] private ShootBot _enemy;

    private Vector3 _spawnPosition;

    public void Spawner() => Spawn();

    private void Awake()
    {
        _enemy = GetComponent<ShootBot>();
    }

    [ContextMenu("Spawn Coin")]
    private void Spawn()
    {
        _spawnPosition = _enemy.transform.position;
        // Vector3 position = new Vector3(Random.Range(-10.0F, 10.0F), 1, Random.Range(-10.0F, 10.0F));
        Instantiate(coins, _spawnPosition, Quaternion.Euler(0,0,90));
        }
}
