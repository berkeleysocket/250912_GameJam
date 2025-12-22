using UnityEngine;

namespace Ksy.Scripts.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RendererX : MonoBehaviour
    {
        private SpriteRenderer _spRenderer;
        public bool IsFilp { get; private set; }

        private void Awake()
        {
            _spRenderer = GetComponent<SpriteRenderer>();

            Debug.Assert(_spRenderer != null, "<color=red>_spRenderer is null!!</color>");
        }
        public void FilpX(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            bool flip = dir.x < 0;

            _spRenderer.flipX = flip;
            IsFilp = flip;
        }

        public void FilpX(bool value)
        {
            _spRenderer.flipX = value;
            IsFilp = value;
        }
    }
}