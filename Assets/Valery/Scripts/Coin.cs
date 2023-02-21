
using System;
using UnityEngine;

namespace Valery
{
   [RequireComponent(typeof(Rigidbody))]
   [RequireComponent(typeof(Collider))]
   public class Coin : MonoBehaviour
   {
      [SerializeField] private int _value = 10;

      public int Value() => _value;

      public event Action Collected;

      private void OnTriggerEnter(Collider _collider)
      {
         if (_collider.gameObject.GetComponent<Player>() != null)
         {
            Collected?.Invoke();
            Destroy(gameObject);
         }

      }
   }
}
