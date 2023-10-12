using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class undigraph : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int n = 4;
        int[,] c = { 
            {0,1,1,1 },
            {1,0,1,0 },
            {1,1,0,1 },
            {1,0,1,0 } 
        };
        int[] x = new int[n];
        Hamilton(n,x,c);
    }

    void Hamilton(int n, int[] x, int[,] c)
    {
        int i;
        int[] visited = new int[n];
        int k;
        /*��ʼ�� x ����� visited ����*/
        for (i = 0; i < n; i++)
        {
            x[i] = 0;
            visited[i] = 0;
        }
        /*������ʼ����*/
        k = 0;
        visited[0] = 1;
        x[0] = 0;
        k = k + 1;
        /*������������*/
        while (k >= 0)
        {
            x[k] = x[k] + 1;
            while (x[k] < n)
            {
                if (visited[x[k]] == 0 && c[x[k - 1], x[k]] == 1) 
                {
                    /*�ڽӶ��� x[k]δ�����ʹ�*/ 
                    break; 
                }
                else { 
                    x[k] = x[k] + 1; 
                }
            }
            if (x[k] < n && k == n-1 && c[x[k],0] == 1){ 
                /*�ҵ�һ�����ܶ��ٻ�·*/
                for (k = 0; k < n; k++)
                {
                    Debug.Log(x[k] + "--");
                    /*������ܶ��ٻ�·*/
                }

                Debug.Log(x[0]);
                return;
            }
            else if (x[k] < n && k < n - 1)
            {
                /*���õ��ڶ���ķ��ʱ�־��������һ������*/
                visited[x[k]] = 1;
                k = k + 1;
            }
            else
            {
                /*û��δ�����ʹ����ڽӶ��㣬���˵���һ������*/
                x[k] = 0;
                visited[x[k-1]] = 0;
                k = k - 1;
            }
        }
    }
}
