using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain : MonoBehaviour
{
    int[,] cost;
    int[,] trace;

    string result="";

    // Start is called before the first frame update
    void Start()
    {
        int n = 6;

        int[] seq = { 30,35,15,5,10,20,25};

        cost = new int[n,n];

        trace = new int[n,n];

        for (int i = 0; i < trace.GetLength(0); i++)
        {
            for (int j = 0; j < trace.GetLength(1); j++)
            {
                trace[i, j] = -1;
            }
        }

        Debug.Log(matrixchain(n,seq));

        result = "";

        BestS(trace, 0, n-1);

        Debug.Log(result);

        Debug.Log("次序矩阵s为：");
        for (int i = 0; i < n; i++)
        {
            string s = "";
            for (int j = 0; j < n; j++)
                s+= (trace[i,j]+1) + "\t";
            Debug.Log(s);
        }

        Debug.Log("最优解矩阵m为：");
        for (int i = 0; i < n; i++)
        {
            string s = "";
            for (int j = 0; j < n; j++)
                s += cost[i, j] + "\t";
            Debug.Log(s);
        }  

    }

    int matrixchain(int n, int[] seq)
    {
        int tempCost;
        int tempTrace = 0;
        int i, j, k, p;
        int temp;
        for (i = 0; i < n; i++)
        {
            cost[i, i] = 0;
        }
        for (p = 1; p < n; p++)
        {
            for (i = 0; i < n - p; i++)
            {
                j = i + p;
                tempCost = -1;
                for (k = i; k < j; k++)
                {
                    temp = cost[i, k] + cost[k + 1, j] + seq[i] * seq[k + 1] * seq[j + 1];
                    if (tempCost == -1 || tempCost > temp)
                    {
                        tempCost = temp;
                        tempTrace = k;
                    }
                }
                cost[i, j] = tempCost;
                trace[i, j] = tempTrace;
            }
        }
        return cost[0, n - 1];
    }

    void BestS(int[,] s, int i, int j)
    {
        if (i == j)
        {
            result+= "A" + (i+1); return;
        }
        else
        {
            result += "("; 
            BestS(s, i, s[i,j]);
            result += ")";
            result += "("; 
            BestS(s, s[i,j] + 1, j);
            result += ")";

        }
    }
}
