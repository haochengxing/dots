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
        /*初始化 x 数组和 visited 数组*/
        for (i = 0; i < n; i++)
        {
            x[i] = 0;
            visited[i] = 0;
        }
        /*访问起始顶点*/
        k = 0;
        visited[0] = 1;
        x[0] = 0;
        k = k + 1;
        /*访问其他顶点*/
        while (k >= 0)
        {
            x[k] = x[k] + 1;
            while (x[k] < n)
            {
                if (visited[x[k]] == 0 && c[x[k - 1], x[k]] == 1) 
                {
                    /*邻接顶点 x[k]未被访问过*/ 
                    break; 
                }
                else { 
                    x[k] = x[k] + 1; 
                }
            }
            if (x[k] < n && k == n-1 && c[x[k],0] == 1){ 
                /*找到一条哈密尔顿回路*/
                for (k = 0; k < n; k++)
                {
                    Debug.Log(x[k] + "--");
                    /*输出哈密尔顿回路*/
                }

                Debug.Log(x[0]);
                return;
            }
            else if (x[k] < n && k < n - 1)
            {
                /*设置当期顶点的访问标志，继续下一个顶点*/
                visited[x[k]] = 1;
                k = k + 1;
            }
            else
            {
                /*没有未被访问过的邻接顶点，回退到上一个顶点*/
                x[k] = 0;
                visited[x[k-1]] = 0;
                k = k - 1;
            }
        }
    }
}
