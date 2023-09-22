using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sort2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 9, 5, 8, 7, 2, 6, 4, 1, 3 };

        quickSort(array, 0, array.Length - 1);

        Debug.Log(string.Join(",", array));
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
}
