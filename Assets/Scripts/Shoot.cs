using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shootPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(PlayerShoot());
            }
        }

        private IEnumerator PlayerShoot()
        {
            Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            yield return new WaitForSeconds(2);
            
        }
    }