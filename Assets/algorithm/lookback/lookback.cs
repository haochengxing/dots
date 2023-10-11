using UnityEngine;

public class lookback : MonoBehaviour
{

    void Start()
    {
        backtrack(0);

        for (int i = 0; i < bestX.Length; i++)
        {
            Debug.Log("��"+(i+1)+"�������ӵ�"+ bestX[i]+ "����Ӧ�̹���");
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
            /*�õ������*/

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
            /*�н�*/

            found = 1;

        }

        for (j = 0; j < m; j++)
        {

            /*��i�������ӵ�j����Ӧ�̹���*/

            x[i] = j;

            //Debug.Log("i=" + i + ",j=" + j);

            cw = cw + w[i, j];

            cp = cp + c[i, j];

            if (cp <= cc && cw < bestW)
            {
                /*�����������չ��ǰ���*/

                if (backtrack(i + 1) > 0 ) { found = -1; }

            }

            /*����*/

            cw = cw - w[i, j];

            cp = cp - c[i, j];

        }

        return found;

    }
}
