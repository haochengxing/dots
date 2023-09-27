using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sort2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int[] array = { 9, 5, 8, 7, 2, 6, 4, 1, 3 };

        //quickSort(array, 0, array.Length - 1);

        //Debug.Log(string.Join(",", array));


        int[] array2 = {0, 9, 5, 8, 7, 2, 6, 4, 1, 3 };

        HeapSort(array2, array2.Length-1);

        array2[0] = 0;

        Debug.Log(string.Join(",", array2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }


    void quickSort(int[] array, int p, int r)
    {

        if (p < r)
        {

            //int q = partition(array, p, r);
            int q = randomized_partition(array, p, r);

            quickSort(array, p, q - 1);

            quickSort(array, q + 1, r);

        }

    }

    int partition(int[] array, int p, int r)
    {

        int x = array[r];
        int i = p - 1;

        for (int j = p; j <= r - 1; j++)
        {

            if (array[j] <= x)
            {

                i = i + 1;

                swap(array, i, j);

            }

        }

        swap(array, i + 1, r);

        return i + 1;

    }

    int randomized_partition(int[] array, int p, int r)
    {

        int i = new System.Random().Next(p,r);

        swap(array, i, r);

        return partition(array, p,r);

    }


    void Heapify(int[] array, int v, int n)
    {
        int i, j;
        i = v; j = 2 * i;
        array[0] = array[i];
        while (j <= n)
        {
            if (j < n && array[j] < array[j + 1])
            {
                j++;
            }
            if (array[0] < array[j])
            {
                array[i] = array[j]; 
                i = j;
                j = 2 * i;
            }
            else
            {
                j = n + 1;
            }
        }
        array[i] = array[0];
    }

    void HeapSort(int[] array, int n)
    {
        int i;
        for (i = n / 2; i >= 1; i--)
        {
            Heapify(array, i, n);
        }
        for (i = n; i > 1; i--)
        {
            array[0] = array[i];
            array[i] = array[1];
            array[1] = array[0];
            Heapify(array, 1, i - 1);
        }
    }


}
