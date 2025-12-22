using System.Drawing;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField] private IntEventChannel intEventChannel;
    [SerializeField] private int test;
    private void Awake()
    {
        intEventChannel.OnEvent += Test12;
    }

    private void Oestroy()
    {
        intEventChannel.OnEvent -= Test12;
    }

    private void Test12(int a)
    {
        test = a;
    }
}
