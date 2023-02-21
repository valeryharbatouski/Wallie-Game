
using UnityEngine;

namespace Valery
{
   [RequireComponent(typeof(Rigidbody))]
   [RequireComponent(typeof(Collider))]
   public class Coin : MonoBehaviour
   {
      [SerializeField] private int _value = 10;

      public int Value() => _value;

      private void OnTriggerEnter(Collider _collider)
      {
         if (_collider.gameObject.GetComponent<Player>() != null)
         {
            Destroy(gameObject);
         }

      }
   }
}
