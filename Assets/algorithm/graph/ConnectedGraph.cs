using System;
using UnityEngine;

public class Gnode
{
    /* 邻接表的表结点类型 */

    public int adjvex;                /* 邻接顶点编号 */

    public int weight;    /* 弧上的权值 */

    public Gnode nextarC;    /* 指示下一个弧的结点 */

}


public class Adjlist
{
    /* 邻接表的头结点类型 */

    public char vdata;               /* 顶点的数据信息 */

    public Gnode Firstadj;     /* 指向邻接表的第一个表结点 */

}


public class LinkedWDigraph
{
    /* 图的类型*/

    public int n, e; /* 图中顶点个数和边数 */

    public Adjlist[] head;        /* 指向图中第一个顶点的邻接表的头结点 */

}


public class ConnectedGraph : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gnode node = null;


        Adjlist v1 = new Adjlist();
        node = new Gnode();
        node.adjvex = 2;
        node.weight = 10;
        v1.Firstadj = node;

        node = new Gnode();
        node.adjvex = 3;
        node.weight = 30;
        v1.Firstadj.nextarC = node;


        Adjlist v2 = new Adjlist();
        node = new Gnode();
        node.adjvex = 4;
        node.weight = 30;
        v2.Firstadj = node;

        node = new Gnode();
        node.adjvex = 6;
        node.weight = 50;
        v2.Firstadj.nextarC = node;


        Adjlist v3 = new Adjlist();
        node = new Gnode();
        node.adjvex = 5;
        node.weight = 30;
        v3.Firstadj = node;

        node = new Gnode();
        node.adjvex = 6;
        node.weight = 20;
        v3.Firstadj.nextarC = node;


        Adjlist v4 = new Adjlist();


        Adjlist v5 = new Adjlist();
        node = new Gnode();
        node.adjvex = 4;
        node.weight = 10;
        v5.Firstadj = node;


        Adjlist v6 = new Adjlist();
        node = new Gnode();
        node.adjvex = 4;
        node.weight = 20;
        v6.Firstadj = node;


        LinkedWDigraph digraph = new LinkedWDigraph();
        digraph.e = 8;
        digraph.n = 6;
        digraph.head = new Adjlist[digraph.n+1];
        digraph.head[1] = v1;
        digraph.head[2] = v2;
        digraph.head[3] = v3;
        digraph.head[4] = v4;
        digraph.head[5] = v5;
        digraph.head[6] = v6;

        for (int i = 1; i < digraph.head.Length; i++)
        {
            digraph.head[i].vdata = Convert.ToChar(i.ToString());
        }

        Debug.Log(Toplogical(digraph));
    }

    // Update is called once per frame
    void Update()
    {

    }

    int Toplogical(LinkedWDigraph G)
    {
        Gnode p;

        int j = 0, w = 0, top = 0;

        int[] Stack;
        int[] ve;
        int[] indegree;

        ve = new int[G.n + 1];

        indegree = new int[G.n + 1]; /*存储网中各顶点的入度*/

        Stack = new int[G.n + 1];  /*存储入度为0的顶点的编号*/

        if (ve == null || indegree == null || Stack == null) return 0;

        for (j = 1; j <= G.n; j++)
        {

            ve[j] = 0; indegree[j] = 0;

        }/*for*/

        for (j = 1; j <= G.n; j++)
        {
            /* 求网中各顶点的入度 */

            p = G.head[j].Firstadj;

            while (p != null)
            {

                indegree[p.adjvex]++;
                p = p.nextarC;


            }/*while*/

        }/*for*/

        for (j = 1; j <= G.n; j++)
        {
            /*求网中入度为0的顶点并保存其编号*/

            if (indegree[j] == 0) Stack[++top] = j;

        }

        while (top > 0)
        {

            w = Stack[top--];

            Debug.Log(G.head[w].vdata);

            p = G.head[w].Firstadj;

            while (p != null)
            {

                indegree[p.adjvex]--;

                if (indegree[p.adjvex] == 0)

                    Stack[++top] = p.adjvex;

                if (ve[w] + p.weight > ve[p.adjvex])

                    ve[p.adjvex] = ve[w] + p.weight;

                p = p.nextarC;

            }/* while */

        }/* while */


        return ve[w];

    }/*Toplogical*/
}
