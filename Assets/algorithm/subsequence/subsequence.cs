using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subsequence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int i = 0, j = 0, len = 0;

        int[] a = { 3, 5, 7, 1, 2, 8 };
        int n = a.Length;
        int[] b = new int[n];

        b[0] = 1;

        for (i = 1; i < n; i++)
        {

            for (j = 0, len = 0; j < i; j++)
            {

                if (a[j] <= a[i] && len < b[j])

                    len = b[j];

            }

            b[i] = len + 1;

        }

        Debug.Log(maxL(b, n));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int maxL(int[] b, int n)
    {

        int i, temp = 0;

        for (i = 0; i < n; i++)
        {

            if (b[i] > temp)

                temp = b[i];

        }

        return temp;

    }
}
