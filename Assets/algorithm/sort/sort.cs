using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 9, 5, 8, 7, 2, 6, 4, 1, 3 };

        //insertSort(array);
        //bubbleSort(array);
        //selectSort(array);
        //shellSort(array);
        //quickSort(array,0,array.Length-1);
        //heapSort(array);
        mergeSort(array, 0, array.Length - 1);


        Debug.Log(string.Join(",", array));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //交换的方法
    void swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }


    //插入排序
    //最好情况的时间复杂度：O(n)
    //最差情况的时间复杂度：O(n^2)


    //直接插入	O(n^2)	O(1)	稳定
    void insertSort(int[]array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            for(int j = i - 1; j >= 0; j--)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
                else
                {
                    break;
                }
            }
        }
    }

    //冒泡排序
    //时间复杂度O(n^2)


    
    //冒泡排序 O(n^2)  O(1)    稳定
    void bubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length-i-1; j++)
            {
                if (array[j] > array[j+1])
                {
                    int temp = array[j];
                    array[j] = array[j+1];
                    array[j+1] = temp;
                }
            }
        }
    }


    //简单选择排序
    //最好情况的时间复杂度：O(n)
    //最差情况的时间复杂度：O(n^2)


    //简单选择 O(n^2)  O(1)    不稳定
    void selectSort(int[] array)
    {
        for (int i=0;i<array.Length;i++) 
        {
            int minIndex = i;
            for (int j = i+1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex!=i)
            {
                swap(array,i,minIndex);
            }
        }
    }


    //希尔排序
    //最好情况的时间复杂度：O(n)
    //最差情况的时间复杂度：O(n^1.3)


    //希尔排序 O(n^2)  O(1)    不稳定
    void shellSort(int[] array)
    {
        int gap = array.Length;
        while (gap > 1)
        {
            gap/= 2;
            shell(array, gap);
        }
    }

    void shell(int[] array,int gap)
    {
        for (int i = gap; i < array.Length; i++)
        {
            int temp = array[i];
            int j = i - gap;
            for (; j>=0; j-=gap)
            {
                if (array[j]>temp)
                {
                    array[j + gap] = array[j];
                }
                else
                {
                    break;
                }
            }
            array[j + gap] = temp;
        }
    }


    //快速排序
    //最好情况的时间复杂度：O(nlog2n)
    //最差情况的时间复杂度：O(n^2)


    //快速排序	O(nlgn)	O(nlgn)	不稳定
    void quickSort(int[]array,int begin,int end)
    {
        if (begin > end)
        {
            return;
        }
        int temp = array[begin];
        int i = begin;
        int j = end;
        while (i != j)
        {
            while (array[i]<=temp && i < j)
            {
                i++;
            }
            while (array[j] >= temp && i < j)
            {
                j--;
            }
            if (j > i)
            {
                swap(array, i, j);
            }
        }
        array[begin] = array[i];
        array[i] = temp;
        quickSort(array, begin, i-1);
        quickSort(array, i+1, end);
    }

    //堆排序
    //最好情况的时间复杂度：O(nlog2n)
    //最差情况的时间复杂度：O(nlog2n)


    //堆排序		O(nlgn)	O(1)	不稳定
    void heapSort(int []array)
    {
        createBigHeap(array);
        int end = array.Length - 1;
        while (end>0)
        {
            swap(array,0,end);
            shiftDown(array,0, end);
            end--;
        }
    }

    void createBigHeap(int[]array)
    {
        for (int parent = (array.Length-2) / 2; parent >= 0; parent--)
        {
            shiftDown(array, parent,array.Length);
        }
    }

    void shiftDown(int []array,int parent,int len)
    {
        int child = 2*parent+1;
        while (child<len)
        {
            if(child+1<len && array[child] < array[child + 1])
            {
                child++;
            }
            if(array[parent] < array[child])
            {
                swap(array, parent, child);
                parent = child;
                child = 2*parent+1;
            }
            else
            {
                break;
            }
        }
    }

    //归并排序
    //最好情况的时间复杂度：O(nlog2n)
    //最差情况的时间复杂度：O(nlog2n)


    //归并排序	O(nlgn)	O(n)	稳定
    void mergeSort(int[]array,int left,int right)
    {
        if (left >= right)
        {
            return;
        }
        int mid = (left + right)/2;
        mergeSort(array, left, mid);
        mergeSort(array, mid+1, right);
        merge(array, left, mid, right);
    }

    void merge(int[]array,int left,int mid,int right)
    {
        int s1 = left;
        int s2=mid+1;
        int []temp = new int[right - left + 1];
        int i= 0;
        while (s1<=mid&&s2<=right)
        {
            if (array[s1] < array[s2])
            {
                temp[i++] = array[s1++];
            }
            else
            {
                temp[i++] = array[s2++];
            }
        }
        while (s1 <= mid)
        {
            temp[i++] = array[s1++];
        }
        while (s2 <= right)
        {
            temp[i++] = array[s2++];
        }
        for (int j = 0; j < temp.Length; j++)
        {
            array[left + j] = temp[j];
        }
    }
}
