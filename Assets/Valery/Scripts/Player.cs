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

        [Header("")]
        [SerializeField] private BotBullet _botBullet;
        [SerializeField] private Coin _coin;

        [SerializeField] private Animator _animator;
        //for Android
        // [SerializeField] private VariableJoystick _joystick;

        private Rigidbody _rb;
        private Shoot _shoot;
        private Vector3 _input;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public event Action<Coin> CoinColleted;
        public event Action<BotBullet> PlayerHittedByBullet;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _shoot = GetComponent<Shoot>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(_shoot.SingleShot());
            }

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                _animator.SetTrigger(IsRunning);
            }
            else
            {   _animator.ResetTrigger(IsRunning);
                _animator.Play(Idle);
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
                PlayerHittedByBullet?.Invoke(_botBullet);
            }
        }
    }
}
