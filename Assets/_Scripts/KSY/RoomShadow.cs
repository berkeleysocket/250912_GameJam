using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ksy.Scripts
{
    public class RoomShadow : MonoBehaviour
    {
        private SpriteRenderer _spRenderer;
        private Coroutine _currentFade;
        private Coroutine _currentShow;

        void Awake()
        {
            _spRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if(Keyboard.current.lKey.wasPressedThisFrame) Fade();
            if(Keyboard.current.oKey.wasPressedThisFrame) Show();
        }
        public void Fade()
        {
            if(_currentFade == null && _currentFade == null)
                _currentFade = StartCoroutine(_fade());
        }
        public void Show()
        {
            if(_currentFade == null && _currentFade == null)
                _currentShow = StartCoroutine(_show());
        }
        private IEnumerator _fade()
        {
            while(_spRenderer.color.a > 0.5)
            {
                var color = _spRenderer.color;
                color.a -= 0.01f;
                _spRenderer.color = color;
                yield return new WaitForSeconds(0.005f);
            }

            _currentFade = null;
        }
        private IEnumerator _show()
        {
            while(_spRenderer.color.a < 1)
            {
                var color = _spRenderer.color;
                color.a += 0.01f;
                _spRenderer.color = color;
                yield return new WaitForSeconds(0.005f);
            }

            _currentShow = null;
        }
    }
}

