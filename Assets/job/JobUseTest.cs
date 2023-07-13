using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
public class JobUseTest : MonoBehaviour
{
    //是否开启Job计算
    [SerializeField]
    private bool mIsJob;
    void Update()
    {
        //记录一下开始的时间
        float startTime = Time.realtimeSinceStartup;
        if (mIsJob)
        {
            NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
            for (int i = 0; i < 10; i++)
            {
                JobHandle jobHandle = StartJobCalculation();
                jobHandleList.Add(jobHandle);
            }
            //在job系统处理时 会暂停主线程，要等我们job系统的所有计算都结束了之后，继续跑主线程
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
        //打印计算的耗时  这个耗时是毫数
        Debug.Log("mIsJob "+ mIsJob+" "+(Time.realtimeSinceStartup - startTime) * 1000 + "ms");
    }
    /// <summary>
    /// 默认计算
    /// </summary>
    public void NormalCalculation()
    {
        for (int i = 0; i < 100000; i++)
        {
            math.pow(math.sqrt(i), i);
        }
    }
    /// <summary>
    /// 开始job计算
    /// </summary>
    /// <returns></returns>
    public JobHandle StartJobCalculation()
    {
        JobCalculationStruct jobCalculation = new JobCalculationStruct();
        return jobCalculation.Schedule();
    }
}
/// <summary>
///Job计算
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
