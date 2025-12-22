using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    [AddComponentMenu("Director/Renderer")]
    public class DirectorRenderer : MonoBehaviour
    {
        private Director _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<Director>();
        }

        public void Flip(Vector2 value)
        {
            if(_owner == null) return;
        
            if(value.x < 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
