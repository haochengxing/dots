using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editdistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(EditDistance("CTGA", "ACGCTA"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int EditDistance(string str1,string str2)
    {
        int i=0,j=0;
        int temp = 0;
        int len1=str1.Length;
        int len2=str2.Length;
        int[,]d= new int[len1+1,len2+1];
        for (i = 0; i <= len1; i++)
        {
            d[i, 0] = i;
        }
        for (j = 0; j <= len2; j++)
        {
            d[0, j] = j;
        }
        for (i = 1; i <= len1; i++)
        {
            for (j = 1; j <= len2; j++)
            {
                if (str1[i-1].Equals(str2[j-1]))
                {
                    d[i, j] = d[i - 1, j - 1];
                }
                else
                {
                    temp = Mathf.Min(d[i - 1, j] + 1, d[i,j-1]+1);
                    d[i, j] = Mathf.Min(temp, d[i - 1, j - 1]+1);
                }
            }
        }
        return d[len1,len2];
    }
}
