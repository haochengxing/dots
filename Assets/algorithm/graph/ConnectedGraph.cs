using System;
using UnityEngine;

/*
 ����int Toplogical(Linded WDipaph G)�Ĺ����Ƕ�ͼG�еĶ�������������򣬲����عؼ�·���ĳ��ȡ�
 ����ͼG��ʾһ������n�������AOE-����ͼ�ж����1��n���α�ţ�ͼG�Ĵ洢�ṹ�����ڽӱ��ʾ�����������Ͷ������£�

 AOE(Activity On Edge network)������һ�ָ�Ȩ�������޻�ͼ��
 ��AOE��������������ķ������£�
 ����AOE����ѡ��һ�����Ϊ0(û��ǰ��)�Ķ������������
 �ڴ�����ɾ���ö��㼰����ö����йص����бߡ�
 ���ظ�����������ֱ�����в��������Ϊ0�Ķ���Ϊֹ��

 https://easylearn.baidu.com/edu-page/tiangong/bgkdetail?id=762f9cefb8f67c1cfad6b8c9
 */

public class Gnode
{
    /* �ڽӱ�ı������� */

    public int adjvex;                /* �ڽӶ����� */

    public int weight;    /* ���ϵ�Ȩֵ */

    public Gnode nextarC;    /* ָʾ��һ�����Ľ�� */

}


public class Adjlist
{
    /* �ڽӱ��ͷ������� */

    public char vdata;               /* �����������Ϣ */

    public Gnode Firstadj;     /* ָ���ڽӱ�ĵ�һ������ */

}


public class LinkedWDigraph
{
    /* ͼ������*/

    public int n, e; /* ͼ�ж�������ͱ��� */

    public Adjlist[] head;        /* ָ��ͼ�е�һ��������ڽӱ��ͷ��� */

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

        indegree = new int[G.n + 1]; /*�洢���и���������*/

        Stack = new int[G.n + 1];  /*�洢���Ϊ0�Ķ���ı��*/

        if (ve == null || indegree == null || Stack == null) return 0;

        for (j = 1; j <= G.n; j++)
        {

            ve[j] = 0; indegree[j] = 0;

        }/*for*/

        for (j = 1; j <= G.n; j++)
        {
            /* �����и��������� */

            p = G.head[j].Firstadj;

            while (p != null)
            {

                indegree[p.adjvex]++;
                p = p.nextarC;


            }/*while*/

        }/*for*/

        for (j = 1; j <= G.n; j++)
        {
            /*���������Ϊ0�Ķ��㲢��������*/

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
