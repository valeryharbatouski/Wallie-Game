using System;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private Coin _coin;
    [SerializeField] private int _coinCounter = 0;
    [SerializeField] private Text Text;

    [SerializeField] private Slider _heathBar;
    [SerializeField] private float _healthValue = 100;
    [SerializeField] private BotBullet _bullet;

    [SerializeField] private Animator _animator;

    [SerializeField] private VariableJoystick _joystick;
    
    
    private Vector3 _input;
    private static readonly int IsWalking = Animator.StringToHash("isRunning");

    private void Awake()
    {
        _heathBar.value = _healthValue;
        _animator = GetComponent<Animator>();
        _animator.ResetTrigger(IsWalking);
    }

    private void Update()
    {
        Inputs();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        // _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        
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
        _rb.MovePosition(transform.position + transform.forward * (_input.normalized.magnitude * _speed * Time.deltaTime));
        _animator.SetTrigger(IsWalking);
        }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Coin>())
        {
            var value = _coin.Value();
            _coinCounter += value;
            Text.text = _coinCounter.ToString();
        }
        if (collider.GetComponent<BotBullet>())
        _healthValue -= _bullet.DamageValue();
        _heathBar.value = _healthValue;
    }
}
