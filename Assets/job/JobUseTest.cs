using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
public class JobUseTest : MonoBehaviour
{
    //�Ƿ���Job����
    [SerializeField]
    private bool mIsJob;
    void Update()
    {
        //��¼һ�¿�ʼ��ʱ��
        float startTime = Time.realtimeSinceStartup;
        if (mIsJob)
        {
            NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
            for (int i = 0; i < 10; i++)
            {
                JobHandle jobHandle = StartJobCalculation();
                jobHandleList.Add(jobHandle);
            }
            //��jobϵͳ����ʱ ����ͣ���̣߳�Ҫ������jobϵͳ�����м��㶼������֮�󣬼��������߳�
            JobHandle.CompleteAll(jobHandleList);
            jobHandleList.Dispose();
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                NormalCalculation();
            }
        }
        //��ӡ����ĺ�ʱ  �����ʱ�Ǻ���
        Debug.Log("mIsJob "+ mIsJob+" "+(Time.realtimeSinceStartup - startTime) * 1000 + "ms");
    }
    /// <summary>
    /// Ĭ�ϼ���
    /// </summary>
    public void NormalCalculation()
    {
        for (int i = 0; i < 100000; i++)
        {
            math.pow(math.sqrt(i), i);
        }
    }
    /// <summary>
    /// ��ʼjob����
    /// </summary>
    /// <returns></returns>
    public JobHandle StartJobCalculation()
    {
        JobCalculationStruct jobCalculation = new JobCalculationStruct();
        return jobCalculation.Schedule();
    }
}
/// <summary>
///Job����
/// </summary>
[BurstCompile]
public struct JobCalculationStruct : IJob
{
    public void Execute()
    {
        for (int i = 0; i < 100000; i++)
        {
            math.pow(math.sqrt(i), i);
        }
    }
}
