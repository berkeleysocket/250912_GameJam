using System.Drawing;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField] private IntEventChannel intEventChannel;
    [SerializeField] private int test;

    [ContextMenu("Test")]
    public void Test()
    {
        intEventChannel.Raise(test);
    }
}
