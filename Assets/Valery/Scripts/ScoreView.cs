using UnityEngine;
using UnityEngine.UI;

namespace Valery
{
    [RequireComponent(typeof(Text))]
    public class ScoreView : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        public void Set(int value)
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();
            }

            _text.text = value.ToString();
        }
    }
}