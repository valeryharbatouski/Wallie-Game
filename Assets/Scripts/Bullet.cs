
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]private float _speed = 50;
        private Rigidbody _rb;
        [SerializeField] private int _damage = 10;

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