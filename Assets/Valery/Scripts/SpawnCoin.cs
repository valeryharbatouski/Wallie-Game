using UnityEngine;
using Random = UnityEngine.Random;

namespace Valery
{

    public class SpawnCoin : MonoBehaviour
    {
        [SerializeField] private GameObject coins;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _rewardCount = 5;

        private Vector3 _spawnPosition;

        public void Spawner() => Spawn();

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        [ContextMenu("Spawn Coin")]
        private void Spawn()
        {
            for (int i = 0; i < _rewardCount; i++)
            {
                Vector3 position = new Vector3(Random.Range(-1.0F, 2.0F), 1, Random.Range(-1.0F, 2.0F));
                _spawnPosition = _enemy.transform.position + position;
                Instantiate(coins, _spawnPosition, Quaternion.Euler(0, 0, 90));
            }

        }
    }
}
