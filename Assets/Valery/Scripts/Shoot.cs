
using System.Collections;
using UnityEngine;

namespace Valery
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private int _shootDelay = 2;

        public GameObject Bullet() => _bullet;
        public Transform ShootPiont() => _shootPoint;
        public IEnumerator InfinityShoot() => InfinityShooting();
        public IEnumerator SingleShot() => SingleShoot();


        private IEnumerator InfinityShooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shootDelay);
                Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            }

            //TODO Add null check
        }

        private IEnumerator SingleShoot()
        {
            Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}