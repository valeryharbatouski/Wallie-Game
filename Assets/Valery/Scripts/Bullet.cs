
using System.Collections;
using UnityEngine;

namespace Valery
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 50;
        [SerializeField] private int _damage = 10;

        private Rigidbody _rb;

        public int DamageValue() => _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rb.velocity = transform.forward * _speed;
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}