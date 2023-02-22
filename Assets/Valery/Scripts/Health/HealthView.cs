using System;
using UnityEngine;
using UnityEngine.UI;

namespace Valery
{
    [RequireComponent(typeof(Text))]
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _value;

        private void Start()
        {
            _value = GetComponent<Slider>();
        }

        public void Set(float value)
        {
            if (_value == null)
            {
                _value = GetComponent<Slider>();
            }
            
            _value.value = value;
        }
    }
}