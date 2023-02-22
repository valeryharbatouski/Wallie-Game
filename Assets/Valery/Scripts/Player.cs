using System;
using UnityEngine;
using UnityEngine.UI;

namespace Valery
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _turnSpeed = 360;
        [SerializeField] private Coin _coin;

        [Header("Health")] [SerializeField] private Health _health;

        [SerializeField] private BotBullet _bullet;

        [SerializeField] private Animator _animator;

        [SerializeField] private VariableJoystick _joystick;

        private Rigidbody _rb;
        private Shoot _shoot;
        private Vector3 _input;
        
        private static readonly int IsWalking = Animator.StringToHash("isRunning");

        public event Action<Coin> CoinColleted;
        public event Action<BotBullet> HittedByBullet;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            _animator.ResetTrigger(IsWalking);
            _shoot = GetComponent<Shoot>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(_shoot.SingleShot());
            }

            Inputs();
            Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Inputs()
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            // _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        }

        private void Look()
        {
            if (_input != Vector3.zero)
            {
                var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                var skewedInput = matrix.MultiplyPoint3x4(_input);
                var relative = (transform.position + skewedInput) - transform.position;
                var rot = Quaternion.LookRotation(relative, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
            }
        }

        private void Move()
        {
            _rb.MovePosition(transform.position +
                             transform.forward * (_input.normalized.magnitude * _speed * Time.deltaTime));
            _animator.SetTrigger(IsWalking);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Coin>())
            {
                CoinColleted?.Invoke(_coin);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<BotBullet>())
            {
                HittedByBullet?.Invoke(_bullet);
                _health.TakeDamage(_bullet.DamageValue());
            }
        }
    }
}
