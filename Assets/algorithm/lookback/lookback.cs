using UnityEngine;

public class lookback : MonoBehaviour
{

    void Start()
    {
        backtrack(0);

        for (int i = 0; i < bestX.Length; i++)
        {
            Debug.Log("第"+(i+1)+"个部件从第"+ bestX[i]+ "个供应商购买");
        }
    }


    int n = 3;

    int m = 3;

    int cc = 4;

    int[,] w = { { 1, 2, 3 }, { 3, 2, 1 }, { 2, 2, 2 } };

    int[,] c = { { 1, 2, 3 }, { 3, 2, 1 }, { 2, 2, 2 } };

    int bestW = 8;

    int bestC = 0;

    int[] bestX = { 0, 0, 0 };

    int cw = 0;

    int cp = 0;

    int[] x = { 0, 0, 0 };

    int backtrack(int i)
    {

        int j = 0;

        int found = 0;

        if (i > n - 1)
        {  
            /*得到问题解*/

            bestW = cw;

            bestC = cp;

            for (j = 0; j < n; j++)
            {

                bestX[j] = x[j]+1;

            }

            return 1;

        }

        if (cp <= cc)
        {
            /*有解*/

            found = 1;

        }

        for (j = 0; j < m; j++)
        {

            /*第i个部件从第j个供应商购买*/

            x[i] = j;

            //Debug.Log("i=" + i + ",j=" + j);

            cw = cw + w[i, j];

            cp = cp + c[i, j];

            if (cp <= cc && cw < bestW)
            {
                /*深度搜索，扩展当前结点*/

                if (backtrack(i + 1) > 0 ) { found = -1; }

            }

            /*回溯*/

            cw = cw - w[i, j];

            cp = cp - c[i, j];

        }

        return found;

    }
}
