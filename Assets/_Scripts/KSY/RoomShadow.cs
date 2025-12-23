using System.Collections;
using UnityEngine;

using Ksy.Scripts.Object;

namespace Ksy.Scripts
{
    public class RoomShadow : MonoBehaviour
    {
        private SpriteRenderer _roomShadow;
        private SpriteRenderer _doorShadow;
        private Coroutine _currentFade;
        private Coroutine _currentShow;

        private bool InPlayer;
        private bool IsFade; 

        void Awake()
        {
            _roomShadow = GetComponent<SpriteRenderer>();
            
            if(gameObject.transform.childCount != 0)
                _doorShadow = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

            var door = gameObject.GetComponentInParent<Door>();
        }
        void Update()
        {
            if(!IsFade && InPlayer)
                Fade();
            else if(IsFade && !InPlayer)
                Show();
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                InPlayer = true;
            }
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                InPlayer = false;
            }
        }
        public void Fade()
        {
            if(_currentFade == null && _currentShow == null)
                _currentFade = StartCoroutine(_fade());
            
        }
        public void Show()
        {
            if(_currentFade == null && _currentShow == null)
                _currentShow = StartCoroutine(_show());
        }
        private IEnumerator _fade()
        {
            if(_roomShadow == null) yield break;

            while(_roomShadow.color.a > 0.5)
            {
                var colorR = _roomShadow.color;
                colorR.a -= 0.01f;
                _roomShadow.color = colorR;

                if(_doorShadow != null)
                {
                    var colorD = _doorShadow.color;
                    colorD.a -= 0.01f;
                    _doorShadow.color = colorD;
                }

                yield return new WaitForSeconds(0.005f);
            }

            _currentFade = null;
            IsFade = true;
        }
        private IEnumerator _show()
        {
            if(_roomShadow == null) yield break;

            while(_roomShadow.color.a < 1)
            {
                var colorR = _roomShadow.color;
                colorR.a += 0.01f;
                _roomShadow.color = colorR;

                if(_doorShadow != null)
                {
                    var colorD = _doorShadow.color;
                    colorD.a += 0.01f;
                    _doorShadow.color = colorD;
                }

                yield return new WaitForSeconds(0.005f);
            }

            _currentShow = null;
            IsFade = false;
        }
    }
}

