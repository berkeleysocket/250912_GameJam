using UnityEngine;

namespace AJ._01.Scripts
{
    public class Trace : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float interactRadius = 3f;
        public float InteractRadius => interactRadius;

        private void OnEnable()
        {
            if (lifeTime > 0f) Destroy(gameObject, lifeTime);
        }
    }
}
