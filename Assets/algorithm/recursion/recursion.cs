using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recursion : MonoBehaviour
{
    void Start()
    {
        Debug.Log(factorial(3));
        Debug.Log(fibonacci(3));
    }

    //�׳�
    public static int factorial(int m)
    {
        if (m == 0) return 1;
        else return m * factorial(m - 1);
    }


    //Fibonacci���У�쳲�����������
    public static int fibonacci(int m)
    {
        if (m <= 1) return m;
        else return fibonacci(m - 1) + fibonacci(m - 2);
    }
}
