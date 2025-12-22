using _Scripts.Core.Structs;
using _Scripts.Core.Utility;
using UnityEngine;

namespace _Scripts.Core.Events
{    
    [CreateAssetMenu(fileName = "EmptyEventChannel", menuName = "EventChannel/Empty")]
    public class EmptyEventChannel : EventChannel<Empty>
    {
        
    }
}
